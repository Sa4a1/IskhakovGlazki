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

namespace IskhakovGlazki_Saves
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ListAgentBtn_Click(object sender, RoutedEventArgs e)
        {
          MainFrame.Navigate(new AgentPage());
            Manager.MainFrame = MainFrame;
<<<<<<< HEAD
            ListAgentBtn.Visibility = Visibility.Hidden;
=======
            if (Manager.MainFrame.CanGoBack)
                ListAgentBtn.Visibility = Visibility.Hidden;
>>>>>>> 4e509afc1200ddc2edc3409183024c47bdeeba7c
        }
    }
}
