using ApkaSkiny.Models;
using Figgle;
using ApkaSkiny.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ApkaSkiny
{
    public partial class MainWindow : Window
    {
        private readonly Controller _controller;

        public MainWindow(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
            ShowAsciiArt();
            ShowTitle("Przegladarka skinow CS2");
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

        // Click event for switching to the second view
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            // Create and show SecondView
            SecondView secondView = new SecondView(_controller);
            this.Close();  // Close the MainWindow
            secondView.Show();  // Show SecondView
        }

        private void SwitchToTextButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new SkinView(null); // Or pass an already initialized Controller if applicable
            var controller = new Controller(view);
            this.Close(); // Close current window
            controller.Run(); // Start the controller
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }

        public void ShowAsciiArt()
        {
            string asciiArt = @"

     *+                 ::---------++==+=.                                               
     %@#       +@@%%#%%@@@@@@@@@@@@@@@@@%#**++++++++++++=++**-.                           
   .*@%%**++**%#+@###@@@@@@@@@@@@@@@@@@@@@@%%@@@@@@@@%@@@%@@@@%=--:.    :.                
    .%@@@@%@%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#@@@@#@@@@@%%@@@@@@@@@@@%@@%%%#**++=-::.    
       .       ...     .:-=+++****##%@@##%%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%%%%%@%%%%%@@+   
                                          -@@@@@@@@@%=+#%@@@@@@+=+#@@@@@@@@@@@@@@@@@@@@   
                                         :@@@@@@@=.-.    +@@@@@     .-+#@@@@@@@@@@@@@@*   
                                        =@@@@@@@*         =@@@@+         :=*%@@@@@@@@@:   
                                      -%@@@@@@@#           *@@@@-            .-*%@@@@@    
                                   .=%@@@@@@@@+             #@@%:                .-*#-    
                                    %@@@@@@@%:               ..                           
                                     #@@@@%-                                              
";
            // Przypisanie ASCII art do TextBlock w WPF
            TextBlockAsciiArt.Text = asciiArt;
        }

        public void ShowTitle(string title)
        {
            string asciiTitle = FiggleFonts.Standard.Render(title);
            // Wyświetlanie tytułu w TextBlock
            TitleTextBlock.Text = asciiTitle;
        }
    }
}
