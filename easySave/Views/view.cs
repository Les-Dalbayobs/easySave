/// \file view.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 0.4
/// \date 23/01/2022

using System;
using System.Collections.Generic;
using System.Text;

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
        string job1Name; //Stored the name of the job1
        string job2Name; //Stored the name of the job2
        string job3Name; //Stored the name of the job3
        string job4Name; //Stored the name of the job4
        string job5Name; //Stored the name of the job5

        string createJobName; //Storing the name of the job the user wants to create 
        string createJobSource; //Storing the source of the job the user wants to create 
        string createJobDestination; //Storing the destination of the job the user wants to create 
        bool createJobType; //Storing the type of the job the user wants to create 

        int jobNumber; // Initialize Job number variable


        #endregion

        #region properties

        /// <summary>
        /// Getter setter of job1 name
        /// </summary>
        public string Job1Name { get => job1Name; set => job1Name = value; }

        /// <summary>
        /// Getter setter of job2 name
        /// </summary>
        public string Job2Name { get => job2Name; set => job2Name = value; }

        /// <summary>
        /// Getter setter of job3 name
        /// </summary>
        public string Job3Name { get => job3Name; set => job3Name = value; }

        /// <summary>
        /// Getter setter of job4 name
        /// </summary>
        public string Job4Name { get => job4Name; set => job4Name = value; }

        /// <summary>
        /// Getter setter of job5 name
        /// </summary>
        public string Job5Name { get => job5Name; set => job5Name = value; }

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

        /// <summary>
        /// Getter setter of the job number entered by the user
        /// </summary>
        public int JobNumber { get => jobNumber; set => jobNumber = value; }

        #endregion

        #region constructor
        /// <summary>
        /// Constructor of the view class
        /// </summary>
        public view()
        {
        }
        #endregion

        #region methods

        /// <summary>
        /// Displays the program header
        /// </summary>
        public void header()
        {
            Console.Clear(); //Clean console

            //Displaying the menu
            Console.WriteLine(" ----------EASYSAVE---------");
            Console.WriteLine(" Job n°1 : " + this.job1Name);
            Console.WriteLine(" Job n°2 : " + this.job2Name);
            Console.WriteLine(" Job n°3 : " + this.job3Name);
            Console.WriteLine(" Job n°4 : " + this.job4Name);
            Console.WriteLine(" Job n°5 : " + this.job5Name);
            //End of menu display
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
            Console.WriteLine(" ------------MENU-----------");

            Console.WriteLine(" 1.Create");
            Console.WriteLine(" 2.Delete");
            Console.WriteLine(" 3.Save");
            Console.WriteLine(" 4.Language");
            Console.WriteLine(" 5.Exit");

            Console.WriteLine(" ---------------------------");

            Console.Write(" Choose number and press enter : ");
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
            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" 6.Exit");

            Console.Write(" Choose job number and press enter : ");
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
            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" Fill in the fields");

            Console.Write(" Job name : ");
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
            

            Console.Write(" Source path : ");
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
            

            Console.Write(" Destination path : ");
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

            Console.WriteLine(" -----------TYPE----------");
            Console.WriteLine(" Choose the type :");
            Console.WriteLine(" 1.Complete");
            Console.WriteLine(" 2.Differential");
            Console.Write(" Type : ");

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
            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" Checking the information");

            Console.Write(" Job name : ");
            //Displays the value entered by the user
            Console.WriteLine(this.createJobName);

            Console.Write(" Source path : ");
            //Displays the value entered by the user
            Console.WriteLine(this.createJobSource);

            Console.Write(" Destination path : ");
            //Displays the value entered by the user
            Console.WriteLine(this.createJobDestination);

            Console.Write(" Type : ");
            //Check if the user has chosen differential or complete
            if (this.createJobType)
            {
                Console.WriteLine("Complete");
            }
            else
            {
                Console.WriteLine("Differential");
            }

            Console.WriteLine(" ---------VALIDATION--------");
            Console.WriteLine(" 1.Yes");
            Console.WriteLine(" 2.No");


            Console.Write(" Choose number and press enter : ");
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
            Console.WriteLine(" ---------CREATE JOB--------");

            Console.WriteLine(" Job created");
            Console.Write(" Press enter to continue");
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
            Console.WriteLine(" Error, please try again");
            Console.Write(" Press enter to continue");
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
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" 6.Exit");

            Console.Write(" Choose job number and press enter : ");
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
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" Do you really want to delete the job n°" + jobNumber);

            Console.WriteLine(" ---------VALIDATION--------");
            Console.WriteLine(" 1.Yes");
            Console.WriteLine(" 2.No");


            Console.Write(" Choose number and press enter : ");
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
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" Job n°" + jobNumber + " deleted");
            Console.Write(" Press enter to continue");
            //End display

            Console.ReadLine(); //Waits for the user to press enter
        }

        public int chooseSave()
        {
            int job; //Choice of menu

            header();  

            //Displaying the menu
            Console.WriteLine(" ----------SAVE JOB---------");

            Console.Write(" Choose job number and press enter : ");
            //End of menu display

            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                job = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (job > 6 || job < 1)
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
            Console.WriteLine(" ----------SAVE JOB---------");
            if (nbjob == 6)
            {
                Console.WriteLine("You have selected all jobs, Are you sure ?");
            }
            else
            {
                Console.WriteLine("You selected job number " + nbjob + ", Are you sure ?");
            }

            Console.WriteLine(" ---------VALIDATION--------");
            Console.WriteLine(" 1.Yes");
            Console.WriteLine(" 2.No");


            Console.Write(" Choose number and press enter : ");
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

            header();

            Console.WriteLine(" ----------SAVE JOB---------");
            if (nbjob == 6)
            {
                Console.WriteLine("All jobs were saved");
            }
            else
            {
                Console.WriteLine(" Job n°" + nbjob + " successfully saved");
            }
        
            Console.Write(" Press enter to continue");
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

            Console.WriteLine(" ------CHANGE LANGUAGE------");
            Console.WriteLine(" 1. Français");
            Console.WriteLine(" 2. English \n");
            Console.WriteLine(" 6. Exit");
            Console.WriteLine("");
            Console.Write(" Choose number and press enter : ");
            // End of menu display

            // Try catch to manage typing errors
            try
            {
                // Retrieves the value entered by the user and converts it to int
                language = Convert.ToInt32(Console.ReadLine());

                // If - to handle typing errors
                if ((language > 2 || language < 1) && language != 6)
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

            Console.Clear();

            Console.WriteLine(" ---------VALIDATION--------");
            Console.WriteLine("Are you sure ?");
            Console.WriteLine(" 1.Yes");
            Console.WriteLine(" 2.No \n");


            Console.Write(" Choose number and press enter : ");
            //End of menu display

            int validation; // Initialize validation variable
            //Try catch to manage typing errors
            try
            {
                //Retrieves the value entered by the user and converts it to int
                validation = Convert.ToInt32(Console.ReadLine());

                //if - to handle typing errors
                if (validation > 2 || validation < 1)
                {
                    errorMenu(); //Launch the error window
                    int menuError = changeLanguage(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = changeLanguage(); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }

            if (validation == 1) {
                return language; // Return the value of choosed language
            }
            else
            {
                int returnMenu = changeLanguage(); // Return to menu (no validation)
                return returnMenu;
            }
        }

        public void errorSave(int nbJob, bool sourceOrDestination)
        {
            header();

            Console.WriteLine(" ---------ERROR SAVE--------");
            Console.WriteLine(" No backup executed");
            if (sourceOrDestination)
            {
                Console.WriteLine(" Error in the source path of job n°" + nbJob);
            }
            else
            {
                Console.WriteLine(" Error in the destination path of job n°" + nbJob);
            }

            Console.Write(" Press enter to continue");
            //End display

            Console.ReadLine();

        }
        #endregion
    }
}