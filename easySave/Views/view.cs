﻿/// \file view.cs
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
                int menuError = confirmCreate(); //Restarts the menu display and saves the return value

                return menuError; //Returns the menu choice
            }
            

            return job; //Returns the menu choice
        }

        /// <summary>
        /// Method for entering job information
        /// </summary>
        public void create()
        {
            header(); //Display header

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

            header(); //Display header

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
            header(); //Display header

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

            header(); //Display header

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
            header(); //Display header

            //Display of the error message
            Console.WriteLine(" ---------DELETE JOB--------");
            Console.WriteLine(" Job n°.. delete");
            Console.Write(" Press enter to continue");
            //End display

            Console.ReadLine(); //Waits for the user to press enter
        }

        #endregion

    }
}