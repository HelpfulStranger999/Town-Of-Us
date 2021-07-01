using System.Collections.Generic;

namespace TownOfUs.Services
{
    public interface IRoleService
    {
        void AssignRoles(IEnumerable<GameData.PlayerInfo> players, IEnumerable<RoleAssignmentConfiguration> configurations);
        void AssignRole(RoleAssignmentConfiguration role, GameData.PlayerInfo player);

        void Reset();

        IRoleCollection GetRoles();
    }
}