using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TownOfUs.Roles;

namespace TownOfUs.Services
{
    public class ReflectionRoleFactory : IRoleFactory
    {
        public TRole CreateRole<TRole>(IEnumerable<object> possibleDependencies) where TRole : BaseRole
            => CreateRole(typeof(TRole), possibleDependencies) as TRole;

        public BaseRole CreateRole(Type roleType, IEnumerable<object> possibleDependencies)
        {
            var constructors = roleType.GetConstructors();
            var dependencies = possibleDependencies.ToList();
            var dependencyTypes = possibleDependencies.Select(dependency => dependency.GetType()).ToList();

            ConstructorInfo validConstructor = null;
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().All(parameter =>
                {
                    return dependencyTypes.Contains(parameter.ParameterType) || parameter.IsOptional;
                }))
                {
                    validConstructor = constructor;
                    break;
                }
            }

            if (validConstructor == null)
            {
                throw new MissingMethodException($"Could not find a valid public constructor for type {roleType.Name}");
            }

            var parameters = new List<object>();
            foreach (var parameter in validConstructor.GetParameters())
            {
                var loadedDependency = dependencies.FirstOrDefault(dependency => parameter.ParameterType == dependencies.GetType();
                if (loadedDependency == null) loadedDependency = parameter.DefaultValue;
                parameters.Add(loadedDependency);
            }

            var role = validConstructor.Invoke(parameters.ToArray());
            return role as BaseRole;
        }
    }
}