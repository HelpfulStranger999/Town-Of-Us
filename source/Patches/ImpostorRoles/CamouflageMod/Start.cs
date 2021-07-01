using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.ImpostorRoles.CamouflageMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class Start
    {
        public static void Postfix(ShipStatus __instance)
        {
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Camouflager>())
            {
                var seer = (Camouflager)role;
                seer.LastCamouflaged = DateTime.UtcNow;
            }
        }
    }
}