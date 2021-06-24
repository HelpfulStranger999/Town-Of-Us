using Hazel;
using TownOfUs.Roles;

namespace TownOfUs.RPC
{
    public class AbstainRPC : CustomRPC
    {
        public AbstainRPC(MayorRole mayor) : base(mayor, 123)
        {
        }

        protected override void WritePacket(ref MessageWriter writer)
        {
            // We don't need to write anything.
        }
    }
}