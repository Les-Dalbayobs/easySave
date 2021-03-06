using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace easySave___Graphic.Models
{
    class jobThread
    {
        job myjob;
        string encryption;

        public static Semaphore semaphore = new Semaphore(5, 5);

        List<string> prio;

        public static Mutex proBar1 = new Mutex();
        public static Mutex proBar2 = new Mutex();
        public static Mutex proBar3 = new Mutex();
        public static Mutex proBar4 = new Mutex();
        public static Mutex proBar5 = new Mutex();

        public static ProgressBar ProgressBar1;
        public static ProgressBar ProgressBar2;
        public static ProgressBar ProgressBar3;
        public static ProgressBar ProgressBar4;
        public static ProgressBar ProgressBar5;

        public static Label label1;
        public static Label label2;
        public static Label label3;
        public static Label label4;
        public static Label label5;

        public static Label labelName1;
        public static Label labelName2;
        public static Label labelName3;
        public static Label labelName4;
        public static Label labelName5;

        public jobThread(job newJob, System.Windows.Controls.ProgressBar ProBar = null)
        {
            this.myjob = newJob;
            this.encryption = Properties.Settings.Default.encryption;
            createListPrioExtension();
        }

        public void updateLabel(System.Windows.Controls.Label label, string value)
        {
            if (label != null)
            {
                label.Content = value;
            }
        }

        /// <summary>
        /// Method which creates a loop for all threads
        /// </summary>
        public void threadLoop()
        {
            // Initialize finish at false
            bool finish = false;

            semaphore.WaitOne();

            // While loop which provides job to all progress bars
            while (!finish)
            {
                // Check if the first progress bar waits for a job
                if (proBar1.WaitOne(1000))
                {
                    if (Global.stop == false)
                    {
                        if (labelName1 != null)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(labelName1, myjob.Name)), DispatcherPriority.ContextIdle);
                        }
                        myjob.copy(prio, "." + encryption, ProgressBar1, label1);
                    }

                    proBar1.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                // Check if the second progress bar waits for a job
                else if (proBar2.WaitOne(1000))
                {
                    if (Global.stop == false)
                    {
                        if (labelName1 != null)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(labelName2, myjob.Name)), DispatcherPriority.ContextIdle);
                        }
                        myjob.copy(prio, "." + encryption, ProgressBar2, label2);
                    }

                    proBar2.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                // Check if the third progress bar waits for a job
                else if (proBar3.WaitOne(1000))
                {
                    if (Global.stop == false)
                    {
                        if (labelName1 != null)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(labelName3, myjob.Name)), DispatcherPriority.ContextIdle);
                        }
                        myjob.copy(prio, "." + encryption, ProgressBar3, label3);
                    }

                    proBar3.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                // Check if the fourth progress bar waits for a job
                else if (proBar4.WaitOne(1000))
                {
                    if (Global.stop == false)
                    {
                        if (labelName1 != null)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(labelName4, myjob.Name)), DispatcherPriority.ContextIdle);
                        }
                        myjob.copy(prio, "." + encryption, ProgressBar4, label4);
                    }

                    proBar4.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                // Check if the fifth progress bar waits for a job
                else if (proBar5.WaitOne(1000))
                {
                    if (Global.stop == false)
                    {
                        if (labelName1 != null)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(labelName5, myjob.Name)), DispatcherPriority.ContextIdle);
                        }
                        myjob.copy(prio, "." + encryption, ProgressBar5, label5);
                    }

                    proBar5.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
            }
        }

        /// <summary>
        /// Method which create a list of all file extensions the user wants to save in priority
        /// </summary>
        public void createListPrioExtension()
        {
            // Check if the user specified file extensions or not
            if (Properties.Settings.Default.prioExtension != null)
            {
                //  If yes, this creates a list of string which all file extensions he created
                this.prio = new List<string>();

                foreach (var item in Properties.Settings.Default.prioExtension)
                {
                    this.prio.Add(item);
                }
            }
            // If not -> prio = null
            else
            {
                this.prio = null;
            }
        }
    }
}
