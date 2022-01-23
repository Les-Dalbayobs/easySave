/// \file viewModel.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 0.1
/// \date 23/01/2022

using System;
using System.Collections.Generic;
using System.Text;

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
        Views.view view = new Views.view();

        public viewModel()
        {
            menu();
        }

        public void menu()
        {
            int menu; 
                
            menu = view.displayMenu();

            switch (menu)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                default:
                    break;
            } //end switch

        }
    }


}
