using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Service.Interfaces
{
    internal interface IAnswersPicker
    {
        IList<IWord> PickAnswers();
    }
}