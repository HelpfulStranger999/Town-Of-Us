using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.ImpostorRoles.MorphlingMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class MorphUnmorph
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Morphling>())
            {
                var morphling = (Morphling)role;
                if (morphling.Morphed)
                    morphling.Morph();
                else if (morphling.MorphedPlayer) morphling.Unmorph();
            }
        }
    }
}