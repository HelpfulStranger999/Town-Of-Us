using System.Collections.ObjectModel;

namespace TownOfUs
{
    public class GameConfiguration
    {
        public ReadOnlyCollection<IRoleConfiguration> RoleConfigurations { get; }
        public MayorConfiguration Mayor { get; } = new MayorConfiguration();
    }

    public interface IRoleConfiguration
    {
        public Percentage SpawnRate { get; }
    }

    public class MayorConfiguration : IRoleConfiguration
    {
        public Percentage SpawnRate { get; set; } = (Percentage)0.0f;
        public int InitialVoteBankSize { get; set; } = 1;
        public bool ExtraVotesAnonymous { get; set; } = false;
    }
}