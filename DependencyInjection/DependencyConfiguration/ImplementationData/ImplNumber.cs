using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.DependencyConfiguration.ImplementationData
{
    [Flags]
    public enum ImplNumber
    {
        None,
        First,
        Second,
        Any = None | First | Second,
    }
}
