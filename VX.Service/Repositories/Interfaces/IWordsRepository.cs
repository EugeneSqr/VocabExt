using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface IWordsRepository
    {
        IList<IWord> GetWords(string searchString);

        IWord GetWord(int id);
    }
}
