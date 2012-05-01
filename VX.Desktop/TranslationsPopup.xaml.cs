using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VX.Desktop.ServiceFacade;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Desktop
{
    public partial class TranslationsPopup
    {
        private readonly IVocabServiceFacade serviceFacade = new VocabServiceFacade();

        private IWord correctAnswer;
        
        public TranslationsPopup()
        {
            InitializeComponent();
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            ITask task = serviceFacade.GetTask();
            questionSpelling.Content = task.Question.Spelling;
            questionTranscription.Content = task.Question.Transcription;
            answers.ItemsSource = task.Answers;
            correctAnswer = task.CorrectAnswer;
        }

        private void AnswersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            answers.ItemContainerStyleSelector = new AnswerStyleSelector(correctAnswer);
        }
    }
}
