namespace TownOfUs.Roles
{
    // This could eventually be useful for translating, even though that is highly unlikely to ever be done.
    public class RoleInfo
    {
        public int RoleID { get; }
        public string Name { get; }

        public string InfectedDescription { get; }
        public string TaskDescription { get; }
    }
}