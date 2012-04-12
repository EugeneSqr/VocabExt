using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using VX.Domain;
using VX.Domain.Interfaces;

namespace VX.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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
            this.wordsList.ItemsSource = words;
        }
    }
}
