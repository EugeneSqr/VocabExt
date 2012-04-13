using NHibernate;
using NHibernate.Cfg;
using VX.Domain.Interfaces;

namespace VX.Service
{
    // TODO: use autofac for injections
    // TODO: no static classes
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();

                    // TODO: define assembly with string
                    configuration.AddAssembly(typeof(ILanguage).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}