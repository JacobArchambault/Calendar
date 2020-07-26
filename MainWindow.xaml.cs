﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsAreFilled(EventNameTextBox, StartTimeTextBox, LocationTextBox))
            {
                if (AreHours(StartTimeTextBox))
                {
                    WarningLabel.Content = "";
                }
                else
                {
                    WarningLabel.Content = "Invalid data. You must enter a time in format XX:XX in the hours box";
                }
            }
            else
            {
                WarningLabel.Content = "All non-optional fields must be filled";
            }
        }

        private bool AreHours(params TextBox[] textboxes)
        {
            // The regex for matching a time string, credit Steve Valaitis.
            string pattern = @"^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$";
            // Check that all textboxes passed in as parameters match the 
            return textboxes.All(t => Regex.IsMatch(t.Text, pattern));
        }

        // Returns true if no textboxes in the passed in textbox list are null or whitespace.
        private bool FieldsAreFilled(params TextBox[] textBoxList)
        {
            return !((from t in textBoxList where string.IsNullOrWhiteSpace(t.Text) select t).Any());
        }
    }
}
