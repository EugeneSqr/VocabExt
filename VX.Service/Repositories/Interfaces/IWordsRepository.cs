using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface IWordsRepository
    {
        IWord GetById(int wordId);
    }
}
