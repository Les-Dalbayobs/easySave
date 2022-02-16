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
        ResourceManager resource = new ResourceManager("easySave___Graphic.Properties.Resources", Assembly.GetExecutingAssembly());

        public object SelectedJob { get; private set; }

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

                mainW.addJob(newJob);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            if (mainW.SelectedJob != null)
            {
                // Store the old name of the job
                string oldName = mainW.SelectedJob.Name;

                Views.CreatJob editJob = new Views.CreatJob();

                editJob.DataContext = mainW.SelectedJob;

                editJob.ShowDialog();

                mainW.editJob(oldName);
            }
            else
            {
                MessageBox.Show(resource.GetString("selectJob"));
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            if (mainW.SelectedJob != null)
            {
                Views.DeleteJob delete = new Views.DeleteJob();
                delete.DeleteAlert.Content = resource.GetString("wantDelete") + mainW.SelectedJob.Name + " ?";

                if (delete.ShowDialog() == true)
                {
                    mainW.deleteJob(mainW.SelectedJob);
                }
            }
            else
            {
                MessageBox.Show(resource.GetString("selectJob"));
            }
        }
        
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            Views.saveMenu SaveMenu = new Views.saveMenu();

            SaveMenu.DataContext = mainW;


            if (mainW.SelectedJob != null)
            {

                SaveMenu.RadioOneJob.Content = resource.GetString("saveOneJob") + mainW.SelectedJob.Name;
                SaveMenu.RadioOneJob.IsChecked = true;

                SaveMenu.ShowDialog();
            }
            else
            {
                SaveMenu.RadioOneJob.Content = resource.GetString("noSelectJob");
                SaveMenu.RadioOneJob.IsEnabled = false;
                SaveMenu.RadioAllJob.IsChecked = true;
                SaveMenu.RadioAllJob.IsEnabled = false;

                SaveMenu.ShowDialog();
            }
        }
        
        public void ButtonEncryption_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            Views.EncryptionWindow encryptionWindow = new Views.EncryptionWindow();
            encryptionWindow.ShowDialog();
        }

    }
}
