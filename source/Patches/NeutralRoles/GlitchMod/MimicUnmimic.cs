using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.GlitchMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class MimicUnmimic
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Glitch>())
            {
                var glitch = (Glitch)role;
                if (glitch.IsUsingMimic)
                    Utils.Morph(glitch.Player, glitch.MimicTarget);
                else if (glitch.MimicTarget) Utils.Unmorph(glitch.Player);
            }
        }
    }
}