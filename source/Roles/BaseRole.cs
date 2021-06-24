using System;
using System.Collections.Generic;
using UnityEngine;

namespace TownOfUs.Roles
{
    public abstract class BaseRole
    {
        public int ID { get; protected set; }
        public int FactionID { get; protected set; }

        public Color Color { get; }

        public bool HasVoted => CurrentVote != null && !(CurrentVote is HasNotVotedStatus);

        public PlayerControl? Player { get; protected set; }
        public IVoteStatus? CurrentVote { get; protected set; }

        protected BaseRole(int roleID, int factionID, Color color)
        {
            ID = roleID;
            FactionID = factionID;
            Color = color;
        }

        public virtual void AssignPlayer(PlayerControl player)
        {
            if (player != null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            Player = player;
        }

        public virtual bool CanVote()
        {
            return Player != null && !Player.Data.IsDead && !Player.Data.Disconnected && !HasVoted;
        }

        public virtual bool TryCastVote(IVoteStatus vote)
        {
            if (!CanVote()) return false;

            CurrentVote = vote;
            return true;
        }

        public virtual IEnumerable<IVoteStatus> GetVotes()
        {
            yield return CurrentVote;
        }

        public abstract bool CanKill(BaseRole role);
    }

    public enum RoleID : int
    {
        VanillaCrewmate = 1,
        VanillaImpostor = -1,
        Mayor = 2
    }
}