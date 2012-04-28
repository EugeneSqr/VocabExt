using System.Windows;
using VX.Desktop.ServiceFacade;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Desktop
{
    public partial class TranslationsPopup
    {
        private const string TaskFormat = "{0} [{1}]";

        private readonly IVocabServiceFacade serviceFacade = new VocabServiceFacade();
        
        public TranslationsPopup()
        {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            ITask task = serviceFacade.GetTask();
            wordToTranslate.Content = FormatQuestion(task.Question);
            translationsList.ItemsSource = task.Answers;
        }

        private static string FormatQuestion(IWord word)
        {
            return string.Format(TaskFormat, word.Spelling, word.Transcription);
        }

        private void translationsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
