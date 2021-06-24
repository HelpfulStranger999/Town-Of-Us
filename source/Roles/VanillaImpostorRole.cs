using TownOfUs.Factions;

namespace TownOfUs.Roles
{
    public class VanillaImpostorRole : BaseRole
    {
        public VanillaImpostorRole() : base((int)RoleID.VanillaImpostor, ImpostorFaction.ID, Palette.ImpostorRed)
        {
        }

        public override bool CanKill(BaseRole role)
        {
            return role.FactionID != ImpostorFaction.ID;
        }
    }
}