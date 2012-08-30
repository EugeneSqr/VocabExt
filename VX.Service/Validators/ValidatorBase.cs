using VX.Service.Infrastructure.Factories;

namespace VX.Service.Validators
{
    public abstract class ValidatorBase
    {
        protected readonly IAbstractFactory Factory;

        protected ValidatorBase(IAbstractFactory factory)
        {
            Factory = factory;
        }
    }
}