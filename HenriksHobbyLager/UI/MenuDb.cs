using HenriksHobbylager.UI;
using HenriksHobbyLager.Facades;
using HenriksHobbyLager.UI;

namespace HenriksHobbylager;

internal class MenuDb
{
    private readonly SQLiteFacade _sqliteFacade;
    private readonly MongoDbFacade _mongoFacade;

    public MenuDb(SQLiteFacade sqliteFacade, MongoDbFacade mongoFacade)
    {
        _sqliteFacade = sqliteFacade ?? throw new ArgumentNullException(nameof(sqliteFacade), "SQLite-fasaden är null.");
        _mongoFacade = mongoFacade ?? throw new ArgumentNullException(nameof(mongoFacade), "MongoDB-fasaden är null.");
        Console.WriteLine($"SQLite Fasad: {_sqliteFacade}");
        Console.WriteLine($"MongoDB Fasad: {_mongoFacade}");
    }

    public async Task ShowMainMenuAsync()
    {
        Console.WriteLine($"SQLite Fasad: {_sqliteFacade.DatabaseType}"); // Temp: Debug line
        Console.WriteLine($"MongoDB Fasad: {_mongoFacade?.DatabaseType ?? "Ej konfigurerad"}"); // Temp: Debug line

        // Console.Clear();  // Temp: döljer denna tills vi kunnat debugga färdigt vilken databas vi ansluter till där ovan
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
                await OpenSQLiteMenuAsync();
                break;

            case "2":
                await OpenMongoDBMenuAsync();
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

    private async Task OpenSQLiteMenuAsync()
    {
        if (_sqliteFacade == null)
        {
            ConsoleHelper.DisplayColourMessage("SQLite-fasaden är inte korrekt konfigurerad.", ConsoleColor.Red);
            return;
        }

        var menuSQLite = new MenuCrud(_sqliteFacade, null); // Endast SQLite används
        await menuSQLite.ShowMenu();
    }

    private async Task OpenMongoDBMenuAsync()
    {
        if (_mongoFacade == null)
        {
            ConsoleHelper.DisplayColourMessage("MongoDB-fasaden är inte korrekt konfigurerad.", ConsoleColor.Red);
            return;
        }

        var menuMongo = new MenuCrud(null, _mongoFacade); // Endast MongoDB används
        await menuMongo.ShowMenu();
    }
}