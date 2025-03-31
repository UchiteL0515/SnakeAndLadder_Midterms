using System;
using System.Collections.Generic;

namespace SnakeNLadderGame
{
    class MainProgram
    {
        public static void CenterText(string text, ConsoleColor color = ConsoleColor.White, bool newLine = true)
        {
            int screenWidth = Console.WindowWidth;
            Console.SetCursorPosition((screenWidth - text.Length) / 2, Console.CursorTop);
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
            if (newLine) Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            List<Player> Players = new List<Player>();

            Console.Clear();
            Console.WriteLine();
            CenterText("===============================", ConsoleColor.Blue);
            CenterText("|                             |", ConsoleColor.Blue);
            CenterText("| WELCOME TO SNAKE AND LADDER |", ConsoleColor.Cyan);
            CenterText("|                             |", ConsoleColor.Blue);
            CenterText("===============================", ConsoleColor.Blue);
            Console.WriteLine();
            CenterText("Please enter number of players (2-4): ", ConsoleColor.DarkYellow, false);
            Console.ForegroundColor = ConsoleColor.Green;
            int numberOfPlayers = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            Console.ResetColor();

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.WriteLine();
                CenterText("******************************", ConsoleColor.White);
                Console.WriteLine();
                CenterText($"Enter player {i}'s name: ", ConsoleColor.Magenta, false);

                Console.ForegroundColor = ConsoleColor.Green;
                string name = Console.ReadLine();
                Console.ResetColor();
                Players.Add(new Player(name));
            }

            Game game = new Game(Players);
            game.Play();
        }
    }
}
