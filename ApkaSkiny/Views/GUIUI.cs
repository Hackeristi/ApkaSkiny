using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny.View;
using System.Threading.Tasks;

public class GUIUI : IUI
{
    public void ShowMessage(string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }

    public void PrintSkinsTable(IEnumerable<Skin> skins)
    {
        var grid = new System.Windows.Controls.DataGrid();
        grid.ItemsSource = skins;
    }

    public string GetSelection(string prompt, List<string> options)
    {
        var window = new SelectionWindow(prompt, options);
        bool? result = window.ShowDialog();

        if (result == true)
        {
            return window.SelectedOption;
        }

        return null; 
    }

    public string GetUserInput(string prompt, string title)
    {
        var window = new InputWindow(prompt, title);
        bool? result = window.ShowDialog();

        if (result == true)
        {
            return window.UserInput;
        }

        return null;
    }


    public async void ShowAsciiAnimation(string[] animationFrames)
    {
        foreach (var frame in animationFrames)
        {
            await Task.Delay(500);
        }
    }

    public void WaitForUserToContinue()
    {
        MessageBox.Show("Press OK to continue.");
    }

    public void ShowTitle(string title)
    {
        var window = new Window
        {
            Title = title
        };
        window.ShowDialog();
    }
}
