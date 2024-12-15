using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.View;
using System.Threading.Tasks;

public class GUIUI : IUI
{
    public void ShowMessage(string message)
    {
        // Ensures that the method is executed on the UI thread
        Application.Current.Dispatcher.Invoke(() =>
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }

    public void PrintSkinsTable(IEnumerable<Skin> skins)
    {
        var grid = new System.Windows.Controls.DataGrid();
        grid.ItemsSource = skins;
        // You need to add the DataGrid to your WPF window's layout
    }

    public string GetSelection(string prompt, List<string> options)
    {
        var window = new SelectionWindow(prompt, options);
        bool? result = window.ShowDialog();

        if (result == true)
        {
            return window.SelectedOption; // Return the selected option
        }

        return null; // User closed the window or canceled
    }

    public string GetUserInput(string prompt)
    {
        var window = new InputWindow(prompt);
        window.ShowDialog();
        return window.UserInput;
    }

    public async void ShowAsciiAnimation(string[] animationFrames)
    {
        foreach (var frame in animationFrames)
        {
            // Update a Label or TextBlock to show each frame
            // For example, update a TextBlock or Label control with the frame
            await Task.Delay(500); // Simulate frame delay asynchronously
        }
    }

    public void WaitForUserToContinue()
    {
        MessageBox.Show("Press OK to continue.");
    }

    public void ShowTitle(string title)
    {
        // Show title in a window's title bar or any other WPF control
        var window = new Window
        {
            Title = title
        };
        window.ShowDialog();
    }
}
