using NHibernate;
using VX.Domain.Interfaces;

namespace VX.Service
{
    public class LanguageRepository : ILanguageRepository
    {
        public void Add(ILanguage language)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(language);
                    transaction.Commit();
                }
            }
        }

        public void Update(ILanguage language)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ILanguage language)
        {
            throw new System.NotImplementedException();
        }

        public ILanguage GetById(int languageId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<ILanguage>(languageId);
            }
        }
    }
}