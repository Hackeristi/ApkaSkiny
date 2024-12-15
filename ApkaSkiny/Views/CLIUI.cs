using System;
using System.Collections.Generic;
using System.Threading;

namespace ApkaSkiny.Models
{
    public class CLIUI : IUI
    {
        public void ShowTitle(string title)
        {
            Console.WriteLine(title);
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public string GetSelection(string prompt, List<string> options)
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            var choice = Console.ReadLine();
            return options[int.Parse(choice) - 1];  // Zwróć wybraną opcję
        }

        public void ShowAsciiAnimation(string[] animationFrames)
        {
            foreach (var frame in animationFrames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                Thread.Sleep(500);  // Pokazuj animację przez pół sekundy
            }
        }

        public void PrintSkinsTable(IEnumerable<Skin> skins)
        {
            foreach (var skin in skins)
            {
                Console.WriteLine($"{skin.Name} | {skin.WeaponType} | {skin.Price:C}");
            }
        }

        public void WaitForUserToContinue()
        {
            Console.WriteLine("Naciśnij Enter, aby kontynuować...");
            Console.ReadLine();
        }
    }
}
