/// \file view.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 1.1
/// \date 23/01/2022

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

/// <summary>
/// View namespace
/// </summary>
namespace easySave.Views
{
    /// <summary>
    /// Class with all project views
    /// </summary>
    class view
    {
        #region attributes

        List<string> jobsName;

        string createJobName; //Storing the name of the job the user wants to create 
        string createJobSource; //Storing the source of the job the user wants to create 
        string createJobDestination; //Storing the destination of the job the user wants to create 
        bool createJobType; //Storing the type of the job the user wants to create 

        ResourceManager rm;
        #endregion

        #region properties

        /// <summary>
        /// Getter setter of the name entered by the user
        /// </summary>
        public string CreateJobName { get => createJobName; set => createJobName = value; }

        /// <summary>
        /// Getter setter of the source entered by the user
        /// </summary>
        public string CreateJobSource { get => createJobSource; set => createJobSource = value; }

        /// <summary>
        /// Getter setter of the destination entered by the user
        /// </summary>
        public string CreateJobDestination { get => createJobDestination; set => createJobDestination = value; }

        /// <summary>
        /// Getter setter of the type entered by the user
        /// </summary>
        public bool CreateJobType { get => createJobType; set => createJobType = value; }


        public List<string> JobsName { get => jobsName; set => jobsName = value; }

        #endregion

        #region constructor
        /// <summary>
        /// Constructor of the view class
        /// </summary>
        public view()
        {
           rm = new ResourceManager("easySave.Resources.strings", Assembly.GetExecutingAssembly());
           Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
        }
        #endregion

        #region methods

        /// <summary>
        /// Displays the program header
        /// </summary>
        public void header()
        {
            Console.Clear(); //Clean console

            int nbJobs = 1;

            //Displaying the menu
            Console.WriteLine(rm.GetString("easySave"));

            foreach (string job in jobsName)
            {
                Console.WriteLine(rm.GetString("header") + nbJobs.ToString() +" : " + job);
                nbJobs++;
            }
        }

        /// <summary>
        /// Basic application menu
        /// </summary>
        /// <returns>Choice of main menu</returns>
        public int displayMenu()
        {
            int menu = 10; //Choice of menu

            header(); //Display header

            //Displaying the menu
            Console.Write(rm.GetString("menu"));
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                menu = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (menu > 5)
                {
                    errorMenu(); //Launch the error window
                    int menuError = displayMenu(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); // Launch the error window
                int menuError = displayMenu(); // Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }

            return menu; //Returns the menu choice
        }

        /// <summary>
        /// Method for choosing the job number to create the job
        /// </summary>
        /// <returns>Job chosen for creation</returns>
        public int chooseCreate()
        {
            header(); //Display header

            //Displaying the menu
            Console.Write(rm.GetString("menuCreate"));
            //End of menu display

            int job;

            try
            {
                //Retrieves the value entered by the user and converts it to int
                job = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (job > 6 || job <= 0)
                {
                    errorMenu(); //Launch the error window
                    int menuError = chooseCreate(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = chooseCreate(); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }
            

            return job; //Returns the menu choice
        }

        /// <summary>
        /// Method for entering job information
        /// </summary>
        public void create(bool infoDisplay)
        {
            header(); //Display header

            //Displaying the menu
            Console.Write(rm.GetString("createName"));
            //Checks if the information already filled in should be displayed
            if (infoDisplay) 
            {
                Console.WriteLine(this.createJobName);
            }
            //If we don't display it, we store the value given by the user
            else
            {
                this.createJobName = Console.ReadLine();
            }


            Console.Write(rm.GetString("createSource"));
            //Checks if the information already filled in should be displayed
            if (infoDisplay)
            {
                Console.WriteLine(this.createJobSource);
            }
            //If we don't display it, we store the value given by the user
            else
            {
                this.createJobSource = Console.ReadLine();
            }
            

            Console.Write(rm.GetString("createDestination"));
            //Checks if the information already filled in should be displayed
            if (infoDisplay)
            {
                Console.WriteLine(this.createJobDestination);
            }
            //If we don't display it, we store the value given by the user
            else
            {
                this.createJobDestination = Console.ReadLine();
            }

            Console.Write(rm.GetString("createType"));

            string type = Console.ReadLine();

            if(type == "1") //Complete
            {
                this.createJobType = true;
            }
            else if (type == "2") //Differential
            {
                this.createJobType = false;
            }
            else //Incorrect
            {
                //Launch the error window
                errorMenu();

                //Restarts the display but with the information already filled in
                create(true);
            }
            //End of menu display
        }

        /// <summary>
        /// Method to validate job information
        /// </summary>
        /// <returns>Returns if the information entered is correct</returns>
        public int confirmCreate()
        {
            int valid = 10; //Choice of menu

            header(); //Display header

            //Displaying the menu
            Console.Write(rm.GetString("createValidation"));
            //Displays the value entered by the user
            Console.WriteLine(this.createJobName);

            Console.Write(rm.GetString("createSource"));
            //Displays the value entered by the user
            Console.WriteLine(this.createJobSource);

            Console.Write(rm.GetString("createDestination"));
            //Displays the value entered by the user
            Console.WriteLine(this.createJobDestination);

            Console.Write(" Type : ");
            //Check if the user has chosen differential or complete
            if (this.createJobType)
            {
                Console.WriteLine(rm.GetString("complete"));
            }
            else
            {
                Console.WriteLine(rm.GetString("differential"));
            }

            Console.Write(rm.GetString("validation"));
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                valid = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (valid > 2)
                {
                    errorMenu(); //Launch the error window
                    int menuError = confirmCreate(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = confirmCreate(); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }

            return valid; //Returns the menu choice
        }

        /// <summary>
        /// Method to confirm job creation
        /// </summary>
        public void finishCreate()
        {
            header(); //Display header

            //Displaying the menu
            Console.Write(rm.GetString("createFinish"));
            //End display

            Console.ReadLine(); //Waits for the user to press enter
        }

        /// <summary>
        /// Method of displaying the errors concerning a menu
        /// </summary>
        public void errorMenu()
        {
            Console.Clear(); //Clean console

            //Display of the error message
            Console.Write(rm.GetString("errorMenu"));
            //End display

            Console.ReadLine(); //Waits for the user to press enter

            Console.Clear(); //Clean console
        }

        /// <summary>
        /// Method for selecting the job number to delete the job
        /// </summary>
        /// <returns>Selected job for deletion</returns>
        public int chooseDelete()
        {
            int job; // Job number choosed

            header(); //Display header

            //Displaying the menu
            Console.Write(rm.GetString("menuDelete"));
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                job = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (job < 1 || job > 6)
                {
                    errorMenu(); //Launch the error window
                    int menuError = chooseDelete(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = chooseDelete(); //Restarts the menu display

                return menuError; //Returns the choice menu
            }

            return job; //Returns the job choosed

        }

        /// <summary>
        /// Method for validating the chosen job
        /// </summary>
        /// <returns>Returns if the information entered is correct</returns>
        public int confirmDelete(int jobNumber)
        {
            int valid; //Choice of menu

            header(); //Display header

            //Display of the error message
            Console.WriteLine(rm.GetString("deleteConfirm") + jobNumber);

            Console.Write(rm.GetString("validation"));
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                valid = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (valid > 2)
                {
                    errorMenu(); //Launch the error window
                    int menuError = confirmDelete(jobNumber); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = confirmDelete(jobNumber); //Restarts the menu display and saves the return value

                return menuError; //Returns the choice menu
            }

            return valid; //Returns the menu choice
        }

        /// <summary>
        /// Method to confirm job deletion
        /// </summary>
        public void finishDelete(int jobNumber)
        {
            header(); //Display header

            //Display of the error message
            Console.WriteLine(rm.GetString("deleteFinish") + jobNumber + rm.GetString("deleteFinish2"));        
            //End display

            Console.ReadLine(); //Waits for the user to press enter
        }

        /// <summary>
        /// Method to choose the save option
        /// </summary>
        /// <returns></returns>
        public int chooseSave()
        {
            int job; //Choice of menu

            Console.Clear();
            header();  // Display the list of jobs

            Console.Write(rm.GetString("menuSave"));
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                job = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (job > 7 || job < 1)
                {
                    errorMenu(); //Launch the error window
                    int menuError = chooseSave(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = chooseSave(); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }
          
            return job;
        }

        /// <summary>
        /// Method to confirm the job to save
        /// </summary>
        /// <returns>Selected job for save</returns>
        public int confirmSave(int nbjob) 
        {
            int menu; //Choice of menu

            header();

            //Display of the error message
            if (nbjob == 6)
            {
                Console.WriteLine(rm.GetString("saveAll"));
            }
            else
            {
                Console.WriteLine(rm.GetString("saveOne") + nbjob + rm.GetString("saveOne2"));
            }

            Console.Write(rm.GetString("validation"));
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                menu = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (menu > 2 || menu < 1)
                {
                    errorMenu(); //Launch the error window
                    int menuError = confirmSave(nbjob); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = confirmSave(nbjob); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }

            return menu;
        }

        /// <summary>
        /// Method to show progress of saving, and then exit
        /// </summary>
        /// <returns>Nothing</returns>
        public void completedSave(int nbjob)
        {
            //Display of the confirmation message
            Console.Clear();

            header();

            // Display for finished save of all jobs
            if (nbjob == 6)
            {
                Console.WriteLine(rm.GetString("saveFinishAll"));
            }
            // Display finished save message
            else
            {
                Console.WriteLine(rm.GetString("saveFinishOne") + nbjob + rm.GetString("saveFinishOne2"));
            }
            //End of display

            Console.ReadLine(); //Waits for the user to press enter
        }

        /// <summary>
        /// Method to display language menu
        /// </summary>
        /// <returns></returns>
        public int changeLanguage()
        {
            int language; // Initialize language variable

            Console.Clear();

            Console.Write(rm.GetString("languageMenu"));
            // End of menu display

            // Try catch to manage typing errors
            try
            {
                // Retrieves the value entered by the user and converts it to int
                language = Convert.ToInt32(Console.ReadLine());

                // If - to handle typing errors
                if (language > 3 || language < 1)
                {
                    errorMenu(); // Launch the error window
                    int menuError = changeLanguage(); // Restarts the menu display and saves the return value

                    return menuError; // Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); // Launch the error window
                int menuError = changeLanguage(); // Restarts the menu display and saves the return value

                return menuError; // Returns the menu choice
            }

            return language;
            //End of menu display
        }

        /// <summary>
        /// Method to display save error
        /// </summary>
        /// <param name="nbJob"></param>
        /// <param name="sourceOrDestination"></param>
        public void errorSave(int nbJob, bool sourceOrDestination)
        {
            header();

            Console.WriteLine(rm.GetString("errorSave"));
            if (sourceOrDestination)
            {
                Console.WriteLine(rm.GetString("errorSaveSource") + nbJob);
            }
            else
            {
                Console.WriteLine(rm.GetString("errorSaveDestination") + nbJob);
            }

            Console.WriteLine(rm.GetString("pressEnter"));
            //End display

            Console.ReadLine();
        }

        /// <summary>
        /// Method to display the name of the solution at launch
        /// </summary>
        public void Title()
        {
            Console.WriteLine(@"        ______                    _____                      ");
            Console.WriteLine(@"       / ____/____ _ _____ __  __/ ___/ ____ _ _   __ ___     ");
            Console.WriteLine(@"      / __/  / __ `// ___// / / /\__ \ / __ `/| | / // _ \   ");
            Console.WriteLine(@"     / /___ / /_/ /(__  )/ /_/ /___/ // /_/ / | |/ //  __/  ");
            Console.WriteLine(@"    /_____/ \__,_//____/ \__, //____/ \__,_/  |___/ \___/    ");
            Console.WriteLine(@"                        /____/                                  ");
        }

        /// <summary>
        /// Method to create the settings view
        /// </summary>
        public int Settings()
        {
            int settings;

            header();

            Console.WriteLine(rm.GetString("settings"));

            // End of menu display

            // Try catch to manage typing errors
            try
            {
                // Retrieves the value entered by the user and converts it to int
                settings = Convert.ToInt32(Console.ReadLine());

                // If - to handle typing errors
                if (settings > 3 || settings < 1)
                {
                    errorMenu(); // Launch the error window
                    int menuError = Settings(); // Restarts the menu display and saves the return value

                    return menuError; // Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); // Launch the error window
                int menuError = Settings(); // Restarts the menu display and saves the return value

                return menuError; // Returns the menu choice
            }

            return settings;
            //End of menu display
        }


        /// <summary>
        /// Method to create the settings view
        /// </summary>
        public int Logs()
        {
            int logs;

            header();

            Console.WriteLine(rm.GetString("logs"));

            // End of menu display

            // Try catch to manage typing errors
            try
            {
                // Retrieves the value entered by the user and converts it to int
                logs = Convert.ToInt32(Console.ReadLine());

                // If - to handle typing errors
                if (logs > 3 || logs < 1)
                {
                    errorMenu(); // Launch the error window
                    int menuError = Logs(); // Restarts the menu display and saves the return value

                    return menuError; // Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); // Launch the error window
                int menuError = Logs(); // Restarts the menu display and saves the return value

                return menuError; // Returns the menu choice
            }

            return logs;
            //End of menu display
        }

        #endregion
    }
}