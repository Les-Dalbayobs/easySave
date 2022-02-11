using System.Reflection;
using System.Resources;
using System.Windows;

namespace easySave___Graphic.Views
{
    /// <summary>
    /// Logique d'interaction pour WindowLang.xaml
    /// </summary>
    public partial class WindowLang : Window
    {
        ResourceManager resource = new ResourceManager("easySave___Graphic.Properties.Resources", Assembly.GetExecutingAssembly());

        public WindowLang()
        {
            InitializeComponent();
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxLanguage.SelectedItem == ComboBoxLanguage.Items[0])
            {
                Properties.Settings.Default.lang = "en";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.lang = "fr";
                Properties.Settings.Default.Save();
            }

            this.Close();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
