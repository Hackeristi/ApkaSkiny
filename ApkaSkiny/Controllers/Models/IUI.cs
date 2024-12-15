using ApkaSkiny.Models;
using Spectre.Console;

namespace ApkaSkiny.Models
{
    public interface IUI
    {
        void ShowMessage(string message);
        void PrintSkinsTable(IEnumerable<Skin> skins);
        string GetSelection(string prompt, List<string> options);
        string GetUserInput(string prompt);
        void ShowAsciiAnimation(string[] animationFrames);
        void WaitForUserToContinue();
        void ShowTitle(string title);
    }
}