using VX.Model;

namespace VX.Service.Infrastructure.Factories.EntitiesContext
{
    public interface IContextFactory
    {
        Entities Build();
    }
}