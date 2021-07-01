using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Services;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.MayorMod
{
    public static class AddAbstain
    {
        private static Sprite Abstain => TownOfUs.Abstain;

        public static void UpdateButton(Mayor role, MeetingHud __instance)
        {
            var skip = __instance.SkipVoteButton;
            role.Abstain.gameObject.SetActive(skip.gameObject.active && !role.VotedOnce);
            role.Abstain.voteComplete = skip.voteComplete;
            role.Abstain.GetComponent<SpriteRenderer>().enabled = skip.GetComponent<SpriteRenderer>().enabled;
            role.Abstain.GetComponent<SpriteRenderer>().sprite = Abstain;
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
        private static class MeetingHudStart
        {
            public static void GenButton(Mayor role, MeetingHud __instance)
            {
                var skip = __instance.SkipVoteButton;
                role.Abstain = Object.Instantiate(skip, skip.transform.parent);
                role.Abstain.Parent = __instance;
                role.Abstain.SetTargetPlayerId(251);
                role.Abstain.transform.localPosition = skip.transform.localPosition +
                                                       new Vector3(0f, -0.17f, 0f);
                skip.transform.localPosition += new Vector3(0f, 0.20f, 0f);
                UpdateButton(role, __instance);
            }

            public static void Postfix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return;
                var mayorRole = RoleService.Instance.GetRoles().GetRoleOfPlayer<Mayor>(PlayerControl.LocalPlayer);
                GenButton(mayorRole, __instance);
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.ClearVote))]
        private static class MeetingHudClearVote
        {
            public static void Postfix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return;
                var mayorRole = RoleService.Instance.GetRoles().GetRoleOfPlayer<Mayor>(PlayerControl.LocalPlayer);
                UpdateButton(mayorRole, __instance);
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
        private static class MeetingHudConfirm
        {
            public static void Postfix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return;
                var mayorRole = RoleService.Instance.GetRoles().GetRoleOfPlayer<Mayor>(PlayerControl.LocalPlayer);
                mayorRole.Abstain.ClearButtons();
                UpdateButton(mayorRole, __instance);
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Select))]
        private static class MeetingHudSelect
        {
            public static void Postfix(MeetingHud __instance, int __0)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return;
                var mayorRole = RoleService.Instance.GetRoles().GetRoleOfPlayer<Mayor>(PlayerControl.LocalPlayer);
                if (__0 != 251) mayorRole.Abstain.ClearButtons();

                UpdateButton(mayorRole, __instance);
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))]
        private static class MeetingHudVotingComplete
        {
            public static void Postfix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return;
                var mayorRole = RoleService.Instance.GetRoles().GetRoleOfPlayer<Mayor>(PlayerControl.LocalPlayer);
                UpdateButton(mayorRole, __instance);
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
        private static class MeetingHudUpdate
        {
            public static void Postfix(MeetingHud __instance)
            {
                if (!PlayerControl.LocalPlayer.Is(RoleEnum.Mayor)) return;
                var mayorRole = RoleService.Instance.GetRoles().GetRoleOfPlayer<Mayor>(PlayerControl.LocalPlayer);
                switch (__instance.state)
                {
                    case MeetingHud.VoteStates.Discussion:
                        if (__instance.discussionTimer < PlayerControl.GameOptions.DiscussionTime)
                        {
                            mayorRole.Abstain.SetDisabled();
                            break;
                        }

                        mayorRole.Abstain.SetEnabled();
                        break;
                }

                UpdateButton(mayorRole, __instance);
            }
        }
    }
}