using HenriksHobbylager.UI;
using HenriksHobbylager.Interface;
using HenriksHobbyLager.UI;

namespace HenriksHobbylager;

internal class MenuDb
{
    private readonly IProductFacade _sqliteFacade;
    private readonly IProductFacade _mongoFacade;

    public MenuDb(IProductFacade sqliteFacade, IProductFacade mongoFacade)
    {
        _sqliteFacade = sqliteFacade ?? throw new ArgumentNullException(nameof(sqliteFacade), "SQLite-fasaden är null.");
        _mongoFacade = mongoFacade ?? throw new ArgumentNullException(nameof(mongoFacade), "MongoDB-fasaden är null.");
    }

    public async Task ShowMainMenuAsync()
    {
        Console.Clear();
        ConsoleHelper.DisplayColourMessage("========================================", ConsoleColor.Green);
        ConsoleHelper.DisplayColourMessage("       🎉 Henriks Hobbylager 🎉        ", ConsoleColor.Green);
        ConsoleHelper.DisplayColourMessage("========================================", ConsoleColor.Green);

        Console.WriteLine();
        ConsoleHelper.DisplayColourMessage("Vad vill du göra idag?", ConsoleColor.Yellow);
        Console.WriteLine("----------------------------------------");

        Console.WriteLine("[1] 📂 SQLite");
        Console.WriteLine("[2] 🌐 MongoDB");
        Console.WriteLine("[0] ❌ Avsluta");
        Console.WriteLine("----------------------------------------");
        Console.Write("Välj ett alternativ: ");

        var menuOption = Console.ReadLine();

        switch (menuOption)
        {
            case "1":
                if (_sqliteFacade == null)
                {
                    ConsoleHelper.DisplayColourMessage("❌ SQLite-fasaden är inte korrekt konfigurerad.", ConsoleColor.Red);
                    return;
                }

                Console.Clear();
                ConsoleHelper.DisplayColourMessage("🔧 Öppnar SQLite...", ConsoleColor.Cyan);
                await Task.Delay(1000);
                var menuSQLite = new MenuCrud(_sqliteFacade, _sqliteFacade, _mongoFacade);
                await menuSQLite.ShowMenu();
                break;

            case "2":
                if (_mongoFacade == null)
                {
                    ConsoleHelper.DisplayColourMessage("❌ MongoDB-fasaden är inte korrekt konfigurerad.", ConsoleColor.Red);
                    return;
                }

                Console.Clear();
                ConsoleHelper.DisplayColourMessage("🌐 Öppnar MongoDB...", ConsoleColor.Cyan);
                await Task.Delay(1000);
                var menuMongo = new MenuCrud(_mongoFacade, _sqliteFacade, _mongoFacade);
                await menuMongo.ShowMenu();
                break;

            case "0":
                ConsoleHelper.DisplayColourMessage("❌ Avslutar programmet. Tack för att du använde Henriks Hobbylager!", ConsoleColor.Green);
                Console.ReadKey();
                Environment.Exit(0);
                break;

            default:
                ConsoleHelper.DisplayColourMessage("❌ Felaktigt val. Försök igen.", ConsoleColor.Red);
                Console.ResetColor();
                break;
        }
    }
}
