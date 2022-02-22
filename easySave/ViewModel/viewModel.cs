/// \file viewModel.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 1.1
/// \date 23/01/2022

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("easySave - Graphic")]

/// <summary>
/// ViewModel namespace
/// </summary>
namespace easySave.ViewModel
{
    /// <summary>
    /// Class to make the link between views and models
    /// </summary>
    class viewModel
    {
        #region attributes

        /// <summary>
        /// Creation of the view
        /// </summary>
        Views.view view = new Views.view();

        /// <summary>
        /// Job table
        /// </summary>
        List<Models.job> jobs;

        int nbJobMax;

        /// <summary>
        /// Store the string in json
        /// </summary>
        string jsonString;

        /// <summary>
        /// Creation serialize JsonSerializer to serialize objects or value types into JSON,
        /// and to deserialize JSON into objects or value types.
        /// </summary>
        JsonSerializer serializer;

        /// <summary>
        /// Program file path
        /// </summary>
        string pathFilesEasySave = @"c:\EasySave";

        // Create list to store strings of log save file
        List<string> logSaveList = new List<string>();

        // Variable to store type of logs, initialized with JSON by default value ( = false )
        bool Typelog = false;
        

        #endregion

        #region properties

        #endregion

        #region constructor

        /// <summary>
        /// Constructor of the view class (without parameters)
        /// </summary>
        public viewModel()
        {
            serializer = new JsonSerializer();

            nbJobMax = 5;

            //Create or reset the job table
            this.jobs = new List<Models.job>();

            for (int i = 0; i < nbJobMax; i++)
            {
                Models.job addJob = new Models.job();

                this.jobs.Add(addJob);
            }

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

            for (int i = 0; i < nbJobMax; i++)
            {
                this.jobs[i].SetPathFileLogProgress(pathLogFolder);
            }

            updateHeader();
            menu();
        }

        #endregion

        #region methods

        /// <summary>
        /// Main program loop
        /// </summary>
        public void menu()
        {
            int menu = 0;

            do
            {
                // Call method to display menu
                menu = view.displayMenu();

                // Switch to manage choices into the menu
                switch (menu)
                {
                    // Create job menu
                    case 1:
                        {
                            int nbJob = view.chooseCreate();
                            
                            if (nbJob == 6) // = Exit
                            {
                                break;
                            }

                            view.create(false);

                            int valid = view.confirmCreate();
                            if (valid == 2) // = Cancel creation
                            {
                                break;
                            }

                            createJob(nbJob);

                            view.finishCreate();
                            break;
                        }

                    // Delete menu choice   
                    case 2:
                        {
                            int nbJob = view.chooseDelete(); // Initialize Job number variable
                            if (nbJob == 6) // Equal to exit choice
                            {
                                break;
                            }

                            int valid = view.confirmDelete(nbJob);
                            if (valid == 2) // = Cancel delete
                            {
                                break;
                            }
                            else
                            {
                                deleteJob(nbJob);
                                view.finishDelete(nbJob);

                                break;
                            }
                        }

                    // Save menu choice
                    case 3:
                        {
                            int nbjob = view.chooseSave();

                            if (nbjob == 7) // Exit choice
                            {
                                break;
                            }

                            int valid = view.confirmSave(nbjob);
                            if (valid == 2) // Cancel save
                            {
                                break;
                            }

                            if (nbjob == 6) // Save all jobs
                            {
                                int numberJob = 1;
                                bool error = false;

                                foreach (var job in jobs)
                                {
                                    if (job.PathSource != null || job.PathDestination != null)
                                    {
                                        if (!job.verifExist(job.PathSource))
                                        {
                                            view.errorSave(numberJob, true);
                                            error = true;
                                            break;
                                        }
                                        if (!job.verifCreateDestination())
                                        {
                                            view.errorSave(numberJob, false);
                                            error = true;
                                            break;
                                        }
                                    }
                                    numberJob++;
                                }
                                if (!error)
                                {
                                    foreach (var job in jobs)
                                    {
                                        if (job.PathSource != null || job.PathDestination != null)
                                        {
                                            job.copy(Typelog);
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (this.jobs[nbjob - 1].PathSource != null || this.jobs[nbjob -1 ].PathDestination != null)
                                {
                                    if (!this.jobs[nbjob - 1].verifExist(this.jobs[nbjob - 1].PathSource))
                                    {
                                        view.errorSave(nbjob, true);
                                        break;
                                    }
                                    else if (!this.jobs[nbjob - 1].verifCreateDestination())
                                    {
                                        view.errorSave(nbjob, false);
                                        break;
                                    }
                                    else
                                    {
                                        this.jobs[nbjob - 1].copy(Typelog);
                                    }
                                }
                                else
                                {
                                    view.errorMenu();
                                    break;
                                }
                                
                            }

                            view.completedSave(nbjob);

                            break;
                        }

                    // Settings menu
                    case 4:
                        {
                            int settings = view.Settings();

                            if (settings == 3)
                            {
                                break;
                            }

                            else if (settings == 2)
                            {
                                int language = view.changeLanguage();
                                if (language == 1)
                                {
                                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");
                                }
                                else if (language == 2)
                                {
                                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
                                }
                                break;
                            }

                            else if (settings == 1)
                            {
                                int logs = view.Logs();
                                if (logs == 1)
                                {
                                    Typelog = false;
                                }
                                else if (logs == 2)
                                {
                                    Typelog = true;
                                }
                                break;
                            }

                            break;
                        }

                    default:
                        break;

                } //end switch

            } while (menu != 5);
        }

        /// <summary>
        /// Method to test the creation of a job and the backups 
        /// </summary>
        public void testCreateJob()
        {
            Models.job job1 = new Models.job("TEST 1", @"\\PC-SHERIDAN\c$\Users\Sheridan\Desktop\test doc", @"\\PC-SHERIDAN\c$\Users\Sheridan\Desktop\test doc1", false);
            Console.WriteLine(job1.Name);
            Console.WriteLine(job1.PathSource);
            Console.WriteLine(job1.PathDestination);
            Console.WriteLine(job1.Complete);

            Console.WriteLine(job1.verifExist(job1.PathSource));

            Console.WriteLine(job1.calculNbFiles(job1.PathSource));

            Console.WriteLine(job1.calculSizeFolder(job1.PathSource));

            Console.WriteLine(job1.copy(Typelog));
        }

        /// <summary>
        /// Method to update job names in the header
        /// </summary>
        public void updateHeader()
        {
            view.JobsName = new List<string>();

            // Loop to display each jobs name
            for (int i = 0; i < nbJobMax; i++)
            {
                string name = this.jobs[i].Name;

                view.JobsName.Add(name);
            }
        }

        /// <summary>
        /// Method to create a job
        /// </summary>
        /// <param name="numberJob">Number of the job</param>
        public void createJob(int numberJob)
        {
            this.jobs[numberJob - 1].Name = view.CreateJobName;
            this.jobs[numberJob - 1].PathSource = view.CreateJobSource;
            this.jobs[numberJob - 1].PathDestination = view.CreateJobDestination;

            if (view.CreateJobType)
            {
                this.jobs[numberJob - 1].Complete = true;
            }
            else
            {
                this.jobs[numberJob - 1].Complete = false;
            }

            updateHeader(); //Update the header

            serializeJob(); //Serialization jobs 
            exportConfig(); //Export jobs
        }

        /// <summary>
        /// Method to serialize jobs
        /// </summary>
        public void serializeJob()
        {
            //Convert the table to suit the json format (in string)
            jsonString = JsonConvert.SerializeObject(jobs, Formatting.Indented);
        }

        /// <summary>
        /// Method to deserialize the json string
        /// </summary>
        public void deserializeJob()
        {
            var listJob = JsonConvert.DeserializeObject<List<Models.job>>(jsonString);
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
            using (var streamWriter = new StreamWriter(pathFilesEasySave + @"\configJob.json"))
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
            if (!File.Exists(pathFilesEasySave + @"\configJob.json"))
            {

                serializeJob(); //Serialization jobs 
                exportConfig(); //Export jobs
            }
            else
            {
                //StreamReader instance to read text from a file
                using (var streamReader = new StreamReader(pathFilesEasySave + @"\configJob.json"))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        //Deserialization and import into the job table
                        jobs = serializer.Deserialize<List<Models.job>>(jsonReader);
                    }
                }
            }
        }

        /// <summary>
        /// Method to delete job
        /// </summary>
        /// <param name="numberJob"></param>
        public void deleteJob(int numberJob)
        {
            this.jobs[numberJob - 1].Name = null;
            this.jobs[numberJob - 1].PathSource = null;
            this.jobs[numberJob - 1].PathDestination = null;
            this.jobs[numberJob - 1].Complete = false;

            updateHeader(); //Update the job list

            serializeJob(); //Serialization jobs 
            exportConfig(); //Export jobs
        }

        #endregion
    }
}
