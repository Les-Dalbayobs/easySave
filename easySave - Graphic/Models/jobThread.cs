using System;
using System.Collections.Generic;
using System.Text;

namespace easySave___Graphic.Models
{
    class jobThread
    {
        job myjob;
        System.Windows.Controls.ProgressBar progressBar;
        string encryption;

        public jobThread(job newJob, System.Windows.Controls.ProgressBar ProBar)
        {
            this.myjob = newJob;
            this.progressBar = ProBar;
            this.encryption = Properties.Settings.Default.encryption;
        }

        public void threadLoop()
        {
            myjob.copy(encryption, progressBar);
        }
    }
}
