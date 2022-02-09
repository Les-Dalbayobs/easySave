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
    /// Logique d'interaction pour DeleteJob.xaml
    /// </summary>
    public partial class DeleteJob : Window
    {
        public DeleteJob()
        {
            InitializeComponent();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}