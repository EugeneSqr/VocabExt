using VX.Model;

namespace VX.Service.Infrastructure.Factories.Context
{
    public interface IContextFactory
    {
        EntitiesContext Build();
    }
}