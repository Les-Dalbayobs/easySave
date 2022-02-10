using easySave.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace easySave___Graphic.ViewModel
{
    class MainWindowsViewsModel
    {

        #region atributes
        ObservableCollection<job> jobs;

        job selectedJob;

        string pathFilesEasySave = @"c:\EasySave";

        /// <summary>
        /// Creation serialize JsonSerializer to serialize objects or value types into JSON,
        /// and to deserialize JSON into objects or value types.
        /// </summary>
        JsonSerializer serializer;
        string jsonString;
        #endregion

        #region properties
        //public List<job> Jobs { get => jobs; set => jobs = value; }
        public ObservableCollection<job> Jobs { get => jobs; set => jobs = value; }
        
        public job SelectedJob { get => selectedJob; set => selectedJob = value; }
        #endregion

        #region contructor
        public MainWindowsViewsModel()
        {
            

            serializer = new JsonSerializer();

            importConfig();


            //for (int i = 0; i < 3; i++)
            //{
            //    job Job = new job();

            //    Job.Name = "Test";
            //    Job.PathDestination = "test";
            //    Job.PathSource = "Test" + i.ToString();
            //    Job.TypeSave = false;

            //    this.jobs.Add(Job);
            //}

            //string pathLogFolder = pathFilesEasySave + @"\Log";
            //string pathLogProgressSave = pathLogFolder + @"\logProgressSave.json";

            //for (int i = 0; i < jobs.Count; i++)
            //{
            //    this.jobs[i].SetPathFileLogProgress(pathLogFolder);
            //}

            //if (!File.Exists(pathLogProgressSave))
            //{
            //    if (!Directory.Exists(pathLogFolder))
            //    {
            //        Directory.CreateDirectory(pathLogFolder);
            //    }
            //    File.Create(pathLogProgressSave).Close();
            //}

        }

        #endregion

        #region methods
        public void deleteJob(job jobDelete)
        {

        }

        public void addJob(job jobAdd)
        {
            this.jobs.Add(jobAdd);
            serializeJob();
            exportConfig();
        }

        public void serializeJob()
        {
            //Convert the table to suit the json format (in string)
            jsonString = JsonConvert.SerializeObject(this.jobs, Formatting.Indented);
        }

        /// <summary>
        /// Method to deserialize the json string
        /// </summary>
        public void deserializeJob()
        {
            var listJob = JsonConvert.DeserializeObject<ObservableCollection<job>>(jsonString);
        }

        /// <summary>
        /// Method to export the json
        /// </summary>
        public void exportConfig()
        {
            //Check if the folder exists
            if (!Directory.Exists(pathFilesEasySave))
            {
                //If it does not exist we create it
                Directory.CreateDirectory(pathFilesEasySave);
            }

            //Write each directory name to a file.
            using (var streamWriter = new StreamWriter(pathFilesEasySave + @"\configJobGraphical.json"))
            {
                //Initializes a new instance of the JsonTextWriter class using the specified TextWriter.
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(jsonString));
                }
            }
        }

        /// <summary>
        /// Method to import the json
        /// </summary>
        public void importConfig()
        {
            //Check if the folder exists
            if (!Directory.Exists(pathFilesEasySave))
            {
                //If it does not exist we create it
                Directory.CreateDirectory(pathFilesEasySave);
            }

            //If the file does not exist we create it
            if (!File.Exists(pathFilesEasySave + @"\configJobGraphical.json"))
            {

                serializeJob(); //Serialization jobs 
                exportConfig(); //Export jobs
            }
            else
            {
                //StreamReader instance to read text from a file
                using (var streamReader = new StreamReader(pathFilesEasySave + @"\configJobGraphical.json"))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        //Deserialization and import into the job table
                        this.jobs = serializer.Deserialize<ObservableCollection<job>>(jsonReader);
                    }
                }
            }
            if (jobs == null)
            {
                this.jobs = new ObservableCollection<job>();
            }
        }
        #endregion
    }
}
