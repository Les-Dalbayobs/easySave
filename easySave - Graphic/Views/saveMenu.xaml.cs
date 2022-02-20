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

            ProgressBar progressBar1 = ProgressBarJob1;
            ProgressBar progressBar2 = ProgressBarJob2;
            ProgressBar progressBar3 = ProgressBarJob3;
            ProgressBar progressBar4 = ProgressBarJob4;
            ProgressBar progressBar5 = ProgressBarJob5;

            int Worker, IOC;

            ThreadPool.GetMinThreads(out Worker, out IOC);
            ThreadPool.SetMinThreads(1, IOC);

            ThreadPool.GetMaxThreads(out Worker, out IOC);
            ThreadPool.SetMaxThreads(5, IOC);


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

                                ThreadPool.QueueUserWorkItem((s) => thread.threadLoop());
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
                        Models.jobThread thread = new Models.jobThread(mainW.SelectedJob, progressBar1);
                        Thread oneJob = new Thread(new ThreadStart(thread.threadLoop));
                        oneJob.Start();
                        //mainW.SelectedJob.copy("." + mainW.EncryptionExtension, progressBar);
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
