using System.Windows.Controls.Primitives;

namespace VX.Desktop
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Hide();
            var translationsBaloon = new TranslationsBaloon();
            taskbarIcon.ShowCustomBalloon(translationsBaloon, PopupAnimation.Slide, 5000);
        }
    }
}