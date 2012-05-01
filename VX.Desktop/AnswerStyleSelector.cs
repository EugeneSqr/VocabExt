using System.Windows;
using System.Windows.Controls;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Desktop
{
    public class AnswerStyleSelector : StyleSelector
    {
        public IWord CorrectAnswer { get; set; }

        public Style ShowAnswersStyle { get; set; }

        public Style HideAnswersStyle { get; set; }
        
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var answer = item as IWord;
            return answer != null && CorrectAnswer != null ? ShowAnswersStyle : HideAnswersStyle;
        }
    }
}
