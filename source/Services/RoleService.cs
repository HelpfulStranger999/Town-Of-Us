using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TownOfUs.NeutralRoles.PhantomMod;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public class RoleService : IRoleService
    {
        // Temporary Singleton for smoother integration
        public static RoleService Instance { get; } = new RoleService(new ReflectionRoleFactory(), Configuration.Instance);

        private IRoleFactory Factory { get; }
        private Configuration Config { get; }
        private IRoleCollection Collection { get; }
        private Random RandomGenerator { get; }

        public RoleService(IRoleFactory roleFactory, Configuration config)
        {
            Factory = roleFactory;
            Config = config;
            Collection = new RoleCollection();
        }

        public void AssignRoles(IEnumerable<PlayerControl> players, IEnumerable<RoleAssignmentConfiguration> configurations)
        {
            var totalPlayers = players.Shuffle().ToList();

            if (Config.LoversProbability <= RandomGenerator.NextDouble())
            {
                AssignLovers(totalPlayers);
            }

            var nonimpostors = players.Where(player => !player.Data.IsImpostor).Shuffle().ToList();
            var impostors = players.Where(player => player.Data.IsImpostor).Shuffle().ToList();

            AssignRolesToCrewmates(nonimpostors,
                configurations.Where(config => config.Faction == Faction.Crewmates));

            AssignRolesToCrewmates(nonimpostors,
                configurations.Where(config => config.Faction == Faction.Neutral), Config.MaxNeutralRoles);

            AssignRolesToCrewmates(impostors,
                configurations.Where(config => config.Faction == Faction.Impostors), Config.MaxImpostorRoles);

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte)CustomRPC.SetPhantom, SendOption.Reliable, -1);

            if (Config.PhantomProbability <= RandomGenerator.NextDouble())
            {
                var possiblePhantoms = nonimpostors;
                if (!possiblePhantoms.Any())
                {
                    possiblePhantoms = Collection.GetRoles().Where(role => role.Faction == Faction.Crewmates)
                        .Select(role => role.Player).ToList();
                }

                var phantom = possiblePhantoms.SelectRandomElement();
                SetPhantom.WillBePhantom = PlayerControl.AllPlayerControls.ToArray().Single(player => player.Data.PlayerId == phantom.PlayerId);
                writer.Write(phantom.PlayerId);
            }
            else
            {
                writer.Write(byte.MaxValue);
            }

            AmongUsClient.Instance.FinishRpcImmediately(writer);

            foreach (var crewmate in nonimpostors)
                AssignRole(typeof(Crewmate), crewmate);

            foreach (var impostor in impostors)
                AssignRole(typeof(Impostor), impostor);
        }

        private void AssignLovers(List<PlayerControl> players)
        {
            var firstPlayer = players.SelectRandomElement();
            players.Remove(firstPlayer);

            PlayerControl secondPlayer = null;

            if (AssignLover(firstPlayer))
            {
                SpinWait.SpinUntil(() =>
                {
                    secondPlayer = players.SelectRandomElement();
                    return !secondPlayer.Data.IsImpostor;
                });
            }
            else
            {
                secondPlayer = players.SelectRandomElement();
            }

            players.Remove(secondPlayer);
            AssignLover(secondPlayer);

            var firstLover = Collection.GetRoleOfPlayer<BaseLover>(firstPlayer.PlayerId);
            var secondLover = Collection.GetRoleOfPlayer<BaseLover>(secondPlayer.Data.PlayerId);
            firstLover.SetLover(secondLover);
            secondLover.SetLover(firstLover);

            firstLover.SendSetRpc();
        }

        private bool AssignLover(PlayerControl player)
        {
            if (player.Data.IsImpostor)
            {
                AssignRole(typeof(LovingImpostor), player);
                return true;
            }
            else
            {
                AssignRole(typeof(Lover), player);
                return false;
            }
        }

        private void AssignRolesToCrewmates(List<PlayerControl> players,
            IEnumerable<RoleAssignmentConfiguration> configurations, int maxRoles = -1)
        {
            var orderedConfigs = configurations.OrderByDescending(config => config.Probability);
            var guaranteedConfigs = orderedConfigs.TakeWhile(config => config.Probability == 100.0f).ToList();

            var assignedRoles = 0;

            foreach (var config in guaranteedConfigs.Shuffle())
            {
                var role = AssignRole(config.RoleType, players[0]);
                players.RemoveAt(0);
                if (++assignedRoles >= maxRoles) return;
            }

            var tickets = new List<RoleAssignmentConfiguration>();
            foreach (var config in orderedConfigs.SkipWhile(config => config.Probability == 100.0f))
            {
                for (var i = 0; i < config.Probability / 0.05; i++)
                    tickets.Add(config);
            }

            foreach (var config in tickets.Shuffle())
            {
                var role = AssignRole(config.RoleType, players[0]);
                players.RemoveAt(0);
                if (++assignedRoles >= maxRoles) return;
            }
        }

        public BaseRole AssignRole(Type roleType, PlayerControl player)
        {
            var role = Factory.CreateRole(roleType, Array.Empty<object>());
            Collection.AddRole(player.Data.PlayerId, role);
            role.SendSetRpc();
            role.AssignPlayer(player);
            return role;
        }

        public void Reset()
        {
            Collection.Clear();
        }

        public IRoleCollection GetRoles()
            => Collection;
    }
}