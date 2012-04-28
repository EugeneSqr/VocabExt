namespace VX.Domain.DataContracts.Interfaces
{
    public interface ITranslation
    {
        IWord Source { get; set; }

        IWord Target { get; set; }
    }
}
