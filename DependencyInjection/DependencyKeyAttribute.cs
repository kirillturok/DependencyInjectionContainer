using System;
using DependencyInjection.DependencyConfiguration.ImplementationData;

namespace DependencyInjection
{
    public class DependencyKeyAttribute : Attribute
    {
        public ImplNumber ImplNumber { get; }

        public DependencyKeyAttribute(ImplNumber number)
        {
            this.ImplNumber = number;
        }
    }
}
