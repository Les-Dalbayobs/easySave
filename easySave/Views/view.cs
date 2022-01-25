/// \file view.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 0.1
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
        #endregion

        #region properties
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
        /// Basic application menu
        /// </summary>
        /// <returns>Choice of main menu</returns>
        public int displayMenu()
        {
            int menu = 10; //Choice of menu

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
            //Displaying the menu
            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" 6.Exit");

            Console.Write(" Choose job number and press enter : ");
            //End of menu display

            //Retrieves the value entered by the user and converts it to int
            int job = Convert.ToInt32(Console.ReadLine());

            return job; //Returns the menu choice
        }

        /// <summary>
        /// Method for entering job information
        /// </summary>
        public void create()
        {
            //Displaying the menu
            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" Fill in the fields");

            Console.Write(" Job name : ");
            Console.ReadLine();

            Console.Write(" Source path : ");
            Console.ReadLine();

            Console.Write(" Destination path : ");
            Console.ReadLine();

            Console.WriteLine(" -----------TYPE----------");
            Console.WriteLine(" Choose the type :");
            Console.WriteLine(" 1.Complete");
            Console.WriteLine(" 2.Differential");
            Console.Write(" Type : ");
            Console.ReadLine();
            //End of menu display
        }


        /// <summary>
        /// Method to validate job information
        /// </summary>
        /// <returns>Returns if the information entered is correct</returns>
        public int confirmCreate()
        {
            int valid = 10; //Choice of menu

            //Displaying the menu
            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" Checking the information");

            Console.WriteLine(" Job name : ");

            Console.WriteLine(" Source path : ");

            Console.WriteLine(" Destination path : ");

            Console.WriteLine(" Type : ");

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
            //Displaying the menu
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" 6.Exit");

            Console.Write(" Choose job number and press enter : ");
            //End of menu display

            //Retrieves the value entered by the user and converts it to int
            int job = Convert.ToInt32(Console.ReadLine());

            return job; //Returns the menu choice
        }

        /// <summary>
        /// Method for validating the chosen job
        /// </summary>
        /// <returns>Returns if the information entered is correct</returns>
        public int confirmDelete()
        {
            int valid; //Choice of menu

            //Display of the error message
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" Do you really want to delete the job n°.");

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
                    int menuError = confirmDelete(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = confirmDelete(); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }

            return valid; //Returns the menu choice
        }

        /// <summary>
        /// Method to confirm job deletion
        /// </summary>
        public void finishDelete()
        {
            //Display of the error message
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" Job n°.. delete");
            Console.Write(" Press enter to continue");
            //End display

            Console.ReadLine(); //Waits for the user to press enter
        }

        /// <summary>
        /// Method to select the job to save
        /// </summary>
        /// <returns>Selected job for save</returns>
        public int chooseSave()
        {
            int job; // Initialize job variable

            //Displaying the menu
            Console.WriteLine(" ----------SAVE JOB---------");
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
                    int menuError = confirmSave(); //Restarts the menu display and saves the return value

                    return menuError; //Returns the menu choice
                }
            }
            catch
            {
                errorMenu(); //Launch the error window
                int menuError = confirmSave(job); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }

            return job; //Returns the menu choice

        }

        public int confirmSave(int job)
        {
            return job;
        }

            #endregion
    }
}