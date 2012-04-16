namespace VX.Domain.Interfaces
{
    public interface IWordsRepository
    {
        IWord GetById(int wordId);
    }
}
