using System.Collections.Generic;
using VX.Domain;
using VX.Domain.Entities;
using VX.Domain.Surrogates;

namespace VX.Service.Repositories.Interfaces
{
    public interface IWordsRepository
    {
        IList<IWord> GetWords(string searchString);

        IWord GetWord(int id);

        bool CheckWordExists(string spelling);

        IServiceOperationResponse SaveWord(IWord word);
    }
}
