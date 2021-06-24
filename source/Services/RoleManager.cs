using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public class RoleManager
    {
        public ReadOnlyCollection<BaseRole> Roles => ActiveRoles.AsReadOnly();
        private List<BaseRole> ActiveRoles { get; } = new List<BaseRole>();

        public void AssignRoles(GameConfiguration config)
        {
            config.RoleConfigurations()
        }

        public TRole GetRole<TRole>() where TRole : BaseRole
            => (ActiveRoles.Single(role => role is TRole) as TRole)!;

        public BaseRole GetRoleByPlayer<TRole>(byte playerID)
            => ActiveRoles.Where(role => role is TRole)
                    .Single(role => role.Player != null &&
                                    role.Player.PlayerId == playerID);

        public IReadOnlyCollection<TRole> GetRolesByPlayers<TRole>(IEnumerable<byte> playerIDs) where TRole : BaseRole
        {
            var ids = playerIDs.ToHashSet();
            return ActiveRoles.Where(role => role is TRole &&
                                             role.Player != null &&
                                             ids.Contains(role.Player.PlayerId))
                              .Select(role => (role as TRole)!)
                              .ToList().AsReadOnly();
        }
    }
}