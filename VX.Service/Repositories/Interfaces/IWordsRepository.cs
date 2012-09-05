using System.Collections.Generic;
using VX.Domain.Entities;

namespace VX.Service.Repositories.Interfaces
{
    public interface IWordsRepository
    {
        IList<IWord> GetWords(string searchString);

        IWord GetWord(int id);

        bool CheckWordExists(string spelling);

        bool SaveWord(IWord word);
    }
}
