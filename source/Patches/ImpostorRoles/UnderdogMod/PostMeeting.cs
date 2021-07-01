using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.ImpostorRoles.UnderdogMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public static class HUDClose
    {
        public static void Postfix()
        {
            var role = RoleService.Instance.GetRoles().GetRoleOfPlayer(PlayerControl.LocalPlayer);
            if (role?.RoleType == RoleEnum.Underdog)
                ((Underdog)role).SetKillTimer();
        }
    }
}