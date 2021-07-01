using HarmonyLib;
using InnerNet;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.GlitchMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    internal class Update
    {
        private static void Postfix(HudManager __instance)
        {
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started &&
                RoleService.Instance.GetRoles().TryGetRoleOfPlayer<Glitch>(PlayerControl.LocalPlayer, out var glitch))
            {
                glitch.Update(__instance);
            }
        }
    }
}