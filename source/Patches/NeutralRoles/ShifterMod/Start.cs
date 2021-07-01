using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.ShifterMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class Start
    {
        public static void Postfix(ShipStatus __instance)
        {
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Shifter>())
            {
                var shifter = (Shifter)role;
                shifter.LastShifted = DateTime.UtcNow;
            }
        }
    }
}