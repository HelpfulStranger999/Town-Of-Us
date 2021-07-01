using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.EngineerMod
{
    public enum EngineerFixPer
    {
        Round,
        Game
    }

    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Engineer>())
            {
                var engineer = (Engineer) role;
                if (CustomGameOptions.EngineerFixPer == EngineerFixPer.Round) engineer.UsedThisRound = false;
            }
        }
    }
}