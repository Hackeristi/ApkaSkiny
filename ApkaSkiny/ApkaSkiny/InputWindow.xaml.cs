using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace ApkaSkiny.View
{
    public partial class InputWindow : Window
    {
        public string UserInput { get; private set; }

        public string Prompt { get; private set; }

        public InputWindow(string prompt, string title)
        {
            InitializeComponent();
            this.Prompt = prompt;
            this.Title = title;
            this.DataContext = this;
        }

        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            UserInput = InputTextBox.Text;
            this.DialogResult = true;   
            this.Close();                
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
