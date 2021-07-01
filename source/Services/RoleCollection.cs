using System.Collections.Generic;
using System.Linq;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public class RoleCollection : IRoleCollection
    {
        private Dictionary<byte, BaseRole> PlayerRoleBindings { get; } = new Dictionary<byte, BaseRole>();
        private Dictionary<int, BaseRole> RoleIdBindings { get; } = new Dictionary<int, BaseRole>();

        public bool TryGetRole<TRole>(out TRole role) where TRole : BaseRole
        {
            role = RoleIdBindings.SingleOrDefault(role => role is TRole) as TRole;
            return role == null;
        }

        public TRole GetRole<TRole>() where TRole : BaseRole
        {
            if (TryGetRole<TRole>(out var role))
                return role;
            throw new KeyNotFoundException($"No role of type {typeof(TRole).Name} was found");
        }

        public bool TryGetRole(int id, out BaseRole role)
            => RoleIdBindings.TryGetValue(id, out role);

        public BaseRole GetRole(int id)
        {
            if (TryGetRole(id, out var role))
                return role;

            throw new KeyNotFoundException($"No role of id {id} was found");
        }

        public bool TryGetRoleOfPlayer(byte playerId, out BaseRole role)
            => PlayerRoleBindings.TryGetValue(playerId, out role);

        public BaseRole GetRoleOfPlayer(byte playerId)
        {
            if (TryGetRoleOfPlayer(playerId, out BaseRole role))
                return role;

            throw new KeyNotFoundException($"Player with id {playerId} does not have a role assigned");
        }

        public bool TryGetRoleOfPlayer<TRole>(byte playerId, out TRole role) where TRole : BaseRole
        {
            if (TryGetRoleOfPlayer(playerId, out BaseRole baseRole))
            {
                role = baseRole as TRole;
                return true;
            }

            role = null;
            return false;
        }

        public TRole GetRoleOfPlayer<TRole>(byte playerId) where TRole : BaseRole
            => GetRoleOfPlayer(playerId) as TRole;

        public bool AddRole(byte playerId, BaseRole role)
        {
            return PlayerRoleBindings.TryAdd(playerId, role) && RoleIdBindings.TryAdd(role.RoleId, role);
        }

        public void Clear()
        {
            RoleIdBindings.Clear();
            PlayerRoleBindings.Clear();
        }

        public IEnumerable<TRole> GetRoles<TRole>() where TRole : BaseRole
        {
            return RoleIdBindings.Values.Where(role => role is TRole).Cast<TRole>();
        }

        public IEnumerable<BaseRole> GetRoles()
        {
            return RoleIdBindings.Values;
        }

        public void RemoveRole(int roleId)
        {
            if (RoleIdBindings.TryGetValue(roleId, out var role))
                PlayerRoleBindings.Remove(role.Player.PlayerId);
            RoleIdBindings.Remove(roleId);
        }

        public void RemovePlayer(byte playerId)
        {
            if (PlayerRoleBindings.TryGetValue(playerId, out var role))
                RoleIdBindings.Remove(role.RoleId);
            PlayerRoleBindings.Remove(playerId);
        }
    }
}