using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
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
        // the file that we are writing to.
        string path = "schedule.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            // if the textboxes passed in are filled...
            if (FieldsAreFilled(EventNameTextBox, StartTimeTextBox, LocationTextBox))
            {
                // and if the Start time textbox has regex matching that for a time entry...
                if (AreHours(StartTimeTextBox))
                {
                    // Then clear the warning label,
                    WarningLabel.Content = "";
                    // add the event to the schedule txt file, grabbing the values from the appropriate locations.
                    AddEventToSchedule(EventNameTextBox, DatePicker, StartTimeTextBox, LocationTextBox, DescriptionTextBox);
                    // Reset all textbox values to blank (leave the calendar value uncleared to prevent the reader from passing in a null value there) 
                    ClearTextBoxes(EventNameTextBox, StartTimeTextBox, LocationTextBox, DescriptionTextBox);
                    // And read the events from the .txt file to the multi-line events Textbox on the right-hand side of the form.
                    ReadEventsFromFile(path);
                }
                else
                {
                    // Let the reader know if the Hours textbox input is formatted incorrectly.
                    WarningLabel.Content = "Invalid data. You must enter a valid time in format XX:XX in the start time box";
                }
            }
            else
            {
                // Ensure all required fields are filled, and prompt the user if they aren't
                WarningLabel.Content = "All non-optional fields must be filled";
            }
        }

        private void AddEventToSchedule(TextBox eventNameTextBox, DatePicker datePicker, TextBox startTimeTextBox, TextBox locationTextBox, TextBox descriptionTextBox)
        {
            // Use the textwriter type to write a line entry to the .txt file. The boolean second parameter in the TextWriter constructor tells the StreamWriter to create a new file at path location if no file currently exists.
            string date = ((DateTime)datePicker.SelectedDate).ToShortDateString();
            using TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine($"Event name: {eventNameTextBox.Text}.\n\t Date: {date}.\n\t Start time: {startTimeTextBox.Text}.\n\t Location: {locationTextBox.Text}.\n\t Description: {descriptionTextBox.Text}");
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
            // Check that all textboxes passed in as parameters match the Regex pattern.
            return textboxes.All(t => Regex.IsMatch(t.Text, pattern));
        }

        // Returns true if no textboxes in the passed in textbox list are null or whitespace.
        private bool FieldsAreFilled(params TextBox[] textBoxList)
        {
            return !((from t in textBoxList where string.IsNullOrWhiteSpace(t.Text) select t).Any());
        }

        private void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(EventNameTextBox, StartTimeTextBox, LocationTextBox, DescriptionTextBox, EventListTextBox);
            File.Delete(path);
        }
    }
}
