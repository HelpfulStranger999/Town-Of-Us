using System;
using TownOfUs.Roles;

namespace TownOfUs.Exceptions
{
    public partial class Guard
    {
        public partial class Against
        {
            public static void UnassignedRole(BaseRole role)
            {
                if (role.Player == null)
                {
                    // TODO Custom exception?
                    throw new InvalidOperationException($"{role.LocalizedInfo.Name} has not been assigned to a player.");
                }
            }
        }
    }
}