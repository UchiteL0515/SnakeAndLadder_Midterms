using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{
	
	class Menu{

	}

	class MainProgram{
		public static void Main(string[] args){
			List<Player> Players = new List<Player>();

			Console.Clear();
			Console.WriteLine("Welcome to Snake and Ladders!");
			
			int numberOfPlayers = 0;
			bool validInput = false;

			while(!validInput){
				Console.Write("Please enter number of players (2-4): ");
				string tempText = Console.ReadLine();

				if(int.TryParse(tempText, out numberOfPlayers)){
					if(numberOfPlayers >= 2 && numberOfPlayers <= 4){
						validInput = true;
					} else{
						Console.WriteLine("Must be within the range...");
					}
				} else{
					Console.WriteLine("Must be a valid number...");
				}
			}

			for(int i = 1; i <= numberOfPlayers; i++){
				Console.Write($"Enter player {i}'s name: ");
				string name = Console.ReadLine();

				while(string.IsNullOrWhiteSpace(name)){
					Console.Write("Player name cannot be empty...try again: ");
					name = Console.ReadLine();
				}

				Players.Add(new Player(name));
			}

			Game game = new Game(Players);
			game.Play();
		}
	}
}
