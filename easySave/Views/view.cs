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

        public int displayMenu()
        {
            Console.WriteLine(" ------------MENU-----------");
            int menu = 5;
            Console.WriteLine(" 1.Create");
            Console.WriteLine(" 2.Delete");
            Console.WriteLine(" 3.Save");
            Console.WriteLine(" 4.Language");
            Console.WriteLine(" 5.Exit");
            Console.WriteLine(" ---------------------------");
            Console.Write(" Chosse number and press enter : ");
            menu = Convert.ToInt32(Console.ReadLine());

            return menu;
        }

        #endregion


    }
}