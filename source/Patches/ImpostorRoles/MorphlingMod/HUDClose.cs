using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;
using Object = UnityEngine.Object;

namespace TownOfUs.ImpostorRoles.MorphlingMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Morphling))
            {
                var role = RoleService.Instance.GetRoles().GetRoleOfPlayer<Morphling>(PlayerControl.LocalPlayer);
                role.MorphButton.renderer.sprite = TownOfUs.SampleSprite;
                role.SampledPlayer = null;
                role.LastMorphed = DateTime.UtcNow;
            }
        }
    }
}