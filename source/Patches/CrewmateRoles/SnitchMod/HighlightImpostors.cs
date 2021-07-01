using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;

namespace TownOfUs.CrewmateRoles.SnitchMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HighlightImpostors
    {
        private static void UpdateMeeting(MeetingHud __instance)
        {
            foreach (var state in __instance.playerStates)
            {
                if (Utils.PlayerById(state.TargetPlayerId).Data.IsImpostor) state.NameText.color = Palette.ImpostorRed;

                var role = RoleService.Instance.GetRoles().GetRoleOfPlayer(state.TargetPlayerId);
                if (role.Faction == Faction.Neutral && CustomGameOptions.SnitchSeesNeutrals)
                    state.NameText.color = role.Color;
            }
        }

        public static void Postfix(HudManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Snitch)) return;
            var role = RoleService.Instance.GetRoles().GetRoleOfPlayer<Snitch>(PlayerControl.LocalPlayer);
            if (!role.TasksDone) return;
            if (MeetingHud.Instance) UpdateMeeting(MeetingHud.Instance);

            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (player.Data.IsImpostor) player.nameText.color = Palette.ImpostorRed;
                var playerRole = RoleService.Instance.GetRoles().GetRoleOfPlayer(player);
                if (playerRole.Faction == Faction.Neutral && CustomGameOptions.SnitchSeesNeutrals)
                    player.nameText.color = role.Color;
            }
        }
    }
}