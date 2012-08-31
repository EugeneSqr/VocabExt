using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;

namespace VX.Service
{
    public class ServiceHostFactory : AutofacHostFactory
    {
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<VocabExtService>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(service => service.GetCustomAttributes(typeof(RegisterServiceAttribute), false).Any())
                .AsImplementedInterfaces();

            Container = builder.Build();

            return base.CreateServiceHost(constructorString, baseAddresses);
        }

        protected override ServiceHost CreateSingletonServiceHost(object singletonInstance, Uri[] baseAddresses)
        {
            if (singletonInstance == null)
            {
                throw new ArgumentNullException("singletonInstance");
            }
            if (baseAddresses == null)
            {
                throw new ArgumentNullException("baseAddresses");
            }
            return new ServiceHost(singletonInstance, baseAddresses);
        }
    }
}