using System.Windows;
using VX.Desktop.ServiceFacade;
using VX.Desktop.ServiceFacade.VocabServiceReference;

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
            TaskContract task = serviceFacade.GetTask();
            wordToTranslate.Content = FormatQuestion((WordContract)task.Question);
            translationsList.ItemsSource = task.Answers;
        }

        private static string FormatQuestion(WordContract word)
        {
            return string.Format(TaskFormat, word.Spelling, word.Transcription);
        }

        private void translationsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
