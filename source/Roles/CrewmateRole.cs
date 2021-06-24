using TownOfUs.Factions;
using UnityEngine;

namespace TownOfUs.Roles
{
    public abstract class CrewmateRole : BaseRole
    {
        protected CrewmateRole(int roleID, Color color)
            : base(roleID, CrewmateFaction.ID, color)
        {
        }

        public override bool CanKill(BaseRole role)
        {
            return false;
        }
    }
}