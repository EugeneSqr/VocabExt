﻿using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface ILanguagesRepository
    {
        ILanguage GetLanguage(int languageId);
    }
}