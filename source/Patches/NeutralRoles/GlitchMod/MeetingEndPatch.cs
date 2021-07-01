using HarmonyLib;
using System;
using TownOfUs.Roles;
using TownOfUs.Services;
using Object = UnityEngine.Object;

namespace TownOfUs.NeutralRoles.GlitchMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    internal class MeetingExiledEnd
    {
        private static void Prefix(Object obj)
        {
            if (ExileController.Instance != null && obj == ExileController.Instance.gameObject)
            {
                if (RoleService.Instance.GetRoles().TryGetRole<Glitch>(out var glitch))
                    glitch.LastKill = DateTime.UtcNow;
            }
        }
    }
}