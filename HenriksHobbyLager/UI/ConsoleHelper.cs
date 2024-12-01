using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.UI
{
    internal static class ConsoleHelper
    {
        internal static string GetNonNullInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                DisplayColourMessage("Fältet får inte vara tomt. Försök igen.", ConsoleColor.Red);
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input;
        }

        internal static void DisplayColourMessage(string message, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static void PrintCentered(string text)
        {
            int windowWidth = Console.WindowWidth;
            int textPadding = (windowWidth - text.Length) / 2;
            Console.WriteLine(new string(' ', textPadding) + text);
        }
    }
}