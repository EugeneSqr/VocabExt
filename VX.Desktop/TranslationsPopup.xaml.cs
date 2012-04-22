using System.Collections.Generic;
using System.Windows;
using VX.Domain;
using VX.Domain.Interfaces;

namespace VX.Desktop
{
    public partial class TranslationsPopup
    {
        private const string TaskFormat = "{0} [{1}]";
        
        public TranslationsPopup()
        {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            translationsList.ItemsSource = GetTranslations();
            wordToTranslate.Content = GetTask();
        }

        private static string GetTask()
        {
            var word = new Word
            {
                Id = 1,
                Spelling = "Dog",
                Transcription = "Dog"
            };

            return string.Format(TaskFormat, word.Spelling, word.Transcription);
        }

        private static IEnumerable<IWord> GetTranslations()
        {
            IList<IWord> words = new List<IWord>();
            words.Add(new Word
            {
                Spelling = "Cat",
                Transcription = "Cat"
            });
            words.Add(new Word
            {
                Spelling = "Dog",
                Transcription = "Dog"
            });
            words.Add(new Word
            {
                Spelling = "Cow",
                Transcription = "Cow"
            });

            return words;
        }

        private void translationsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
