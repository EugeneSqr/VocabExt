using System;
using VX.Domain.Entities;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories.Responses;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    [RegisterService]
    public class WordValidator : ValidatorBase, IWordValidator
    {
        private readonly ILanguagesRepository languagesRepository;

        private const string EmptyWordErrorCode = "0";
        private const string EmptySpellingErrorCode = "1";
        private const string WrongLanguageErrorCode = "2";
        private const string WordAlreadyExistsErrorCode = "3";

        public WordValidator(
            IResponsesFactory responsesFactory, 
            ILanguagesRepository languagesRepository) : base(responsesFactory)
        {
            this.languagesRepository = languagesRepository;
        }

        public IOperationResponse ValidateExist(IWord word, IWordsRepository wordsRepository)
        {
            Func<IWord, IOperationResponse> validationFunction =
                parameter =>
                    {
                        var retrievedWord = wordsRepository.GetWord(parameter.Id);
                        if (retrievedWord == null || retrievedWord.Id == default(int))
                        {
                            return wordsRepository.CheckWordExists(parameter.Spelling)
                                       ? BuildWordExistsResponse()
                                       : BuildValidationPassedResponse();
                        }

                        return BuildWordExistsResponse();
                    };

            return PerformValidation(validationFunction, word);
        }

        public IOperationResponse ValidateSpelling(IWord word)
        {
            Func<IWord, IOperationResponse> validationFunction =
                parameter =>
                    {
                        if (parameter.Spelling != null)
                        {
                            if (parameter.Spelling.Trim() != string.Empty)
                            {
                                return BuildValidationPassedResponse();
                            }
                        }

                        return BuildEmptySpellingResponse();
                    };

            return PerformValidation(validationFunction, word);
        }

        public IOperationResponse ValidateLanguage(IWord word)
        {
            Func<IWord, IOperationResponse> validationFunction =
                parameter =>
                    {
                        if (parameter.Language == null)
                            return BuildWrongLanguageResponse();

                        var language = languagesRepository.GetLanguage(parameter.Language.Id);
                        return language == null
                                   ? BuildWrongLanguageResponse()
                                   : BuildValidationPassedResponse();
                    };

            return PerformValidation(validationFunction, word);
        }

        public IOperationResponse Validate(IWord word, IWordsRepository wordsRepository)
        {
            Func<IWord, IOperationResponse> validationFunction =
                parameter =>
                    {
                        var result = ValidateSpelling(parameter);
                        if (result.Status)
                        {
                            result = ValidateLanguage(parameter);
                            if (result.Status)
                            {
                                result = ValidateExist(parameter, wordsRepository);
                                if (result.Status)
                                {
                                    return BuildValidationPassedResponse();
                                }
                            }
                        }

                        return result;
                    };

            return PerformValidation(validationFunction, word);
        }

        private IOperationResponse PerformValidation(
            Func<IWord, IOperationResponse> validationFunction, 
            IWord word)
        {
            return word == null 
                ? BuildEmptyWordResponse() 
                : validationFunction(word);
        }

        private IOperationResponse BuildEmptyWordResponse()
        {
            return ResponsesFactory.Create(false, ServiceOperationAction.Validate, EmptyWordErrorCode);
        }

        private IOperationResponse BuildWrongLanguageResponse()
        {
            return ResponsesFactory.Create(false, ServiceOperationAction.Validate, WrongLanguageErrorCode);
        }

        private IOperationResponse BuildWordExistsResponse()
        {
            return ResponsesFactory.Create(false, ServiceOperationAction.Validate, WordAlreadyExistsErrorCode);
        }

        private IOperationResponse BuildEmptySpellingResponse()
        {
            return ResponsesFactory.Create(false, ServiceOperationAction.Validate, EmptySpellingErrorCode);
        }

        private IOperationResponse BuildValidationPassedResponse()
        {
            return ResponsesFactory.Create(true, ServiceOperationAction.Validate);
        }
    }
}