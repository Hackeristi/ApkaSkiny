using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace ApkaSkiny.View
{
    public partial class InputWindow : Window
    {
        // Property to store user input
        public string UserInput { get; private set; }

        // Property to store prompt message
        public string Prompt { get; private set; }

        // Constructor to accept prompt and set the title dynamically
        public InputWindow(string prompt, string title)
        {
            InitializeComponent();
            this.Prompt = prompt;
            this.Title = title; // Set the window title dynamically
            this.DataContext = this; // Bind the Prompt property to the UI
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
