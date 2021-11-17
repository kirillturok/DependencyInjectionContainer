using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.DependencyConfiguration.ImplementationData;

namespace DependencyInjection.DependencyConfiguration
{
    public class DependencyConfig
    {
        public Dictionary<Type, List<ImplContainer>> DependenciesDictionary { get; private set; }

        public DependencyConfig()
        {
            DependenciesDictionary = new Dictionary<Type, List<ImplContainer>>();
        }

        public void Register<TDependency, TImplementation>(LifeCycle ttl = LifeCycle.InstancePerDependency,ImplNumber number = ImplNumber.None) 
            where TDependency : class 
            where TImplementation : TDependency
        {
            Register(typeof(TDependency), typeof(TImplementation), ttl, number);
        }

        public void Register(Type dependencyType, Type implementType, LifeCycle ttl, ImplNumber number)
        {
            if (!IsDependency(implementType, dependencyType))
            {
                throw new ArgumentException("Incompatible parameters");
            }

            var implContainer = new ImplContainer(implementType, ttl, number);
            if (this.DependenciesDictionary.ContainsKey(dependencyType))
            {
                var index = this.DependenciesDictionary[dependencyType]
                    .FindIndex(elem => elem.ImplementationsType == implContainer.ImplementationsType);
                if (index != -1)
                {
                    this.DependenciesDictionary[dependencyType].RemoveAt(index);
                }

                this.DependenciesDictionary[dependencyType].Add(implContainer);

            }
            else
            {
                this.DependenciesDictionary.Add(dependencyType, new List<ImplContainer>() { implContainer });
            }
        }

        private bool IsDependency(Type implementation, Type dependency)
        {
            return implementation.IsAssignableFrom(dependency) || implementation.GetInterfaces().Any(i => i.ToString() == dependency.ToString());
        }
    }
}
