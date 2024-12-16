using System.Collections.Generic;
using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.ViewModels;
using System.Windows.Input; // Add this line for MouseEventArgs
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using ApkaSkiny.View;
namespace ApkaSkiny
{
    public partial class SelectSkinDialog : Window
    {
        public string SelectedSkin { get; private set; } // Wybrany skin

        public SelectSkinDialog(string title, List<string> skinChoices)
        {
            try
            {
                InitializeComponent();
                Title = title; // Ustawienie tytułu okna
                SkinListBox.ItemsSource = skinChoices; // Powiązanie z listą skinów
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedSkin = SkinListBox.SelectedItem as string; // Pobranie wybranego skina
            if (SelectedSkin == null)
            {
                MessageBox.Show("Nie wybrano żadnego skina!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true; // Ustawienie wyniku okna na true
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

    }
}
