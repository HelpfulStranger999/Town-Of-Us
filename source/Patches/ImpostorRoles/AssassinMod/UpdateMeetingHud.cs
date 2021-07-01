﻿using HarmonyLib;
using TownOfUs.Extensions;
using TownOfUs.Roles;
using TownOfUs.Services;
using UnityEngine;

namespace TownOfUs.ImpostorRoles.AssassinMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
    public class UpdateMeetingHud
    {
        private static void Postfix(MeetingHud __instance)
        {
            if (!RoleService.Instance.GetRoles().TryGetRoleOfPlayer<Assassin>(PlayerControl.LocalPlayer, out var assassin))
                return;

            foreach (var voteArea in __instance.playerStates)
            {
                var targetId = voteArea.TargetPlayerId;
                assassin.Guesses.TryGetValue(targetId, out var currentGuess);

                if (
                    assassin.GuessedThisMeeting ||
                    string.IsNullOrEmpty(currentGuess)
                ) continue;

                var playerData = Utils.PlayerById(targetId)?.Data;
                if (playerData == null || playerData.Disconnected)
                {
                    assassin.Guesses.Remove(targetId);
                    ShowHideButtons.HideSingle(assassin, targetId, false);
                    continue;
                }

                var nameText = "\n" + (currentGuess == "None"
                    ? "Guess"
                    : "<color=#" +
                        assassin.ColorMapping[currentGuess].ToHtmlStringRGBA() +
                    $">{currentGuess}??</color>"
                );

                voteArea.NameText.text += nameText;
                voteArea.NameText.transform.localPosition = new Vector3(0.6f, 0.03f, -0.1f);
            }
        }
    }
}