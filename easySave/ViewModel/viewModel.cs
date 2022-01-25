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
        Views.view view = new Views.view();
        #endregion

        #region properties
        #endregion

        #region constructor
        public viewModel()
        {
            testCreateJob();
        }
        #endregion

        #region methods
        /// <summary>
        /// Main program loop
        /// </summary>
        public void menu()
        {
            int menu; 
                
            menu = view.displayMenu();

            switch (menu)
            {
                case 1:
                    view.chooseCreate();
                    view.create();
                    view.confirmCreate();
                    view.finishCreate();
                    break;

                case 2:
                    view.chooseDelete();
                    view.confirmDelete();
                    view.finishDelete();
                    break;

                case 3:
                    break;

                case 4:
                    break;

                default:
                    break;
            } //end switch
        }

        public void testCreateJob()
        {
            Models.job job1 = new Models.job("TEST 1", @"\\LP-SHERIDAN\c$\Users\sheri\Desktop\testdoc", @"\\LP-SHERIDAN\c$\Users\sheri\Desktop\testdoc1", true);
            Console.WriteLine(job1.Name);
            Console.WriteLine(job1.PathSource);
            Console.WriteLine(job1.PathDestination);
            Console.WriteLine(job1.TypeSave);

            Console.WriteLine(job1.verifExist());

            Console.WriteLine(job1.calculSize());
        }

        #endregion
    }


}
