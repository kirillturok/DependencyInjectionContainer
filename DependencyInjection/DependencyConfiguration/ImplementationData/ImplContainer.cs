using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
