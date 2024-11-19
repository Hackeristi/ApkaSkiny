using Spectre.Console;
using Figgle;

namespace ApkaSkiny.Views
{
    public class SkinView
    {
        public void ShowTitle(string title)
        {
            string asciiTitle = FiggleFonts.Standard.Render(title);

            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[#D30E92]" + asciiTitle + "[/]");

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[#FFD6E9]Wybierz opcję:[/]")
                    .AddChoices("Wejdź do aplikacji", "Wyjdź z aplikacji")
                    .HighlightStyle(new Style(foreground: new Color(0xC7, 0x92, 0x74)))
            );

            if (selection == "Wejdź do aplikacji")
            {
                AnsiConsole.MarkupLine("Wejście do aplikacji...");
            }
            else if (selection == "Wyjdź z aplikacji")
            {
                AnsiConsole.MarkupLine("[#0671B7]Zamykanie aplikacji...[/]");
                Environment.Exit(0);
            }
        }

        public void ShowAsciiAnimation(string[] animationFrames)
        {
            foreach (var frame in animationFrames)
            {
                AnsiConsole.Clear();
                string asciiFrame = FiggleFonts.Standard.Render(frame);
                AnsiConsole.MarkupLine($"[#D30E92]{asciiFrame}[/]");
                System.Threading.Thread.Sleep(500);
            }
            AnsiConsole.Clear();
        }

        public void PrintSkinsTable(IEnumerable<Skin> skins)
        {
            AnsiConsole.Clear();

            var table = new Table()
                .BorderColor(new Color(0xFF, 0xBC, 0xDA))
                .Border(TableBorder.Rounded)
                .AddColumn("[#946656]Nazwa[/]")
                .AddColumn("[#946656]Kolekcja[/]")
                .AddColumn("[#946656]Typ broni[/]")
                .AddColumn("[#946656]Kategoria[/]")
                .AddColumn("[#946656]Cena[/]")
                .AddColumn("[#946656]Strona[/]")
                .AddColumn("[#946656]Ulubione[/]");

            foreach (var skin in skins)
            {
                var priceColor = skin.Price switch
                {
                    > 500 => "[#D30E92]",
                    < 50 => "[#DB76BC]",
                    _ => "[#FFC0EE]"
                };

                table.AddRow(
                    $"[#C79274]{skin.Name}[/]",
                    $"[#C79274]{skin.Collection}[/]",
                    $"[#C79274]{skin.WeaponType}[/]",
                    $"[#C79274]{skin.WeaponCategory}[/]",
                    $"{priceColor}${skin.Price:F2}[/]",
                    $"[#C79274]{skin.Side}[/]",
                    skin.IsFavorite ? "[#FFC0EE]*[/]" : "[#C79274]x[/]"
                );
            }

            AnsiConsole.Write(table);
        }

        public void ShowMessage(string message, string color = "#FFD6E9")
        {
            AnsiConsole.MarkupLine($"[{color}]{message}[/]");
        }

        public string GetUserInput(string prompt)
        {
            AnsiConsole.Markup($"[#FFBCDA]{prompt}[/]: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public string GetSelection(string prompt, List<string> options)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[#FFBCDA]{prompt}[/]")
                    .AddChoices(options)
                    .HighlightStyle(new Style(foreground: new Color(0xC7, 0x92, 0x74)))
            );
        }
    }
}
