using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.CrewmateRoles.InvestigatorMod
{
    public static class EndGame
    {
        public static void Reset()
        {
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Investigator>())
                role.AllPrints.Clear();
        }

        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
        public static class EndGamePatch
        {
            public static void Prefix(AmongUsClient __instance)
            {
                Reset();
            }
        }

        [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
        public static class EndGameManagerPatch
        {
            public static bool Prefix(EndGameManager __instance)
            {
                Reset();

                return true;
            }
        }
    }
}