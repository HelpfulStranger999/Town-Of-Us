using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class AmongUsClient_OnGameEnd
    {
        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] GameOverReason reason,
            [HarmonyArgument(0)] bool showAd)
        {
            Utils.potentialWinners.Clear();
            foreach (var player in PlayerControl.AllPlayerControls)
                Utils.potentialWinners.Add(new WinningPlayerData(player.Data));
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
    public class EndGameManager_SetEverythingUp
    {
        public static void Prefix()
        {
            if (BaseRole.NobodyWins)
            {
                TempData.winners = new List<WinningPlayerData>();
                return;
            }

            var jester = RoleService.Instance.GetRoles().GetRoles<Jester>().FirstOrDefault(j => j.VotedOut);
            if (jester != null)
            {
                var winners = Utils.potentialWinners.Where(x => x.Name == jester.PlayerName).ToList();
                TempData.winners = new List<WinningPlayerData>();
                foreach (var win in winners)
                {
                    win.IsDead = false;
                    TempData.winners.Add(win);
                }

                return;
            }

            var executioner = RoleService.Instance.GetRoles().GetRoles<Executioner>().FirstOrDefault(e => e.TargetVotedOut);
            if (executioner != null)
            {
                var winners = Utils.potentialWinners.Where(x => x.Name == executioner.PlayerName).ToList();
                TempData.winners = new List<WinningPlayerData>();
                foreach (var win in winners) TempData.winners.Add(win);
                return;
            }

            var lover = RoleService.Instance.GetRoles().GetRoles<BaseLover>().FirstOrDefault(l => l.LoveCoupleWins);
            if (lover != null)
            {
                var lover1 = (BaseLover)lover;
                var lover2 = lover1.OtherLover;
                var winners = Utils.potentialWinners
                    .Where(x => x.Name == lover1.PlayerName || x.Name == lover2.PlayerName).ToList();
                TempData.winners = new List<WinningPlayerData>();
                foreach (var win in winners) TempData.winners.Add(win);
                return;
            }

            var glitch = RoleService.Instance.GetRoles().GetRoles<Glitch>().FirstOrDefault(g => g.GlitchWins);
            if (glitch != null)
            {
                var winners = Utils.potentialWinners.Where(x => x.Name == glitch.PlayerName).ToList();
                TempData.winners = new List<WinningPlayerData>();
                foreach (var win in winners) TempData.winners.Add(win);
                return;
            }

            var arsonist = RoleService.Instance.GetRoles().GetRoles<Arsonist>().FirstOrDefault(a => a.ArsonistWins);
            if (arsonist != null)
            {
                var winners = Utils.potentialWinners.Where(x => x.Name == arsonist.PlayerName).ToList();
                TempData.winners = new List<WinningPlayerData>();
                foreach (var win in winners) TempData.winners.Add(win);
                return;
            }

            var phantom = RoleService.Instance.GetRoles().GetRoles<Phantom>().FirstOrDefault(p => p.CompletedTasks);
            if (phantom != null)
            {
                var winners = Utils.potentialWinners.Where(x => x.Name == phantom.PlayerName).ToList();
                TempData.winners = new List<WinningPlayerData>();
                foreach (var win in winners) TempData.winners.Add(win);
            }
        }
    }
}