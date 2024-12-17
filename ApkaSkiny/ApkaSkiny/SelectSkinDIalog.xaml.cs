using System.Collections.Generic;
using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.ViewModels;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using ApkaSkiny.View;
using System.Windows.Media.Animation;

namespace ApkaSkiny
{
    public partial class SelectSkinDialog : Window
    {
        public string SelectedSkin { get; private set; }

        public SelectSkinDialog(string title, List<string> skinChoices)
        {
            try
            {
                InitializeComponent();
                Title = title;
                SkinListBox.ItemsSource = skinChoices; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedSkin = SkinListBox.SelectedItem as string; 
            if (SelectedSkin == null)
            {
                MessageBox.Show("Nie wybrano żadnego skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)Application.Current.Resources["ButtonHoverColor"];
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)Application.Current.Resources["ButtonBackgroundColor"];
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(Window.OpacityProperty, fadeInAnimation);
        }

    }
}
