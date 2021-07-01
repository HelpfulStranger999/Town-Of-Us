using System;
using HarmonyLib;
using TownOfUs.Roles;
using Object = UnityEngine.Object;

namespace TownOfUs.ImpostorRoles.UndertakerMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Undertaker))
            {
                var role = BaseRole.GetRole<Undertaker>(PlayerControl.LocalPlayer);
                role.DragDropButton.renderer.sprite = TownOfUs.DragSprite;
                role.CurrentlyDragging = null;
                role.LastDragged = DateTime.UtcNow;
            }
        }
    }
}