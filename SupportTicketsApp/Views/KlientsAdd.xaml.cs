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
using System.Windows.Shapes;

namespace SupportTicketsApp.Views
{
    /// <summary>
    /// Логика взаимодействия для KlientsAdd.xaml
    /// </summary>
    public partial class KlientsAdd : Window
    {
        public KlientsAdd()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateKlientButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
