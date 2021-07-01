using System.Collections.Generic;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public interface IRoleCollection
    {
        bool TryGetRole<TRole>(out TRole role) where TRole : BaseRole;
        bool TryGetRole(int id, out BaseRole role);

        TRole GetRole<TRole>() where TRole : BaseRole;
        BaseRole GetRole(int id);

        IEnumerable<BaseRole> GetRoles();
        IEnumerable<TRole> GetRoles<TRole>() where TRole : BaseRole;

        bool TryGetRoleOfPlayer(byte playerId, out BaseRole role);
        bool TryGetRoleOfPlayer(PlayerControl player, out BaseRole role)
            => TryGetRoleOfPlayer(player.PlayerId, out role);

        BaseRole GetRoleOfPlayer(byte playerId);
        BaseRole GetRoleOfPlayer(PlayerControl player)
            => GetRoleOfPlayer(player.PlayerId);

        bool TryGetRoleOfPlayer<TRole>(byte playerId, out TRole role) where TRole : BaseRole;
        bool TryGetRoleOfPlayer<TRole>(PlayerControl player, out TRole role) where TRole : BaseRole
            => TryGetRoleOfPlayer<TRole>(player.PlayerId, out role);

        TRole GetRoleOfPlayer<TRole>(byte playerId) where TRole : BaseRole;
        TRole GetRoleOfPlayer<TRole>(PlayerControl player) where TRole : BaseRole
            => GetRoleOfPlayer<TRole>(player.PlayerId);

        bool AddRole(byte playerId, BaseRole role);
        void RemoveRole(int roleId);
        void RemovePlayer(byte playerId);
        void Clear();
    }
}