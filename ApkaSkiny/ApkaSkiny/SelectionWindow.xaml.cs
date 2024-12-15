using System.Collections.Generic;
using System.Windows;

namespace ApkaSkiny.View
{
    public partial class SelectionWindow : Window
    {
        public string SelectedOption { get; private set; }
        public string Prompt { get; private set; }
        public List<string> Options { get; private set; }

        public SelectionWindow(string prompt, List<string> options)
        {
            InitializeComponent();
            Prompt = prompt;
            Options = options;

            // Set the DataContext for binding
            DataContext = this;

            // Populate the ListBox with options
            OptionsListBox.ItemsSource = Options;
        }

        private void OptionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Update the selected option
            SelectedOption = (string)OptionsListBox.SelectedItem;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure an option is selected
            if (!string.IsNullOrEmpty(SelectedOption))
            {
                DialogResult = true; // Close the dialog with success
                Close();
            }
            else
            {
                MessageBox.Show("Please select an option before proceeding.");
            }
        }
    }
}
