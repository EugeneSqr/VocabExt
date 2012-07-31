namespace VX.Domain
{
    public interface IServiceOperationResponse
    {
        string ErrorMessage { get; set; }

        string StatusMessage { get; set; }

        int OperationActionCode { get; }

        bool Status { get; }
    }
}
