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
        /// Creation of job objects
        /// </summary>
        Models.job job1 = new Models.job();
        Models.job job2 = new Models.job();
        Models.job job3 = new Models.job();
        Models.job job4 = new Models.job();
        Models.job job5 = new Models.job();

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

            for (int i = 0; i < nbJobMax; i++)
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
                menu = view.displayMenu();

                switch (menu)
                {
                    case 1:
                        {
                            int nbJob = view.chooseCreate();
                            if (nbJob == 6)
                            {
                                break;
                            }

                            view.create(false);

                            int valid = view.confirmCreate();
                            if (valid == 2)
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
                            if (nbJob == 6)
                            {
                                break;
                            }

                            int valid = view.confirmDelete(nbJob);
                            if (valid == 2)
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

                    case 3:
                        {
                            int nbjob = view.chooseSave();

                            int valid = view.confirmSave(nbjob);
                            if (valid == 2)
                            {
                                break;
                            }

                            if (nbjob == 6)
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
                                            job.copy();
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
                                    this.jobs[nbjob - 1].copy();
                                }
                            }

                            view.completedSave(nbjob);

                            break;
                        }
                    case 4:
                        view.changeLanguage();

                        break;

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
            Console.WriteLine(job1.TypeSave);

            Console.WriteLine(job1.verifExist(job1.PathSource));

            Console.WriteLine(job1.calculNbFiles(job1.PathSource));

            Console.WriteLine(job1.calculSizeFolder(job1.PathSource));

            Console.WriteLine(job1.copy());
        }

        /// <summary>
        /// Method to update job names in the header
        /// </summary>
        public void updateHeader()
        {
            view.JobsName = new List<string>();

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
                this.jobs[numberJob - 1].TypeSave = true;
            }
            else
            {
                this.jobs[numberJob - 1].TypeSave = false;
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
        /// Method to copy the import of json into jobs
        /// </summary>
        public void copyImportConfig()
        {
            this.job1 = jobs[0];
            this.job2 = jobs[1];
            this.job3 = jobs[2];
            this.job4 = jobs[3];
            this.job5 = jobs[4];
        }

        public void deleteJob(int numberJob)
        {
            this.jobs[numberJob - 1].Name = null;
            this.jobs[numberJob - 1].PathSource = null;
            this.jobs[numberJob - 1].PathDestination = null;
            this.jobs[numberJob - 1].TypeSave = false;

            updateHeader(); //Update the job list

            serializeJob(); //Serialization jobs 
            exportConfig(); //Export jobs
        }
        #endregion
    }
}
