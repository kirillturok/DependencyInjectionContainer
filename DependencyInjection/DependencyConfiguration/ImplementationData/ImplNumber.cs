using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.DependencyConfiguration.ImplementationData
{
    public enum ImplNumber
    {
        None = 1,
        First = 2,
        Second = 4,
        Any = None | First | Second,
    }
}
