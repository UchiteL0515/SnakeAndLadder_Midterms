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

		public void Play(){
			bool gameWon = false;
			int currentPlayerIndex = 0;

			while(!gameWon){
				Player currentPlayer = Players[currentPlayerIndex];
				PlayerTurnCounters[currentPlayer]++;

				Console.Clear();
				gameBoard.displayBoards(Players);

				if(currentPlayer.checkStatus()){
					if(currentPlayer.StatusCounter == 0){
						Console.WriteLine($"\nTime has returned for player [{currentPlayer.Name}]...");
						currentPlayer.Status = "Normal";
						currentPlayer.StatusCounter = 0;
					} else{
						Console.WriteLine($"\nPlayer [{currentPlayer.Name}] is stunned and cannot move.");
						currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
						currentPlayer.StatusCounter--;
						//Console.WriteLine(checkTurn); //Debug 
						Console.ReadKey();
						continue;
					}
				}

				Console.WriteLine($"\nPlayer {currentPlayerIndex + 1} {currentPlayer.Name}'s turn:");
				Console.WriteLine("\nChoose what skills to use...");
				displaySkills();
				Console.WriteLine("Or press [Enter] to roll the dice...");
				
				string skillUsed;
				int move = 0;

				while(true){
					Console.Write("Select skill [1-3]: ");
					skillUsed = Console.ReadLine();

						if(string.IsNullOrEmpty(skillUsed)){
							move = rollDice();
							currentPlayer.Position += move; 
							Console.WriteLine($"Player [{currentPlayer.Name}] rolled a {move}");
							break;
						} else if(skillUsed.Equals("1") || skillUsed.Equals("2") || skillUsed.Equals("3")){
							currentPlayer.useSkill(PlayerTurnCounters[currentPlayer], int.Parse(skillUsed), currentPlayerIndex, Players);
							break;
						} else{
							Console.WriteLine("Invalid input... please follow the instructions...");
						}
				}

				currentPlayer.Position = gameBoard.checkPosition(currentPlayer.Position);

				if(currentPlayer.Position > 100){
					int overlap = currentPlayer.Position - 100;
					currentPlayer.Position = 100 - overlap;
					Console.WriteLine($"Oh no! moved past 100...moving back {overlap} steps...");
				} else if(currentPlayer.Position == 100){
					Console.WriteLine($"Player [{currentPlayer.Name}] wins!");
					gameWon = true;
					break;
				} 

				currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;

				Console.Write("Press [Enter] key to continue...");
				Console.ReadKey();
			}
		}

		private void displaySkills(){
			Console.WriteLine("1 - [Stun]");
			Console.WriteLine("2 - [Ladder Time!]");
			Console.WriteLine("3 - [Oh no! Snake]");
		}

		private int rollDice(){
			Random random = new Random();
			return random.Next(1,7);
		}
	}
}
