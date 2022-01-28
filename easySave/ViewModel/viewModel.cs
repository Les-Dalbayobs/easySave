/// \file viewModel.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 0.1
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
        /// 
        /// </summary>
        Views.view view = new Views.view();

        /// <summary>
        /// 
        /// </summary>
        List<Models.job> jobs;

        /// <summary>
        /// 
        /// </summary>
        Models.job job1 = new Models.job();
        Models.job job2 = new Models.job();
        Models.job job3 = new Models.job();
        Models.job job4 = new Models.job();
        Models.job job5 = new Models.job();

        /// <summary>
        /// 
        /// </summary>
        string jsonString;

        /// <summary>
        /// 
        /// </summary>
        JsonSerializer serializer;

        /// <summary>
        /// 
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
            importConfig();
            copyImportConfig();

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
                        
                    case 2:
                        view.chooseDelete();
                        view.confirmDelete();
                        view.finishDelete();
                        break;
                    
                    case 3:
                        view.chooseSave();
                        view.confirmSave();
                        view.completedSave();
                        break;

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

            Console.WriteLine(job1.calculSize(job1.PathSource));

            Console.WriteLine(job1.copy());
        }

        /// <summary>
        /// Method to update job names in the header
        /// </summary>
        public void updateHeader()
        {
            view.Job1Name = this.job1.Name;
            view.Job2Name = this.job2.Name;
            view.Job3Name = this.job3.Name;
            view.Job4Name = this.job4.Name;
            view.Job5Name = this.job5.Name;
        }

        /// <summary>
        /// Method to create a job
        /// </summary>
        /// <param name="numberJob">Number of the job</param>
        public void createJob(int numberJob)
        {
            if (numberJob == 1)
            {
                this.job1.Name = view.CreateJobName;
                this.job1.PathSource = view.CreateJobSource;
                this.job1.PathDestination = view.CreateJobDestination;
                if (view.CreateJobType)
                {
                    this.job1.TypeSave = true;
                }
                else
                {
                    this.job1.TypeSave = false;
                }
            }

            if (numberJob == 2)
            {
                this.job2.Name = view.CreateJobName;
                this.job2.PathSource = view.CreateJobSource;
                this.job2.PathDestination = view.CreateJobDestination;
                if (view.CreateJobType)
                {
                    this.job2.TypeSave = true;
                }
                else
                {
                    this.job2.TypeSave = false;
                }
            }

            if (numberJob == 3)
            {
                this.job3.Name = view.CreateJobName;
                this.job3.PathSource = view.CreateJobSource;
                this.job3.PathDestination = view.CreateJobDestination;
                if (view.CreateJobType)
                {
                    this.job3.TypeSave = true;
                }
                else
                {
                    this.job3.TypeSave = false;
                }
            }

            if (numberJob == 4)
            {
                this.job4.Name = view.CreateJobName;
                this.job4.PathSource = view.CreateJobSource;
                this.job4.PathDestination = view.CreateJobDestination;
                if (view.CreateJobType)
                {
                    this.job4.TypeSave = true;
                }
                else
                {
                    this.job4.TypeSave = false;
                }
            }

            if (numberJob == 5)
            {
                this.job5.Name = view.CreateJobName;
                this.job5.PathSource = view.CreateJobSource;
                this.job5.PathDestination = view.CreateJobDestination;
                if (view.CreateJobType)
                {
                    this.job5.TypeSave = true;
                }
                else
                {
                    this.job5.TypeSave = false;
                }
            }

            updateHeader(); //Update the header

            serializeJob();
            exportConfig();
        }

        /// <summary>
        /// Method to serialize jobs
        /// </summary>
        public void serializeJob()
        {
            //Create or reset the job table
            this.jobs = new List<Models.job>();

            //Add the jobs in the table
            this.jobs.Add(this.job1);
            this.jobs.Add(this.job2);
            this.jobs.Add(this.job3);
            this.jobs.Add(this.job4);
            this.jobs.Add(this.job5);

            //Convert the table to suit the json format (in string)
            jsonString = JsonConvert.SerializeObject(jobs, Formatting.Indented);
        }

        /// <summary>
        /// Method to deserialize the json string
        /// </summary>
        public void deserializeJob()
        {
            var listJob = JsonConvert.DeserializeObject<List<Models.job>>(jsonString);

            /*foreach (var job in listJob)
            {
                Console.WriteLine(job.ToString());
            }*/
        }

        /// <summary>
        /// Method to export the json
        /// </summary>
        public void exportConfig()
        {
            if (!Directory.Exists(pathFilesEasySave))
            {
                Directory.CreateDirectory(pathFilesEasySave);
            }

            using (var streamWriter = new StreamWriter(pathFilesEasySave + @"\configJob.json"))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(jsonString));
                    //Console.WriteLine("Export Ok");
                }
            }
        }

        /// <summary>
        /// Method to import the json
        /// </summary>
        public void importConfig()
        {
            if (!Directory.Exists(pathFilesEasySave))
            {
                Directory.CreateDirectory(pathFilesEasySave);
            }
            if (!File.Exists(pathFilesEasySave + @"\configJob.json"))
            {
                serializeJob();
                exportConfig();
            }
            using (var streamReader = new StreamReader(pathFilesEasySave + @"\configJob.json"))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    jobs = serializer.Deserialize<List<Models.job>>(jsonReader);
                    /*foreach (var job in jobs)
                    {
                        Console.WriteLine(job.ToString());
                    }*/
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
        #endregion
    }
}
