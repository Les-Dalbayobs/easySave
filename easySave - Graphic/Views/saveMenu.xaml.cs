using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for saveMenu.xaml
    /// </summary>
    public partial class saveMenu : Window
    {
        public saveMenu()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;


            if (this.RadioAllJob.IsChecked == true)
            {
                bool error = false;
                foreach (var oneJob in mainW.Jobs)
                {
                    if (!oneJob.verifExist(oneJob.PathSource))
                    {
                        MessageBox.Show("Source path error of the job: " + oneJob.Name);
                        error = true;
                        break;
                    }
                    if (!oneJob.verifCreateDestination())
                    {
                        MessageBox.Show("Destination path error of the job: " + oneJob.Name);
                        error = true;
                        break;
                    }
                    
                }
                if (!error)
                {
                    foreach (var oneJob in mainW.Jobs)
                    {
                        oneJob.copy();
                    }
                }
            }
            else
            {
                if (!mainW.SelectedJob.verifExist(mainW.SelectedJob.PathSource))
                {
                    MessageBox.Show("Source path error");
                }
                else if (!mainW.SelectedJob.verifCreateDestination())
                {
                    MessageBox.Show("Destination path error");
                }
                else
                {
                    mainW.SelectedJob.copy();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
