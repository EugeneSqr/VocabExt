using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VX.Domain.Interfaces
{
    public interface IWord
    {
        string Spelling { get; set; }

        string Transcription { get; set; }

        ILanguage Language { get; set; }
    }
}
