using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace easySave___Graphic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string language = Properties.Settings.Default.lang;
            Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(language);

            InitializeComponent();

            ViewModel.MainWindowsViewsModel mainWindowsViewsModel = new ViewModel.MainWindowsViewsModel();

            this.DataContext = mainWindowsViewsModel;
        }

        private void buttonLanguage_Click(object sender, RoutedEventArgs e)
        {
            Views.WindowLang lang = new Views.WindowLang();

            lang.ShowDialog();

            MainWindow main = new MainWindow();

            Application.Current.MainWindow = main;

            main.Show();

            this.Close();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            easySave.Models.job newJob = new easySave.Models.job();

            Views.CreatJob creatJob = new Views.CreatJob();

            creatJob.DataContext = newJob;

            if (creatJob.ShowDialog() == true)
            {
                ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;
                
                if (newJob.PathSource == null)
                    newJob.PathSource = creatJob.TextBoxSource.Text;

                if (newJob.PathDestination == null)
                    newJob.PathDestination = creatJob.TextBoxDestination.Text;

                if (creatJob.RadioComplete.IsChecked == true)
                    newJob.TypeSave = true;
                else
                    newJob.TypeSave = false;

                mainW.addJob(newJob);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            if (mainW.SelectedJob != null)
            {
                Views.CreatJob editJob = new Views.CreatJob();

                editJob.DataContext = mainW.SelectedJob;

                editJob.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a job");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            if (mainW.SelectedJob != null)
            {
                Views.DeleteJob delete = new Views.DeleteJob();

                if (delete.ShowDialog() == true)
                {
                    mainW.Jobs.Remove(mainW.SelectedJob);
                }
            }
            else
            {
                MessageBox.Show("Please select job");
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Views.saveMenu SaveMenu = new Views.saveMenu();

            SaveMenu.ShowDialog();
        }
    }
}
