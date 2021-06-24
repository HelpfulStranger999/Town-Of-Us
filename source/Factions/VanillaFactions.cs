namespace TownOfUs.Factions
{
    public class CrewmateFaction : IFaction
    {
        public const int ID = 0;
        public int FactionID => ID;
    }

    public class ImpostorFaction : IFaction
    {
        public const int ID = 1;
        public int FactionID => ID;
    }
}