using Hazel;
using System.Linq;
using TownOfUs.Exceptions;
using TownOfUs.Roles;

namespace TownOfUs.RPC
{
    public class ExtraVotesRPC : CustomRPC
    {
        public const byte ID = 0;

        private MayorRole Mayor { get; }

        public ExtraVotesRPC(MayorRole mayor) : base(PlayerControl.LocalPlayer.NetId, ID)
        {
            Guard.Against.UnassignedRole(mayor);
            Mayor = mayor;
        }

        protected override void WritePacket(ref MessageWriter writer)
        {
            writer.Write(Mayor.Player!.PlayerId);
            writer.WriteBytesAndSize(Mayor.ExtraVotes.Select(player => player.PlayerId).ToArray());
        }
    }
}