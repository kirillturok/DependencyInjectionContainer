using System;

namespace DependencyInjectionContainer
{
    public class Dependency
    {
        public Type Type { get; }

        public LifeCycle LifeType { get; }

        public object Key { get; }

        public object Instance { get; set; }

        public Dependency(Type type, LifeCycle lifeType, object key)
        {
            Key = key;
            Type = type;
            LifeType = lifeType;
        }
    }
}
