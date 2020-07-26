using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        string path = "schedule.txt";

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
                    AddEventToSchedule(EventNameTextBox, DatePicker, StartTimeTextBox, LocationTextBox, DescriptionTextBox);
                    ClearTextBoxes(EventNameTextBox, StartTimeTextBox, LocationTextBox, DescriptionTextBox);
                    ReadEventsFromFile(path);
                }
                else
                {
                    WarningLabel.Content = "Invalid data. You must enter a valid time in format XX:XX in the start time box";
                }
            }
            else
            {
                WarningLabel.Content = "All non-optional fields must be filled";
            }
        }

        private void AddEventToSchedule(TextBox eventNameTextBox, DatePicker datePicker, TextBox startTimeTextBox, TextBox locationTextBox, TextBox descriptionTextBox)
        {
            using TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine($"Event name: {eventNameTextBox.Text}.\n\t Date: {datePicker.SelectedDate}.\n\t Start time: {startTimeTextBox.Text}.\n\t Location: {locationTextBox.Text}.\n\t Description: {descriptionTextBox.Text}");
        }

        private void ReadEventsFromFile(string path)
        {
            using TextReader tw = new StreamReader(path, true);
            EventListTextBox.Text = tw.ReadToEnd();
        }

        private void ClearTextBoxes(params TextBox[] textboxes)
        {
            foreach (TextBox t in textboxes)
            {
                t.Text = "";
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
