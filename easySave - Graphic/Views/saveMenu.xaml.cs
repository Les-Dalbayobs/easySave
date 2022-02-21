using System;
using System.Collections.Generic;
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
    /// Interaction logic for saveMenu.xaml
    /// </summary>
    public partial class saveMenu : Window
    {
        ResourceManager resource = new ResourceManager("easySave___Graphic.Properties.Resources", Assembly.GetExecutingAssembly());

        public saveMenu()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            Models.jobThread.ProgressBar1 = ProgressBarJob1;
            Models.jobThread.ProgressBar2 = ProgressBarJob2;
            Models.jobThread.ProgressBar3 = ProgressBarJob3;
            Models.jobThread.ProgressBar4 = ProgressBarJob4;
            Models.jobThread.ProgressBar5 = ProgressBarJob5;

            if (mainW.checkProcess())
            {
                MessageBox.Show(resource.GetString("noLaunchSave"));
            }
            else
            {
                if (this.RadioAllJob.IsChecked == true)
                {
                    bool error = false;
                    foreach (Models.job oneJob in mainW.Jobs)
                    {
                        if (!oneJob.verifExist(oneJob.PathSource))
                        {
                            MessageBox.Show(resource.GetString("sourcePathError") + oneJob.PathSource);
                            error = true;
                            break;
                        }
                        if (!oneJob.verifCreateDestination())
                        {
                            MessageBox.Show(resource.GetString("destPathError") + oneJob.PathDestination);
                            error = true;
                            break;
                        }
                    }
                    if (!error)
                    {
                        foreach (Models.job oneJob in mainW.Jobs)
                        {
                            if (mainW.checkProcess())
                            {
                                MessageBox.Show(resource.GetString("errorProcessRunning"));
                                break;
                            }
                            else
                            {
                                Models.jobThread thread = new Models.jobThread(oneJob);
                                Thread threadOneJob = new Thread(new ThreadStart(thread.threadLoop));
                                threadOneJob.Start();
                            }
                        }
                    }
                }
                else
                {
                    if (!mainW.SelectedJob.verifExist(mainW.SelectedJob.PathSource))
                    {
                        MessageBox.Show(resource.GetString("sourcePathError") + mainW.SelectedJob.PathSource);
                    }
                    else if (!mainW.SelectedJob.verifCreateDestination())
                    {
                        MessageBox.Show(resource.GetString("destPathError") + mainW.SelectedJob.PathDestination);
                    }
                    else
                    {
                        Models.jobThread thread = new Models.jobThread(mainW.SelectedJob);
                        Thread oneJob = new Thread(new ThreadStart(thread.threadLoop));
                        oneJob.Start();
                    }
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
