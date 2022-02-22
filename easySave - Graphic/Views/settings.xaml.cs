using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour EncryptionWindow.xaml
    /// </summary>
    public partial class settings : Window
    {
        ResourceManager resource = new ResourceManager("easySave___Graphic.Properties.Resources", Assembly.GetExecutingAssembly());

        public settings()
        {
            InitializeComponent();
        }

        private void ComboBoxExtension_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;

            this.TextBoxEncryption.Text = text;
            this.TextBoxEncryption.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.ToggleButtonFormatLog.IsChecked.Value)
            {
                Properties.Settings.Default.typeLog = "json";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.typeLog = "xml";
                Properties.Settings.Default.Save();
            }
            this.Close();
        }

        private void ComboBoxProcess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = e.AddedItems[0].ToString();

            this.TextBoxProcess.Text = text;
            this.TextBoxProcess.Focus();

        }

        private void ButtonRemovePrioExtension_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            if (mainW.SelectPrioExtension != null)
            {
                mainW.deletePrioExtension();
            }
            else if (this.ListBoxPrioExtension.Items.Count == 1)
            {
                mainW.PrioExtension.RemoveAt(0);
            }
            else
            {
                MessageBox.Show(resource.GetString("selectExtension"));
            }
        }

        private void ButtonNewPrioExtension_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            if (mainW.SelectPrioExtension != null)
            {
                mainW.addPrioExtension();
                this.TextBoxPrioExtension.Text = string.Empty;
            }
            else
            {
                MessageBox.Show(resource.GetString("selectAddExtension"));
            }
        }
    }
}