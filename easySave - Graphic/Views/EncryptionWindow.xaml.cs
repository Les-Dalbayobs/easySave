using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace easySave___Graphic.Views
{
    /// <summary>
    /// Logique d'interaction pour EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {

        public EncryptionWindow()
        {
            InitializeComponent();
        }

       
        public void Cancel_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Style_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }
    }

}
