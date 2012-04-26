using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Interfaces.Repositories
{
    public interface IWordsRepository
    {
        IWord GetById(int wordId);
    }
}
