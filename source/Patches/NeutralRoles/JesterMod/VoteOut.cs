using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.JesterMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    internal class MeetingExiledEnd
    {
        private static void Postfix(ExileController __instance)
        {
            var exiled = __instance.exiled;
            if (exiled == null) return;
            var player = exiled.Object;

            var role = RoleService.Instance.GetRoles().GetRoleOfPlayer(player);
            if (role == null) return;
            if (role.RoleType == RoleEnum.Jester) ((Jester)role).Wins();
        }
    }
}