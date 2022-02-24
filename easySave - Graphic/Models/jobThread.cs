using System;
using System.Collections.Generic;
using System.Text;
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

        public void threadLoop()
        {
            bool finish = false;

            semaphore.WaitOne();

            while (!finish)
            {
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

        public void createListPrioExtension()
        {
            if (Properties.Settings.Default.prioExtension != null)
            {
                this.prio = new List<string>();

                foreach (var item in Properties.Settings.Default.prioExtension)
                {
                    this.prio.Add(item);
                }
            }
            else
            {
                this.prio = null;
            }
        }
    }
}
