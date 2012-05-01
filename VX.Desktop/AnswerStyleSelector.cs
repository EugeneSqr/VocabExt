using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Desktop
{
    internal class AnswerStyleSelector : StyleSelector
    {
        private readonly IWord correctAnswer;

        private Style correctAnswerStyle;

        private Style wrongAnswerStyle;

        public AnswerStyleSelector(IWord correctAnswer)
        {
            this.correctAnswer = correctAnswer;
            DefineStyles();
        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var answer = item as IWord;
            if (answer != null && correctAnswer != null)
            {
                if (answer.Id == correctAnswer.Id)
                {
                    return correctAnswerStyle;
                }
            }

            return wrongAnswerStyle;
        }

        private void DefineStyles()
        {
            
            
            correctAnswerStyle = new Style { TargetType = typeof(ListBoxItem) };
            correctAnswerStyle.Setters.Add(new Setter
                                               {
                                                   Property = Control.BackgroundProperty,
                                                   Value = Brushes.Green
                                               });

            wrongAnswerStyle = new Style {TargetType = typeof(ListBoxItem)};
            wrongAnswerStyle.Setters.Add(new Setter
                                             {
                                                 Property = Control.BackgroundProperty,
                                                 Value = Brushes.Red
                                             });
        }
    }
}
