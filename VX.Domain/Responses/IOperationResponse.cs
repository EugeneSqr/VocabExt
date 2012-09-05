namespace VX.Domain.Responses
{
    public interface IOperationResponse
    {
        string ErrorMessage { get; set; }

        string StatusMessage { get; set; }

        int OperationActionCode { get; }

        bool Status { get; }
    }
}
