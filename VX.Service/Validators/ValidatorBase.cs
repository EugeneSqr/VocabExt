using VX.Service.Infrastructure.Factories.ServiceOperationResponses;

namespace VX.Service.Validators
{
    public abstract class ValidatorBase
    {
        protected readonly IServiceOperationResponseFactory ServiceOperationResponseFactory;

        protected ValidatorBase(IServiceOperationResponseFactory serviceOperationResponseFactory)
        {
            ServiceOperationResponseFactory = serviceOperationResponseFactory;
        }
    }
}