﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Resources;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasyConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel.ViewModel mainWindowsViewsModel = new ViewModel.ViewModel();

            this.DataContext = mainWindowsViewsModel;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Connect.IsEnabled = false;
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            Connect.IsEnabled = true;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        } 
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
   
}
