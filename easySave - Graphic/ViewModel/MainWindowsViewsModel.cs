using easySave.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace easySave___Graphic.ViewModel
{
    class MainWindowsViewsModel
    {

        #region atributes
        ObservableCollection<job> jobs;

        job selectedJob;

        string pathFilesEasySave = @"c:\EasySave";

        #endregion

        #region properties
        //public List<job> Jobs { get => jobs; set => jobs = value; }
        public ObservableCollection<job> Jobs { get => jobs; set => jobs = value; }
        
        public job SelectedJob { get => selectedJob; set => selectedJob = value; }
        #endregion

        #region contructor
        public MainWindowsViewsModel()
        {
            this.jobs = new ObservableCollection<job>();

            for (int i = 0; i < 3; i++)
            {
                job Job = new job();

                Job.Name = "Test";
                Job.PathDestination = "test";
                Job.PathSource = "Test" + i.ToString();
                Job.TypeSave = false;

                this.jobs.Add(Job);
            }

            string pathLogFolder = pathFilesEasySave + @"\Log";
            string pathLogProgressSave = pathLogFolder + @"\logProgressSave.json";

            for (int i = 0; i < jobs.Count; i++)
            {
                this.jobs[i].SetPathFileLogProgress(pathLogFolder);
            }

            if (!File.Exists(pathLogProgressSave))
            {
                if (!Directory.Exists(pathLogFolder))
                {
                    Directory.CreateDirectory(pathLogFolder);
                }
                File.Create(pathLogProgressSave).Close();
            }

        }

        #endregion

        #region methods

        #endregion
    }
}
