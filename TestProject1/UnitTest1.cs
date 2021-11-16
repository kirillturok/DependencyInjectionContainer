using NUnit.Framework;
using DependencyInjection.DependencyConfiguration;
using DependencyInjection.DependencyConfiguration.ImplementationData;

namespace TestProject1
{
    public class Tests
    {
        DependencyConfig dependencies;
        DependencyConfig dependencies1;

        [SetUp]
        public void Setup()
        {
            dependencies = new DependencyConfig();
            dependencies.Register<IInterface, Class>();
            dependencies.Register<IStrange, Strange>();

            dependencies1 = new DependencyConfig();
            dependencies1.Register<IInterface, Class>();
            dependencies1.Register<IInterface, Class2>();
            dependencies1.Register<IStrange, Strange>();
        }

        [Test]
        public void RegisteringDependencies()
        {
            bool v1 = dependencies.DependenciesDictionary.ContainsKey(typeof(IInterface));
            bool v2 = dependencies.DependenciesDictionary.ContainsKey(typeof(IStrange));
            int num = dependencies.DependenciesDictionary.Keys.Count;
            Assert.IsTrue(v1, "Dependency dictionary hasn't key IInterface.");
            Assert.IsTrue(v2, "Dependency dictionary hasn't key IStrange.");
            Assert.AreEqual(num, 2,"Dependency dictionary has another number of keys.");
        }

        [Test]
        public void RegisterDoubleDependency()
        {
            var containers = dependencies1.DependenciesDictionary[typeof(IInterface)];
            var type1 = containers[0].ImplementationsType;
            var type2 = containers[1].ImplementationsType;
            int num = dependencies1.DependenciesDictionary.Keys.Count;
            Assert.AreEqual(containers.Count, 2, "Wrong number of dependencies of IInterface.");
            Assert.AreEqual(type1, typeof(Class), "Another type of class Class in container.");
            Assert.AreEqual(type2, typeof(Class2), "Another type of class Class2 in container.");
            Assert.AreEqual(num, 2,"Dependency dictionary has another number of keys.");
        }
    }

    interface IInterface
    {
        void method1();

        void method2();
    }

    class Class : IInterface
    {

        public void method1()
        {
            throw new System.NotImplementedException();
        }

        public void method2()
        {
            throw new System.NotImplementedException();
        }
    }

    interface IStrange
    {
        void mth1();

        void mth2();
    }

    class Strange : IStrange
    {
        IInterface iInterface;

        public Strange(IInterface iInterface)
        {

        }

        public void mth1()
        {
            throw new System.NotImplementedException();
        }

        public void mth2()
        {
            throw new System.NotImplementedException();
        }
    }

    class Class2 : IInterface
    {
        public void method1()
        {
            throw new System.NotImplementedException();
        }

        public void method2()
        {
            throw new System.NotImplementedException();
        }
    }
}