using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls;

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

        public jobThread(job newJob, System.Windows.Controls.ProgressBar ProBar = null)
        {
            this.myjob = newJob;
            this.encryption = Properties.Settings.Default.encryption;
            createListPrioExtension();
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
                        myjob.copy(prio, "." + encryption, ProgressBar1);

                    proBar1.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                else if (proBar2.WaitOne(1000))
                {
                    if (Global.stop == false)
                        myjob.copy(prio, "." + encryption, ProgressBar2);

                    proBar2.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                else if (proBar3.WaitOne(1000))
                {
                    if (Global.stop == false)
                        myjob.copy(prio, "." + encryption, ProgressBar3);

                    proBar3.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                else if (proBar4.WaitOne(1000))
                {
                    if (Global.stop == false)
                        myjob.copy(prio ,"." + encryption, ProgressBar4);

                    proBar4.ReleaseMutex();
                    finish = true;
                    semaphore.Release();
                }
                else if (proBar5.WaitOne(1000))
                {
                    if (Global.stop == false)
                        myjob.copy(prio, "." + encryption, ProgressBar5);

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
