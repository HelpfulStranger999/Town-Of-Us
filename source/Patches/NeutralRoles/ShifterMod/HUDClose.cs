using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;
using Object = UnityEngine.Object;

namespace TownOfUs.NeutralRoles.ShifterMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Shifter>())
            {
                var shifter = (Shifter)role;
                shifter.LastShifted = DateTime.UtcNow;
                shifter.LastShifted = shifter.LastShifted.AddSeconds(-10.0);
            }
        }
    }
}