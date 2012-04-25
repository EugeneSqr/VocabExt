namespace VX.Domain.Interfaces
{
    public interface ITranslation
    {
        IWord Source { get; set; }

        IWord Target { get; set; }
    }
}
