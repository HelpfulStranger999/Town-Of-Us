using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TownOfUs.Events;
using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.Services
{
    public class VoteService
    {
        public event MeetingStartEventHandler? MeetingStarted;

        public event MeetingFrameUpdateEventHandler? MeetingFrameUpdated;

        public event MeetingEndEventHandler? MeetingEnded;

        public event VotingStartEventHandler? VotingStarted;

        public event VoteSelectEventHandler? VoteSelected;

        public event VoteClearEventHandler? VoteCleared;

        public event VoteConfirmEventHandler? VoteConfirmed;

        public event VotingCompleteEventHandler? VotingCompleted;

        private RoleManager RoleManager { get; }

        public VoteService(RoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ReadOnlyCollection<IVoteStatus> TallyResults()
        {
            if (!AmongUsClient.Instance.AmHost)
                throw new InvalidOperationException("Only the host can tally results");

            var results = new List<IVoteStatus>();
            foreach (var role in RoleManager.Roles)
            {
                results.AddRange(role.GetVotes().Where(vote => vote != null && vote.IsRendered));
            }

            return results.AsReadOnly();
        }

        public void RenderResults()
        {
            var resultLists = TallyResults().GroupBy(vote => vote.Serialize())
                .Select(group => group.ToList()).ToList();

            foreach (var state in MeetingHud.Instance.playerStates)
                state.ClearForResults();

            var maxIndex = resultLists.Max(list => list.Count);
            for (int index = 0; index < maxIndex; index++)
            {
                foreach (var list in resultLists)
                {
                    if (list.Count <= index)
                    {
                        continue;
                    }

                    var vote = list[index];
                    AnimateVoteIcon(vote, index);
                    list.RemoveAt(index);
                }

                resultLists.RemoveAll(list => list.Count <= index);
            }
        }

        public void AnimateVoteIcon(IVoteStatus vote, int index)
        {
            var voteArea = MeetingHud.Instance.GetAllVoteAreas().Single(area => area.TargetPlayerId == vote.Serialize());
            var player = GameData.Instance.GetPlayerById(vote.Serialize());

            var voteIcon = GenerateVoteIcon(vote.IsAnonymous, player.ColorId, voteArea.transform);

            MeetingHud.Instance.StartCoroutine(Effects.Bloop(0.3f * index, voteIcon.transform));
            voteArea.transform.GetComponent<VoteSpreader>().AddVote(voteIcon);
        }

        private SpriteRenderer GenerateVoteIcon(bool isAnonymous, int? color, Transform parent)
        {
            var vote = UnityEngine.Object.Instantiate(MeetingHud.Instance.PlayerVotePrefab);

            if (isAnonymous)
            {
                PlayerControl.SetPlayerMaterialColors(Palette.DisabledGrey, vote);
            }
            else
            {
                PlayerControl.SetPlayerMaterialColors(color!.Value, vote);
            }

            vote.transform.SetParent(parent);
            vote.transform.localScale = Vector3.zero;
            return vote;
        }
    }
}