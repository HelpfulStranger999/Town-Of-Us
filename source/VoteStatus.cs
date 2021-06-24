using System;
using TownOfUs.Roles;

namespace TownOfUs
{
    public interface IVoteStatus
    {
        public bool IsRendered { get; }
        public bool IsAnonymous { get; }

        public byte Serialize();
    }

    public class PlayerVoteStatus : IVoteStatus
    {
        public PlayerControl Player { get; }

        public bool IsRendered => true;
        public bool IsAnonymous { get; } = false;

        public PlayerVoteStatus(PlayerControl player, bool isAnonymous = false)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            IsAnonymous = isAnonymous;
        }

        public byte Serialize()
        {
            return Player.PlayerId;
        }
    }

    public class SkipVoteStatus : IVoteStatus
    {
        public bool IsRendered => true;
        public bool IsAnonymous { get; } = false;

        public SkipVoteStatus(bool isAnonymous = false)
        {
            IsAnonymous = isAnonymous;
        }

        public byte Serialize()
        {
            return PlayerVoteArea.SkippedVote;
        }
    }

    public class AbstainVoteStatus : IVoteStatus
    {
        public bool IsRendered => false;
        public bool IsAnonymous => false;

        public byte Serialize()
        {
            return MayorRole.AbstainPlayerId;
        }
    }

    public class HasNotVotedStatus : IVoteStatus
    {
        public bool IsRendered => false;
        public bool IsAnonymous => false;

        public byte Serialize()
        {
            return PlayerVoteArea.HasNotVoted;
        }
    }
}