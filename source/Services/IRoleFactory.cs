using System;
using System.Collections.Generic;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public interface IRoleFactory
    {
        public BaseRole CreateRole(Type roleType, IEnumerable<object> possibleDependencies);
        public TRole CreateRole<TRole>(IEnumerable<object> possibleDependencies) where TRole : BaseRole;
    }
}