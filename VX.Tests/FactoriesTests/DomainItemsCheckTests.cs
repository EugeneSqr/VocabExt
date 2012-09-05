using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Surrogates;

namespace VX.Tests.FactoriesTests
{
    public abstract class DomainItemsCheckTests<TType, TImplementation> : StreamSerializationTests<TType, TImplementation>
    {
        protected static void CheckLanguage(int expectedId, string expectedName, string expectedAbbreviation, ILanguage actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedAbbreviation, actual.Abbreviation);
        }

        protected static void CheckWord(
            int expectedId,
            string expectedSpelling,
            string expectedTranscription,
            int langId,
            string langName,
            string langAbbreviation,
            IWord actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedSpelling, actual.Spelling);
            Assert.AreEqual(expectedTranscription, actual.Transcription);

            CheckLanguage(
                langId,
                langName,
                langAbbreviation,
                actual.Language);
        }

        protected static void CheckWordEmptyLanguage(
            int expectedId,
            string expectedSpelling,
            string expectedTranscription,
            IWord actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedSpelling, actual.Spelling);
            Assert.AreEqual(expectedTranscription, actual.Transcription);
            Assert.IsNull(actual.Language);
        }

        protected static void CheckVocabBankSummary(
            int expectedId,
            string expectedName,
            string expectedDescription,
            IVocabBankSummary actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedDescription, actual.Description);
        }

        protected static void CheckEmptyVocabBank(IVocabBank actual)
        {
            Assert.AreEqual(0, actual.Id);
            Assert.IsNull(actual.Name);
            Assert.IsNull(actual.Description);
            Assert.IsNull(actual.Translations);
            Assert.IsNull(actual.Tags);
        }

        protected static void CheckTag(int expectedId, string expectedName, string expectedDescription, ITag actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedDescription, actual.Description);
        }

        protected static void CheckEmptyTranslation(ITranslation actual)
        {
            Assert.AreEqual(0, actual.Id);
            Assert.IsNull(actual.Source);
            Assert.IsNull(actual.Target);
        }

        protected static void CheckEmptyLanguage(ILanguage actual)
        {
            Assert.AreEqual(0, actual.Id);
            Assert.AreEqual(null, actual.Name);
            Assert.AreEqual(null, actual.Abbreviation);
        }

        protected static void CheckEmptyWord(IWord actual)
        {
            Assert.AreEqual(0, actual.Id);
            Assert.AreEqual(null, actual.Spelling);
            Assert.AreEqual(null, actual.Transcription);
            Assert.IsNull(actual.Language);
        }

        protected static void CheckBankTranslationPair(
            int expectedBankId, 
            int expectedTranslationId, 
            IBankTranslationPair actual)
        {
            Assert.AreEqual(expectedBankId, actual.VocabBankId);
            Assert.AreEqual(expectedTranslationId, actual.Translation.Id);
        }

        protected static void CheckBankTranslationPairEmptyTranslation(
            int expectedBankId, 
            IBankTranslationPair actual)
        {
            Assert.AreEqual(expectedBankId, actual.VocabBankId);
            Assert.IsNull(actual.Translation);
        }

        protected static void CheckParentChildIdPair(
            int expectedParentId, 
            int expectedChildId, 
            IParentChildIdPair actual)
        {
            Assert.AreEqual(expectedParentId, actual.ParentId);
            Assert.AreEqual(expectedChildId, actual.ChildId);
        }

        protected static void CheckEmptyParentChildIdPair(IParentChildIdPair actual)
        {
            Assert.AreEqual(0, actual.ParentId);
            Assert.AreEqual(0, actual.ChildId);
        }
    }
}
