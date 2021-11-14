using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionContainer
{
    public class DependencyConfiguration
    {
        private readonly Dictionary<Type, List<Dependency>> _dependencies = new Dictionary<Type, List<Dependency>>();

        private readonly List<Type> _excludedTypes = new List<Type>();

        internal void ExcludeType(Type type)
        {
            _excludedTypes.Add(type);
        }

        internal void RemoveFromExcluded(Type type)
        {
            _excludedTypes.Remove(type);
        }

        internal bool IsExcluded(Type type)
        {
            return _excludedTypes.Contains(type);
        }

        public void Register<TInterface, TImplementation>(string name)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Register(typeof(TInterface), typeof(TImplementation), LifeCycle.InstancePerDependency, name);
        }

        public void Register<TInterface, TImplementation>(LifeCycle lifeCycle, object name)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Register(typeof(TInterface), typeof(TImplementation), lifeCycle, name);
        }

        public void Register(Type @interface, Type implementation, LifeCycle lifeType = LifeCycle.InstancePerDependency, object name = null)
        {
            if (@interface == null) throw new ArgumentNullException(nameof(@interface));
            if (implementation == null) throw new ArgumentNullException(nameof(implementation));
            if (!implementation.IsClass) throw new ArgumentException($"{implementation} must be a reference type");
            if (implementation.IsAbstract || implementation.IsInterface)
                throw new ArgumentException($"{implementation} must be non abstract");
            if (@interface.IsAssignableFrom(implementation) ||
                implementation.IsGenericTypeDefinition && @interface.IsGenericTypeDefinition &&
                 IsAssignableFromGeneric(implementation, @interface))
            {
                var dependency = new Dependency(implementation, lifeType, name);
                if (_dependencies.ContainsKey(@interface))
                {
                    _dependencies[@interface].Add(dependency);
                }
                else
                {
                    _dependencies.Add(@interface, new List<Dependency> { dependency });
                }
            }
            else
            {
                throw new ArgumentException($"{implementation} must be non abstract and must subtype of {@interface}");
            }
        }

        private IEnumerable<Type> GetBaseTypes(Type type)
        {
            for (var baseType = type; baseType != null; baseType = baseType.BaseType)
                yield return baseType;

            var interfaceTypes =
                from Type interfaceType in type.GetInterfaces()
                select interfaceType;

            foreach (var interfaceType in interfaceTypes)
                yield return interfaceType;
        }

        private Type GetTypeDefinition(Type type) =>
            type.IsGenericType ? type.GetGenericTypeDefinition() : type;

        private bool IsAssignableFromGeneric(Type implType, Type interfaceType)
        {
            var baseTypes = GetBaseTypes(GetTypeDefinition(implType));
            return baseTypes
                .Select(GetTypeDefinition)
                .Contains(GetTypeDefinition(interfaceType));
        }

        public void Register<TInterface, TImplementation>(LifeCycle lifeType = LifeCycle.InstancePerDependency)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Register(typeof(TInterface), typeof(TImplementation), lifeType);
        }

        public void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Register(typeof(TInterface), typeof(TImplementation), LifeCycle.Singleton);
        }

        public bool TryGet(Type @interface, out Dependency dependency)
        {
            if (_dependencies.TryGetValue(@interface, out var dependencies))
            {
                dependency = dependencies.First();
                return true;
            }

            dependency = null;
            return false;
        }

        public bool TryGetAll(Type @interface, out IEnumerable<Dependency> dependencies)
        {
            var isFound = _dependencies.TryGetValue(@interface, out var allDependencies);
            dependencies = allDependencies;
            return isFound;
        }
    }
}
