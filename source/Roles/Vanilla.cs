using UnityEngine;

namespace TownOfUs.Roles
{
    public class Impostor : BaseRole
    {
        public Impostor(PlayerControl player) : base(player)
        {
            Name = "Impostor";
            Hidden = true;
            Faction = Faction.Impostors;
            RoleType = RoleEnum.Impostor;
            Color = Palette.ImpostorRed;
        }
    }

    public class Crewmate : BaseRole
    {
        public Crewmate(PlayerControl player) : base(player)
        {
            Name = "Crewmate";
            Hidden = true;
            Faction = Faction.Crewmates;
            RoleType = RoleEnum.Crewmate;
            Color = Color.white;
        }
    }
}