namespace VX.Domain.DataContracts.Interfaces
{
    public interface ITranslation
    {
        int Id { get; set; }

        IWord Source { get; set; }

        IWord Target { get; set; }
    }
}
