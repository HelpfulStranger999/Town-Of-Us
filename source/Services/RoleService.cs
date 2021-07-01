using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AssignRoles(IEnumerable<GameData.PlayerInfo> players, IEnumerable<RoleAssignmentConfiguration> configurations)
        {
            var totalPlayers = players.Shuffle().ToList();

            if (Config.LoversProbability <= RandomGenerator.NextDouble())
            {
                for (int i = 0; i < 2; i++)
                {
                    var player = totalPlayers.SelectRandomElement();
                    if (player.IsImpostor)
                    {
                        AssignRole(new RoleAssignmentConfiguration()
                        {
                            RoleType = typeof(LovingImpostor)
                        })
                    }
                }
            }

            var nonImpostor = players.Where(player => !player.IsImpostor).Shuffle().ToList();
            var impostors = players.Where(player => player.IsImpostor).Shuffle().ToList();

            AssignRolesToCrewmates(nonImpostor,
                configurations.Where(config => config.Faction == Faction.Crewmates));

            AssignRolesToCrewmates(nonImpostor,
                configurations.Where(config => config.Faction == Faction.Neutral), Config.MaxNeutralRoles);

            AssignRolesToCrewmates(impostors,
                configurations.Where(config => config.Faction == Faction.Impostors), Config.MaxImpostorRoles);
        }

        private void AssignRolesToCrewmates(List<GameData.PlayerInfo> players,
            IEnumerable<RoleAssignmentConfiguration> configurations, int maxRoles = -1)
        {
            var orderedConfigs = configurations.OrderByDescending(config => config.Probability);
            var guaranteedConfigs = orderedConfigs.TakeWhile(config => config.Probability == 100.0f).ToList();

            var assignedRoles = 0;
            var remainingPlayers = new Queue<GameData.PlayerInfo>(players.Shuffle());

            foreach (var config in guaranteedConfigs.Shuffle())
            {
                AssignRole(config, remainingPlayers.Dequeue());
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
                AssignRole(config, remainingPlayers.Dequeue());
                if (++assignedRoles >= maxRoles) return;
            }
        }

        public void AssignRole(RoleAssignmentConfiguration roleConfig, GameData.PlayerInfo player)
        {
            var role = Factory.CreateRole(roleConfig.RoleType, System.Array.Empty<object>());
            Collection.AddRole(player.PlayerId, role);
        }

        public void Reset()
        {
            Collection.Clear();
        }

        public IRoleCollection GetRoles()
            => Collection;
    }
}