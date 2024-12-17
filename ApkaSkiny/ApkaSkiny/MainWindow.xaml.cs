using ApkaSkiny.Models;
using Figgle;
using ApkaSkiny.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.VisualBasic.FileIO;

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
            ShowImage();
            ShowText();

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

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            SecondView secondView = new SecondView(_controller);
            this.Close();  
            secondView.Show();  
        }

        private void SwitchToTextButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new SkinView(null); 
            var controller = new Controller(view);
            this.Close(); 
            controller.Run();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
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
            //TextBlockAsciiArt.Text = asciiArt;
        }

        public void ShowImage()
        {
            string imagePath = @"C:\Users\sasza\source\repos\ApkaSkiny\c5575d9f-5d14-4e25-b537-0c566e1b8b83-front3x.jpg";

            ImageControl.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }
        public void ShowText()
        {
            string textimagePath = @"C:\Users\sasza\source\repos\ApkaSkiny\text-to-image.jpg"; // Zmień ścieżkę na odpowiednią

            TextImageControl.Source = new BitmapImage(new Uri(textimagePath, UriKind.Absolute));
        }

        public void ShowTitle(string title)
        {
            string asciiTitle = FiggleFonts.Standard.Render(title);
            // Wyświetlanie tytułu w TextBlock
            //TitleTextBlock.Text = asciiTitle;
        }
    }
}
