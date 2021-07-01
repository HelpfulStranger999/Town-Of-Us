using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.ImpostorRoles.UndertakerMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public static class HUDClose
    {
        public static void Postfix()
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Undertaker))
            {
                var role = RoleService.Instance.GetRoles().GetRoleOfPlayer<Undertaker>(PlayerControl.LocalPlayer);
                role.DragDropButton.renderer.sprite = TownOfUs.DragSprite;
                role.CurrentlyDragging = null;
                role.LastDragged = DateTime.UtcNow;
            }
        }
    }
}