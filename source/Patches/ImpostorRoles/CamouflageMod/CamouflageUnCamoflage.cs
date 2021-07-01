using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.ImpostorRoles.CamouflageMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class CamouflageUnCamouflage
    {
        public static bool CommsEnabled;
        public static bool CamouflagerEnabled;

        public static bool IsCamoed => CommsEnabled | CamouflagerEnabled;

        public static void Postfix(HudManager __instance)
        {
            CamouflagerEnabled = false;
            foreach (var role in RoleService.Instance.GetRoles().GetRoles<Camouflager>())
            {
                if (role.Camouflaged)
                {
                    CamouflagerEnabled = true;
                    role.Camouflage();
                }
                else if (role.Enabled)
                {
                    CamouflagerEnabled = false;
                    role.UnCamouflage();
                }
            }

            if (CustomGameOptions.ColourblindComms)
            {
                if (ShipStatus.Instance != null)
                    switch (PlayerControl.GameOptions.MapId)
                    {
                        case 0:
                        case 2:
                        case 3:
                        case 4:
                            var comms1 = ShipStatus.Instance.Systems[SystemTypes.Comms].Cast<HudOverrideSystemType>();
                            if (comms1.IsActive)
                            {
                                CommsEnabled = true;
                                Utils.Camouflage();
                                return;
                            }

                            break;
                        case 1:
                            var comms2 = ShipStatus.Instance.Systems[SystemTypes.Comms].Cast<HqHudSystemType>();
                            if (comms2.IsActive)
                            {
                                CommsEnabled = true;
                                Utils.Camouflage();
                                return;
                            }

                            break;
                    }

                if (CommsEnabled)
                {
                    CommsEnabled = false;
                    Utils.UnCamouflage();
                }
            }
        }
    }
}