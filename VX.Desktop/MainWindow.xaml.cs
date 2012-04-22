namespace VX.Desktop
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            taskbarIcon.ShowBalloonTip("VocabExt", "Time to learn English", taskbarIcon.Icon);
            Hide();
        }
    }
}