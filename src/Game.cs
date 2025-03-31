using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{
	
	public class Game{
		private List<Player> Players {get; set;}
		private Board gameBoard {get; set;}
		private Dictionary<Player, int> PlayerTurnCounters = new Dictionary<Player, int>();

		public Game(List<Player> players){
			Players = players;
			gameBoard = new Board();
			
			foreach(var player in Players){
				PlayerTurnCounters[player] = 0;
			}
		}

        public void CenterText(string text, bool newLine = true)
        {
            int screenWidth = Console.WindowWidth;
            Console.SetCursorPosition((screenWidth - text.Length) / 2, Console.CursorTop);
            Console.Write(text);
            if (newLine) Console.WriteLine();
        }
        public void Play()
        {
            bool gameWon = false;
            int currentPlayerIndex = 0;

            while (!gameWon)
            {
                Player currentPlayer = Players[currentPlayerIndex];
                PlayerTurnCounters[currentPlayer]++;

                Console.Clear();
                gameBoard.displayBoards(Players);

                if (currentPlayer.checkStatus())
                {
                    if (currentPlayer.StatusCounter == 0)
                    {
                        CenterText($"Time has returned for player [{currentPlayer.Name}]...");
                        currentPlayer.Status = "Normal";
                        currentPlayer.StatusCounter = 0;
                    }
                    else
                    {
                        CenterText($"Player [{currentPlayer.Name}] is stunned and cannot move.");
                        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
                        currentPlayer.StatusCounter--;
                        Console.ReadKey();
                        continue;
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                CenterText($"Player {currentPlayerIndex + 1} {currentPlayer.Name}'s turn:");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                CenterText("Choose what skills to use");
                Console.ResetColor();
                Console.WriteLine();
                displaySkills();
                CenterText("Or press [Enter] to roll the dice!");
                CenterText("Select skill [1-3]: ", false);
                string skillUsed = Console.ReadLine();
                Console.ResetColor();
                Console.WriteLine();

                int move = 0;
                if (string.IsNullOrEmpty(skillUsed))
                {
                    move = rollDice();
                    currentPlayer.Position += move;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    CenterText($"Player [{currentPlayer.Name}] rolled a {move}");
                    Console.ResetColor();
                }
                else if (skillUsed.Equals("1") || skillUsed.Equals("2") || skillUsed.Equals("3"))
                    currentPlayer.useSkill(PlayerTurnCounters[currentPlayer], Convert.ToInt32(skillUsed), currentPlayerIndex, Players);

                currentPlayer.Position = gameBoard.checkPosition(currentPlayer.Position);

                if (currentPlayer.Position > 100)
                {
                    int overlap = currentPlayer.Position - 100;
                    currentPlayer.Position = 100 - overlap;
                    Console.ForegroundColor = ConsoleColor.Red;
                    CenterText($"Oh no! moved past 100...moving back {overlap} steps...");
                    Console.ResetColor();
                }
                else if (currentPlayer.Position == 100)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    CenterText($"Player [{currentPlayer.Name}] wins!");
                    Console.ResetColor();
                    gameWon = true;
                    break;
                }
                currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                CenterText("Press [Enter] key to continue");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        private void displaySkills()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            CenterText("╔═════════════════════╗");
            CenterText("║      SKILLS MENU    ║");
            CenterText("╠═════════════════════╣");

            Console.ForegroundColor = ConsoleColor.Cyan;
            CenterText("║ 1 - [Stun]          ║");

            Console.ForegroundColor = ConsoleColor.Green;
            CenterText("║ 2 - [Ladder Time!]  ║");

            Console.ForegroundColor = ConsoleColor.Red;
            CenterText("║ 3 - [Oh no! Snake]  ║");

            Console.ForegroundColor = ConsoleColor.Magenta;
            CenterText("╚═════════════════════╝");

            Console.ResetColor();
            Console.WriteLine();
        }



        private int rollDice(){
			Random random = new Random();
			return random.Next(1,7);
		}
	}
}
