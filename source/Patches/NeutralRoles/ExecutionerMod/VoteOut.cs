using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.ExecutionerMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    internal class MeetingExiledEnd
    {
        private static void Postfix(ExileController __instance)
        {
            var exiled = __instance.exiled;
            if (exiled == null) return;
            var player = exiled.Object;

            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Executioner>())
                if (player.PlayerId == ((Executioner)role).target.PlayerId)
                    ((Executioner)role).Wins();
        }
    }
}