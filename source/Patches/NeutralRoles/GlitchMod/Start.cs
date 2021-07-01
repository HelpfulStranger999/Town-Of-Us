using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.NeutralRoles.GlitchMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__14), nameof(IntroCutscene._CoBegin_d__14.MoveNext))]
    internal class Start
    {
        private static void Postfix(IntroCutscene._CoBegin_d__14 __instance)
        {
            if (RoleService.Instance.GetRoles().TryGetRole<Glitch>(out var glitch))
            {
                glitch.LastMimic = DateTime.UtcNow;
                glitch.LastHack = DateTime.UtcNow;
                glitch.LastKill = DateTime.UtcNow.AddSeconds(CustomGameOptions.InitialGlitchKillCooldown +
                                                                        CustomGameOptions.GlitchKillCooldown * -1);
            }
        }
    }
}