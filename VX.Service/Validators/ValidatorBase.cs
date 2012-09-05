using VX.Service.Infrastructure.Factories.Responses;

namespace VX.Service.Validators
{
    public abstract class ValidatorBase
    {
        protected readonly IResponsesFactory ResponsesFactory;

        protected ValidatorBase(IResponsesFactory responsesFactory)
        {
            ResponsesFactory = responsesFactory;
        }
    }
}