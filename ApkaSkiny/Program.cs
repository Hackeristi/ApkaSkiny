using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var app = new Application();
        var controller = new Controller(new GUIUI());  // Assuming GUIUI is your UI implementation
        var window = new MainWindow(controller);  // Pass the controller to MainWindow
        app.Run(window);
    }
}
