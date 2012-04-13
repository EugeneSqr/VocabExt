using System.Collections.Generic;

using VX.Domain;
using VX.Domain.Interfaces;

namespace VX.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
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

            wordsList.ItemsSource = words;
        }
    }
}
