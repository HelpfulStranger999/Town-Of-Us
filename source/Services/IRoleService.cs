using System;
using System.Collections.Generic;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public interface IRoleService
    {
        void AssignRoles(IEnumerable<PlayerControl> players, IEnumerable<RoleAssignmentConfiguration> configurations);
        BaseRole AssignRole(Type role, PlayerControl player);

        void Reset();

        IRoleCollection GetRoles();
    }
}