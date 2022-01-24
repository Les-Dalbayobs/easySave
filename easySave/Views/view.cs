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
        /// <returns name="menu">Choice of main menu</returns>
        public int displayMenu()
        {
            int menu = 10;

            Console.WriteLine(" ------------MENU-----------");

            Console.WriteLine(" 1.Create");
            Console.WriteLine(" 2.Delete");
            Console.WriteLine(" 3.Save");
            Console.WriteLine(" 4.Language");
            Console.WriteLine(" 5.Exit");

            Console.WriteLine(" ---------------------------");

            Console.Write(" Choose number and press enter : ");

            try
            {
                menu = Convert.ToInt32(Console.ReadLine());
                if (menu > 5)
                {
                    errorMenu();
                    displayMenu();
                }
            }
            catch
            {
                errorMenu();
                displayMenu();
            }


            return menu;
        }

        public int chooseCreate()
        {

            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" 6.Exit");
            
            Console.Write(" Choose job number and press enter : ");
            
            int job = Convert.ToInt32(Console.ReadLine());

            return job;
        }

        public void create()
        {
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
        }

        public int confirmCreate()
        {
            int valid = 10;

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

            try
            {
                valid = Convert.ToInt32(Console.ReadLine());
                if (valid > 2)
                {
                    errorMenu();
                    confirmCreate();
                }
            }
            catch
            {
                errorMenu();
                confirmCreate();
            }

            return valid;
        }

        public int finishCreate()
        {
            int choose = 10;

            Console.WriteLine(" ---------CREATE JOB--------");
            Console.WriteLine(" Job created");
            Console.WriteLine(" 1.Back to the home page");
            Console.WriteLine(" 2.Back to the job creation page");

            Console.Write(" Choose number and press enter : ");
            
            try
            {
                choose = Convert.ToInt32(Console.ReadLine());
                if (choose > 2)
                {
                    errorMenu();
                    finishCreate();
                }
            }
            catch
            {
                errorMenu();
                finishCreate();
            }

            return choose;
        }

        public void errorMenu()
        {
            Console.Clear();

            Console.WriteLine(" Error, please try again");

            Console.Write(" Press enter to continue");
            Console.ReadLine();

            Console.Clear();
        }
        #endregion


    }
}