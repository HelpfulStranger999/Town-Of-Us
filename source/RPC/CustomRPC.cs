using Hazel;
using TownOfUs.Exceptions;
using TownOfUs.Roles;

namespace TownOfUs.RPC
{
    public abstract class CustomRPC
    {
        public uint NetworkID { get; }
        public byte PacketID { get; }
        public bool IsReliable { get; protected set; } = true;

        protected CustomRPC(uint netID, byte packetID)
        {
            NetworkID = netID;
            PacketID = packetID;
        }

        protected CustomRPC(BaseRole role, byte packetID)
        {
            Guard.Against.UnassignedRole(role);
            NetworkID = role.Player!.PlayerId;
            PacketID = packetID;
        }

        protected abstract void WritePacket(ref MessageWriter writer);

        public void Send()
        {
            var writer = AmongUsClient.Instance.StartRpc(NetworkID, PacketID,
                IsReliable ? SendOption.Reliable : SendOption.None);
            WritePacket(ref writer);
            writer.EndMessage();
        }

        public void SendImmediately()
        {
            var writer = AmongUsClient.Instance.StartRpcImmediately(NetworkID, PacketID,
                IsReliable ? SendOption.Reliable : SendOption.None);
            WritePacket(ref writer);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}