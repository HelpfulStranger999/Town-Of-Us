namespace TownOfUs.Roles
{
    public class Underdog : BaseRole
    {
        public Underdog(PlayerControl player) : base(player)
        {
            Name = "Underdog";
            ImpostorText = () => "Use your comeback power to win";
            TaskText = () => "long kill cooldown when 2 imps, short when 1 imp";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Underdog;
            Faction = Faction.Impostors;
        }
    }
}