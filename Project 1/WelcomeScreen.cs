using System;
using Project_1;

namespace BaseFeatures
{
    public class WelcomeScreen : IWelcomeScreen
    {
        public void WriteWelcomeMessage()
        {
            ConsoleKeyInfo key;

            Console.WriteLine("Welcome to the RentC, your brand new solution to manage and control your company's data without missing anythyng.");
            WriteOnBottomLine("Press ENTER to continue or ESC to quit.");

            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                    Environment.Exit(0);
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                    Console.Clear();

                    Menu menu = new Menu();
                    menu.DisplayMenu();
            }
        }
        public void WriteOnBottomLine(string text)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            Console.CursorLeft = Console.CursorLeft + (Console.WindowWidth - text.Length)/2;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
            Console.Write(text);

            // Restore previous position
            Console.SetCursorPosition(x, y);
        }
    }
}
