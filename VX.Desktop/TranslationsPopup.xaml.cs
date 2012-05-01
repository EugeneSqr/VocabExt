using System.Windows;
using System.Windows.Controls;
using VX.Desktop.ServiceFacade;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Desktop
{
    public partial class TranslationsPopup
    {
        private readonly IVocabServiceFacade serviceFacade = new VocabServiceFacade();

        private AnswerStyleSelector answerStyleSelector;

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
            answerStyleSelector = new AnswerStyleSelector
                                      {
                                          HideAnswersStyle = (Style)Resources["HideAnswersStyle"],
                                          ShowAnswersStyle = (Style)Resources["ShowAnswersStyle"]
                                      };
            answers.ItemContainerStyleSelector = answerStyleSelector;
        }

        private void AnswersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aaa = new AnswerStyleSelector
            {
                HideAnswersStyle = (Style)Resources["HideAnswersStyle"],
                ShowAnswersStyle = (Style)Resources["ShowAnswersStyle"]
            };
            aaa.CorrectAnswer = correctAnswer;
            answers.ItemContainerStyleSelector = aaa;
        }
    }
}
