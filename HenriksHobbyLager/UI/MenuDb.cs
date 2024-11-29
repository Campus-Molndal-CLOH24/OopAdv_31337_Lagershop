using HenriksHobbylager.UI;
using HenriksHobbylager.Interface;

namespace HenriksHobbylager;

internal class MenuDb
{
	private readonly IProductFacade _sqliteFacade;
	private readonly IProductFacade _mongoFacade;

	public MenuDb(IProductFacade sqliteFacade, IProductFacade mongoFacade)
	{
		_sqliteFacade = sqliteFacade;
		_mongoFacade = mongoFacade;
	}
	public async Task ShowMainMenuAsync()
	{
		Console.Clear();
		Console.ForegroundColor = ConsoleColor.Green;
		PrintCentered("========================================");
		PrintCentered("       🎉 Henriks Hobbylager 🎉        ");
		PrintCentered("========================================");
		Console.ResetColor();

		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Vad vill du göra idag?");
		Console.WriteLine("----------------------------------------");
		Console.ResetColor();

		Console.WriteLine("[1] 📂 SQLite");
		Console.WriteLine("[2] 🌐 MongoDB");
		Console.WriteLine("[0] ❌ Avsluta");
		Console.WriteLine("----------------------------------------");
		Console.Write("Välj ett alternativ: ");

		var menuOption = Console.ReadLine();

		switch (menuOption)
		{
			case "1":
				Console.Clear();
				Console.WriteLine("🔧 Öppnar SQLite...");
				await Task.Delay(1000); // Simulerar att något laddar
				var menuSQLite = new MenuCrud(_sqliteFacade);
				await menuSQLite.ShowMenu();
				break;
			case "2":
				Console.Clear();
				Console.WriteLine("🌐 Öppnar MongoDB...");
				await Task.Delay(1000);
				var menuMongo = new MenuCrud(_mongoFacade);
				await menuMongo.ShowMenu();
				break;
			case "0":
				Console.WriteLine("❌ Avslutar programmet. Tack för att du använde Henriks Hobbylager!");
				Console.ReadKey();
				Environment.Exit(0);
				break;
			default:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("❌ Felaktigt val. Försök igen.");
				Console.ResetColor();
				break;
		}
	}

	private void PrintCentered(string text)
	{
		int windowWidth = Console.WindowWidth;
		int textPadding = (windowWidth - text.Length) / 2;
		Console.WriteLine(new string(' ', textPadding) + text);
	}
}

