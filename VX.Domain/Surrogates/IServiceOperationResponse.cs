namespace VX.Domain.Surrogates
{
    public interface IServiceOperationResponse
    {
        string ErrorMessage { get; set; }

        string StatusMessage { get; set; }

        int OperationActionCode { get; }

        bool Status { get; }
    }
}
