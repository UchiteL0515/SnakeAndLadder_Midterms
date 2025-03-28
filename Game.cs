using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{

	public class Game{
		private List<Player> Players {get; set;}
		private Board gameBoard {get; set;}

		public Game(List<Player> players){
			Players = players;
			gameBoard = new Board();
		}

		public void Play(){
			bool gameWon = false;
			int currentPlayerIndex = 0;

			while(!gameWon){
				Player currentPlayer = Players[currentPlayerIndex];
				Console.Clear();
				gameBoard.displayBoards(Players);

				Console.Write($"\nIt is {currentPlayer.Name}'s turn...Press any key to roll the dice");
				Console.ReadKey();

				int move = rollDice();
				Console.WriteLine($"Player {currentPlayer.Name} rolled a {move}");

				currentPlayer.Position += move; 
				currentPlayer.Position = gameBoard.checkPosition(currentPlayer.Position);

				if(currentPlayer.Position > 100){
					int overlap = currentPlayer.Position - 100;
					currentPlayer.Position = 100 - overlap;
					Console.WriteLine($"Oh no! moved past 100...moving back {overlap} steps...");
				} else if(currentPlayer.Position == 100){
					Console.WriteLine($"Player {currentPlayer.Name} wins!");
					gameWon = true;
					break;
				} 
				else currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;

				Console.Write("Press any key to continue...");
				Console.ReadKey();
			}
		}

		private int rollDice(){
			Random random = new Random();
			return random.Next(1,7);
		}
	}
}
