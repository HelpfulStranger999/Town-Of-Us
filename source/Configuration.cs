namespace TownOfUs
{
    public class Configuration
    {
        // Temporary Singleton
        public static Configuration Instance { get; } = new Configuration();

        public int MaxNeutralRoles { get; set; } = 1;
        public int MaxImpostorRoles { get; set; } = 1;

        // Temporary setup
        public float LoversProbability { get; set; } = 0.0f;

        public float PhantomProbability { get; set; } = 0.0f;
    }
}