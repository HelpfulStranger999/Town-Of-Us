using Hazel;
using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Jester : BaseRole
    {
        public bool VotedOut;


        public Jester(PlayerControl player) : base(player)
        {
            Name = "Jester";
            ImpostorText = () => "Get voted out";
            TaskText = () => "Get voted out!\nFake Tasks:";
            Color = new Color(1f, 0.75f, 0.8f, 1f);
            RoleType = RoleEnum.Jester;
            Faction = Faction.Neutral;
        }

        protected override void IntroPrefix(IntroCutscene._CoBegin_d__14 __instance)
        {
            var jesterTeam = new List<PlayerControl>();
            jesterTeam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = jesterTeam;
        }

        internal override bool HasNotReachedEndCondition(ShipStatus __instance)
        {
            if (!VotedOut || !Player.Data.IsDead && !Player.Data.Disconnected) return true;
            Utils.EndGame();
            return false;
        }

        public void Wins()
        {
            //System.Console.WriteLine("Reached Here - Jester edition");
            VotedOut = true;
        }

        public void Loses()
        {
            Player.Data.IsImpostor = true;
        }

        public override void SendSetRpc()
        {
            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte)CustomRPC.SetJester, SendOption.Reliable, -1);
            writer.Write(Player.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}