using System.Windows;
using ApkaSkiny.Models;
using ApkaSkiny;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var app = new Application();
        var controller = new Controller(new GUIUI()); 
        var window = new MainWindow(controller); 
        app.Run(window);
    }
}
