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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Commencez à faire glisser la fenêtre
            this.DragMove();
        }

        ResourceManager resource = new ResourceManager("easySave___Graphic.Properties.Resources", Assembly.GetExecutingAssembly());
        //public static List<Thread> threads = new List<Thread>();

        public saveMenu()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Models.Global.stop = false;
            Models.job.prioFinish = 0;

            Ok.IsEnabled = false;
            ButtonPause.IsEnabled = true;
            ButtonStop.IsEnabled = true;

            ViewModel.MainWindowsViewsModel mainW = this.DataContext as ViewModel.MainWindowsViewsModel;

            Models.jobThread.ProgressBar1 = ProgressBarJob1;
            Models.jobThread.ProgressBar2 = ProgressBarJob2;
            Models.jobThread.ProgressBar3 = ProgressBarJob3;
            Models.jobThread.ProgressBar4 = ProgressBarJob4;
            Models.jobThread.ProgressBar5 = ProgressBarJob5;

            Models.jobThread.label1 = LabelSaveStatut1;
            Models.jobThread.label2 = LabelSaveStatut2;
            Models.jobThread.label3 = LabelSaveStatut3;
            Models.jobThread.label4 = LabelSaveStatut4;
            Models.jobThread.label5 = LabelSaveStatut5;

            Models.jobThread.labelName1 = LabelSave1;
            Models.jobThread.labelName2 = LabelSave2;
            Models.jobThread.labelName3 = LabelSave3;
            Models.jobThread.labelName4 = LabelSave4;
            Models.jobThread.labelName5 = LabelSave5;

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
            Models.Global.stop = true;
            Models.Global.pause = false;
            this.Close();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            Models.Global.stop = true;
            Models.Global.pause = false;
            ButtonPause.IsEnabled = false;
            ButtonPlay.IsEnabled = false;
            ButtonStop.IsEnabled = false;
            Ok.IsEnabled = false;
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            Models.Global.pause = true;
            ButtonPause.IsEnabled = false;
            ButtonPlay.IsEnabled = true;
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            Models.Global.pause = false;
            ButtonPlay.IsEnabled = false;
            ButtonPause.IsEnabled = true;
        }
    }
}
