using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows;
using System.Diagnostics;

namespace easySave___Graphic.ViewModel
{
    class MainWindowsViewsModel
    {
        #region atributes
        ObservableCollection<string> prioExtension;

        /// <summary>
        /// Observable collection with all jobs
        /// </summary>
        ObservableCollection<easySave___Graphic.Models.job> jobs;

        /// <summary>
        /// Job selected in the DataGrid
        /// </summary>
        easySave___Graphic.Models.job selectedJob;

        /// <summary>
        /// easySave folder path
        /// </summary>
        string pathFilesEasySave = @"c:\EasySave";

        /// <summary>
        /// Creation serialize JsonSerializer to serialize objects or value types into JSON,
        /// and to deserialize JSON into objects or value types.
        /// </summary>
        JsonSerializer serializer;

        /// <summary>
        /// Save the json
        /// </summary>
        string jsonString;

        string encryptionExtension;

        List<string> currentProcess;

        string selectedProcess;
        #endregion

        #region properties
        /// <summary>
        /// Getter - Setter of the jobs attribute
        /// </summary>
        public ObservableCollection<easySave___Graphic.Models.job> Jobs { get => jobs; set => jobs = value; }

        /// <summary>
        /// Getter Setter of the SelectedJob attribute
        /// </summary>
        public easySave___Graphic.Models.job SelectedJob { get => selectedJob; set => selectedJob = value; }

        public string EncryptionExtension { get => encryptionExtension; set => encryptionExtension = value; }
        public List<string> CurrentProcess { get => currentProcess; set => currentProcess = value; }
        public string SelectedProcess { get => selectedProcess; set => selectedProcess = value; }
        public ObservableCollection<string> PrioExtension { get => prioExtension; set => prioExtension = value; }

        #endregion

        #region contructor
        public MainWindowsViewsModel()
        {
            serializer = new JsonSerializer();

            encryptionExtension = Properties.Settings.Default.encryption;
            readProcess();
            
            importConfig();

            string pathLogFolder = pathFilesEasySave + @"\Log";

            string pathLogProgressSave = pathLogFolder + @"\logProgressSave.json";

            if (!File.Exists(pathLogProgressSave))
            {
                if (!Directory.Exists(pathLogFolder))
                {
                    Directory.CreateDirectory(pathLogFolder);
                }
                File.Create(pathLogProgressSave).Close();
            }

            upadtePrioExtension();
        }

        #endregion

        #region methods

        public void upadtePrioExtension()
        {
            prioExtension = new ObservableCollection<string>();

            for (int i = 0; i < 10; i++)
            {
                prioExtension.Add("test");
            }
        }

        /// <summary>
        ///  Method to remove a job to the list and export the config
        /// </summary>
        /// <param name="jobDelete">Job to remove in the list</param>
        public void deleteJob(easySave___Graphic.Models.job jobDelete)
        {
            this.jobs.Remove(jobDelete);
            serializeJob();
            exportConfig();
        }

        /// <summary>
        /// Method to add a job to the list and export the config
        /// </summary>
        /// <param name="jobAdd">Job to add in the list</param>
        public void addJob(easySave___Graphic.Models.job jobAdd)
        {
            this.jobs.Add(jobAdd);
            serializeJob();
            exportConfig();
        }

        public void editJob(string oldName)
        {
            updateLog(oldName);
            serializeJob();
            exportConfig();
        }

        /// <summary>
        /// Method to serialize jobs
        /// </summary>
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
            var listJob = JsonConvert.DeserializeObject<ObservableCollection<easySave___Graphic.Models.job>>(jsonString);
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
            using (var streamWriter = new StreamWriter(pathFilesEasySave + @"\configJobsGraphical.json"))
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
            if (!File.Exists(pathFilesEasySave + @"\configJobsGraphical.json"))
            {

                serializeJob(); //Serialization jobs 
                exportConfig(); //Export jobs
            }
            else
            {
                //StreamReader instance to read text from a file
                using (var streamReader = new StreamReader(pathFilesEasySave + @"\configJobsGraphical.json"))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        //Deserialization and import into the job table
                        this.jobs = serializer.Deserialize<ObservableCollection<easySave___Graphic.Models.job>>(jsonReader);
                    }
                }
            }
            // If nothing has been imported, initialize the list
            if (jobs == null)
            {
                this.jobs = new ObservableCollection<easySave___Graphic.Models.job>();
            }
        }

        /// <summary>
        /// Method which change the name of the job in the log when updating it
        /// </summary>
        public void updateLog(string oldName)
        {
            // Search index in state log list
            int index = easySave___Graphic.Models.Global.listSaveAdvancement.FindIndex(item => item.Name == oldName);

            // Replace old name by the new name of the job in the state log
            if (index >= 0)
            {
                easySave___Graphic.Models.Global.listSaveAdvancement[index].Name = selectedJob.Name;
                selectedJob.writeLogAdvancement();
            }
        }

        /// <summary>
        /// Method which delete the the job from the log when deleting it globally
        /// </summary>
        public void deleteLog()
        {
            // Search index in state log list
            int index = easySave___Graphic.Models.Global.listSaveAdvancement.FindIndex(item => item.Name == selectedJob.Name);

            if (index >= 0)
            {
                easySave___Graphic.Models.Global.listSaveAdvancement.RemoveAt(index);
                selectedJob.writeLogAdvancement();
            }
        }

        public void newEncryption()
        {
            Properties.Settings.Default.encryption = encryptionExtension;
            Properties.Settings.Default.Save();
        }

        public void readEncryption()
        {
            encryptionExtension = Properties.Settings.Default.encryption;
        }

        public void updateProcess()
        {
            currentProcess = new List<string>();

            Process[] allProcess = Process.GetProcesses();

            foreach (Process oneProcess in allProcess)
            {
                currentProcess.Add(oneProcess.ProcessName);
            }
        }

        public void newProcess()
        {
            Properties.Settings.Default.processUser = this.selectedProcess;
            Properties.Settings.Default.Save();
        }

        public void readProcess()
        {
            this.selectedProcess = Properties.Settings.Default.processUser;
        }

        public bool checkProcess()
        {
            if (this.selectedProcess != null && this.selectedProcess != "")
            {
                updateProcess();

                if (currentProcess.Contains(selectedProcess))
                {
                    return true;
                }
            }
            
            return false;
        }
        #endregion
    }
}
