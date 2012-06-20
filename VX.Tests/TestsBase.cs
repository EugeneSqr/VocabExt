using Autofac;

namespace VX.Tests
{
    public abstract class TestsBase<TType, TImplementation>
    {
        protected IContainer Container { get; set; }

        protected ContainerBuilder ContainerBuilder { get; set; }

        protected TestsBase()
        {
            ContainerBuilder = new ContainerBuilder();

            ContainerBuilder.RegisterType<TImplementation>()
                .As<TType>()
                .InstancePerLifetimeScope();
        }

        protected void BuildContainer()
        {
            Container = ContainerBuilder.Build();
        }

        protected TType SystemUnderTest
        {
            get
            {
                return Container.Resolve<TType>();
            }
        }
    }
}
