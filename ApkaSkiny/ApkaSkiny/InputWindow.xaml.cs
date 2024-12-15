using ApkaSkiny.Models;
using ApkaSkiny.View;
using System.Windows;
using System.Windows.Input; // Add this line for MouseEventArgs
using System.Windows.Media;
using System.Windows.Controls;

namespace ApkaSkiny.View
{
    public partial class InputWindow : Window
    {
        // Add the Prompt property to store the prompt message
        public string Prompt { get; private set; }

        public string UserInput { get; private set; }

        // Constructor
        public InputWindow(string prompt)
        {
            InitializeComponent();  // Initialize the window and XAML components
            this.Prompt = prompt;   // Set the Prompt property
        }

        // OK button click handler
        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            UserInput = InputTextBox.Text;  // Get the user input from the TextBox
            this.DialogResult = true;       // Set the dialog result to true
            this.Close();                   // Close the window
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = (SolidColorBrush)Resources["ButtonHoverColor"];
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = (SolidColorBrush)Resources["ButtonBackgroundColor"];
            }
        }
    }



}
