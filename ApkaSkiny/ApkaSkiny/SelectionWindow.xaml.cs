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

            DataContext = this;

            OptionsListBox.ItemsSource = Options;
        }

        private void OptionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedOption = (string)OptionsListBox.SelectedItem;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedOption))
            {
                DialogResult = true; 
                Close();
            }
            else
            {
                MessageBox.Show("Please select an option before proceeding.");
            }
        }
    }
}
