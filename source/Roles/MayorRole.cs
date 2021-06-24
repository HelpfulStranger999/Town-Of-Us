using System.Collections.Generic;
using TownOfUs.Events;
using TownOfUs.Services;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class MayorRole : CrewmateRole
    {
        // Equivalent to PlayerVoteArea.MissedVote
        public const byte AbstainPlayerId = 254;

        public bool HasAbstained => CurrentVote is AbstainVoteStatus;
        public int VoteBank { get; private set; }
        public List<IVoteStatus> ExtraVotes { get; } = new List<IVoteStatus>();

        private VoteService VoteService { get; }
        private PlayerVoteArea? AbstainButton { get; set; }

        public MayorRole(VoteService voteService) : base((int)RoleID.Mayor, new Color(0.44f, 0.31f, 0.66f, 1f))
        {
            VoteService = voteService;
        }

        private void GenerateAbstainButton(MeetingHud instance)
        {
            AbstainButton = Object.Instantiate(instance.SkipVoteButton,
                instance.SkipVoteButton.transform.parent);

            AbstainButton.Parent = instance;
            AbstainButton.SetTargetPlayerId(AbstainPlayerId);

            AbstainButton.transform.localPosition = instance.SkipVoteButton.transform.localPosition;
            AbstainButton.transform.localPosition += new Vector3(0f, -0.17f, 0f);

            instance.SkipVoteButton.transform.localPosition += new Vector3(0f, 0.20f, 0f);

            // Temp solution for now
            var spriteRender = AbstainButton.GetComponent<SpriteRenderer>();
            spriteRender.sprite = TownOfUs.Abstain; // This is a bad approach, but I'll deal with sprites later.
            spriteRender.enabled = true;
        }

        public override bool CanVote()
        {
            if (base.CanVote()) return true;
            if (HasVoted)
            {
                if (HasAbstained) return false;
                else return VoteBank > 0;
            }

            // This should never be reached.
            return false;
        }

        public override bool TryCastVote(IVoteStatus vote)
        {
            if (vote == null) return false;
            if (!CanVote()) return false;

            if (vote is AbstainVoteStatus)
            {
                VoteBank++;
            }
            else
            {
                ExtraVotes.Add(vote);
                VoteBank--;
            }

            return true;
        }

        public override IEnumerable<IVoteStatus> GetVotes()
        {
            if (CurrentVote != null)
                yield return CurrentVote;
            foreach (var extraVote in ExtraVotes)
                yield return extraVote;
        }

        public override void AssignPlayer(PlayerControl player)
        {
            base.AssignPlayer(player);
            if (player.PlayerId != PlayerControl.LocalPlayer.PlayerId)
                return;

            VoteService.MeetingStarted += (object sender, MeetingStartEventArgs args) =>
            {
                if (player.Data.IsDead) return;

                GenerateAbstainButton(args.Instance);
                AbstainButton!.SetDisabled();
            };

            VoteService.VoteCleared += (object sender, VoteClearEventArgs args) =>
            {
                AbstainButton!.voteComplete = false;
                AbstainButton.gameObject.SetActive(true);
            };

            VoteService.VoteConfirmed += (object sender, VoteConfirmEventArgs args) =>
            {
                AbstainButton!.ClearButtons();
                AbstainButton.voteComplete = true;
                AbstainButton.gameObject.SetActive(false);
            };

            VoteService.VoteSelected += (object sender, VoteSelectEventArgs args) =>
            {
                if (args.SuspectStateId != AbstainButton!.TargetPlayerId)
                    AbstainButton.ClearButtons();
            };

            VoteService.VotingCompleted += (object sender, VotingCompleteEventArgs args) =>
            {
                AbstainButton!.gameObject.SetActive(false);
            };

            VoteService.MeetingEnded += (object sender, MeetingEndEventArgs args) =>
            {
                Object.Destroy(AbstainButton);
                AbstainButton = null;
            };
        }
    }
}