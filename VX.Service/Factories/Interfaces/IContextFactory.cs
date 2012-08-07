using VX.Model;

namespace VX.Service.Factories.Interfaces
{
    public interface IContextFactory
    {
        Entities Build();
    }
}