using ApkaSkiny.Models;
using ApkaSkiny.Views;
using System.Windows;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ApkaSkiny
{
    public partial class App : Application
    {
        public SecondView SecondView { get; set; }

        // Initialize the Controller and the SecondView somewhere in your application
        public void OpenSecondView()
        {
            // Create a Controller instance
            var controller = new Controller(new CLIUI());  // Assuming ConsoleUI or any other implementation of IUI

            // Create the SecondView instance and pass the Controller to it
            SecondView = new SecondView(controller);
            SecondView.Show();
        }
    }
}
