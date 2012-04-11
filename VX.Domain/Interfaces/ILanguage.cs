using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VX.Domain.Interfaces
{
    public interface ILanguage
    {
        string Name { get; set; }

        string Abbreviation { get; set; }
    }
}
