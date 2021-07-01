using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;
using Object = UnityEngine.Object;

namespace TownOfUs.ImpostorRoles.SwooperMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Swooper))
            {
                var role = RoleService.Instance.GetRoles().GetRoleOfPlayer<Swooper>(PlayerControl.LocalPlayer);
                role.LastSwooped = DateTime.UtcNow;
            }
        }
    }
}