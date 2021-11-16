using System;

namespace DependencyInjection.DependencyConfiguration.ImplementationData
{
    public class ImplContainer
    {
        public Type ImplementationsType { get; }
        public LifeCycle TimeToLive { get; }
        public ImplNumber ImplNumber { get; }

        public ImplContainer(Type implementationsType, LifeCycle timeToLive, ImplNumber implNumber)
        {
            this.ImplNumber = implNumber;
            this.ImplementationsType = implementationsType;
            this.TimeToLive = timeToLive;
        }
    }
}
