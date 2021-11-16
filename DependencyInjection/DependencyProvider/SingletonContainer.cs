using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.DependencyConfiguration.ImplementationData;

namespace DependencyInjection.DependencyProvider
{
    public class SingletonContainer
    {
        public readonly ImplNumber ImplNumber;

        public readonly object Instance;

        public SingletonContainer(object instance, ImplNumber number)
        {
            this.ImplNumber = number;
            this.Instance = instance;
        }
    }
}
