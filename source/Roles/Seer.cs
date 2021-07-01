using Hazel;
using System;
using System.Collections.Generic;
using TownOfUs.CrewmateRoles.SeerMod;
using TownOfUs.Services;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Seer : BaseRole
    {
        public List<byte> Investigated = new List<byte>();

        public Seer(PlayerControl player) : base(player)
        {
            Name = "Seer";
            ImpostorText = () => "Investigate roles";
            TaskText = () => "Investigate roles and find the Impostor";
            Color = new Color(1f, 0.8f, 0.5f, 1f);
            RoleType = RoleEnum.Seer;
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastInvestigated { get; set; }

        public float SeerTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastInvestigated;
            var num = CustomGameOptions.SeerCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public bool CheckSeeReveal(PlayerControl player)
        {
            var role = RoleService.Instance.GetRoles().GetRoleOfPlayer(player);
            return CustomGameOptions.SeeReveal switch
            {
                SeeReveal.All => true,
                SeeReveal.Nobody => false,
                SeeReveal.ImpsAndNeut => role != null && role.Faction != Faction.Crewmates || player.Data.IsImpostor,
                SeeReveal.Crew => role != null && role.Faction == Faction.Crewmates || !player.Data.IsImpostor,
                _ => false,
            };
        }

        public override void SendSetRpc()
        {
            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte)CustomRPC.SetSeer, SendOption.Reliable, -1);
            writer.Write(Player.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}