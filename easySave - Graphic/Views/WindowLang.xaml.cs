using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace easySave___Graphic.Views
{
    /// <summary>
    /// Logique d'interaction pour WindowLang.xaml
    /// </summary>
    public partial class WindowLang : Window
    {

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Commencez à faire glisser la fenêtre
            this.DragMove();
        }

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
