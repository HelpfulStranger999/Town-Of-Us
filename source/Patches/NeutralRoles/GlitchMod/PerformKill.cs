using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.GlitchMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    internal class PerformKill
    {
        public static bool Prefix(KillButtonManager __instance)
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Glitch) && __instance.isActiveAndEnabled &&
                !__instance.isCoolingDown)
                return RoleService.Instance.GetRoles().GetRoleOfPlayer<Glitch>(PlayerControl.LocalPlayer).UseAbility(__instance);

            return true;
        }
    }
}