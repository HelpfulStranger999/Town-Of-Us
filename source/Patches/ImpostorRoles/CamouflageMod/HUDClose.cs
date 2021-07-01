using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;
using Object = UnityEngine.Object;

namespace TownOfUs.ImpostorRoles.CamouflageMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Camouflager>())
            {
                var camouflager = (Camouflager)role;
                camouflager.LastCamouflaged = DateTime.UtcNow;
                camouflager.LastCamouflaged = camouflager.LastCamouflaged.AddSeconds(-10f);
            }
        }
    }
}