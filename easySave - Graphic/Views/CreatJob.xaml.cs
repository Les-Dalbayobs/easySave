using System.Windows;

namespace easySave___Graphic.Views
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class CreatJob : Window
    {
        public CreatJob()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonSource_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pathSource = dialog.SelectedPath;
                this.TextBoxSource.Text = pathSource;
            }
        }

        private void ButtonDestination_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pathDestination= dialog.SelectedPath;
                this.TextBoxDestination.Text = pathDestination;
            }
        }
    }
}
