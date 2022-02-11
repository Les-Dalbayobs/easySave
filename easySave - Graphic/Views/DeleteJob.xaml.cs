using System.Windows;

namespace easySave___Graphic.Views
{
    /// <summary>
    /// Logique d'interaction pour DeleteJob.xaml
    /// </summary>
    public partial class DeleteJob : Window
    {
        public DeleteJob()
        {
            InitializeComponent();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}