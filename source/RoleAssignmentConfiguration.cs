using System;

namespace TownOfUs
{
    public class RoleAssignmentConfiguration
    {
        public float Probability { get; }
        public Type RoleType { get; }
        public Faction Faction { get; }

        public RoleAssignmentConfiguration(float probability, Type roleType, Faction faction)
        {
            Probability = probability;
            RoleType = roleType;
            Faction = faction;
        }
    }
}