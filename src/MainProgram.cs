using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{
	
	class MainProgram{
		public static void Main(string[] args){
			List<Player> Players = new List<Player>();

			Console.Clear();
			Console.WriteLine("Welcome to Snake and Ladders!");
			Console.Write("Please enter number of players (2-4): ");
			int numberOfPlayers = Convert.ToInt32(Console.ReadLine());

			for(int i = 1; i <= numberOfPlayers; i++){
				Console.Write($"Enter player {i}'s name: ");
				string name = Console.ReadLine();
				Players.Add(new Player(name));
			}

			Game game = new Game(Players);
			game.Play();
		}
	}
}
