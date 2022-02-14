using System.Windows;

namespace easySave___Graphic.Views
{
    /// <summary>
    /// Logique d'interaction pour EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        public EncryptionWindow()
        {
            InitializeComponent();
        }
        public void Cancel_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
