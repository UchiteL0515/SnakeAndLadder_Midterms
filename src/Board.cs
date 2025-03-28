using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{
	
	public class Board{
		private const int Size = 100;
		private Dictionary<int,int> Snakes {get; set;} 
		private Dictionary<int,int> Ladders {get; set;}

		public Board(){
			Snakes = new Dictionary	<int,int>{
				{17,7}, {54,34}, {62,19}, {64,60},
				{87,36}, {93,73}, {95,75}, {98,79}
			};

			Ladders = new Dictionary <int,int>{
				{1,38}, {4,14}, {9,31}, {21,42},
				{28,84}, {51,67}, {72,91}, {80,99}
			};
		}
		
		public int checkPosition(int position){
			if(Snakes.ContainsKey(position)){
				Console.WriteLine($"Oh no! hit a snake at position {position}... Going down to {Snakes[position]}.");
				return Snakes[position];
			} else if(Ladders.ContainsKey(position)){
				Console.WriteLine($"Nice! a ladder is at position {position}... Climbing up to {Ladders[position]}.");
				return Ladders[position];
			} else return position;
		}

		public void drawCell(List<Player> Players, int position){
			bool isPlayerHere = false;
			foreach(var player in Players){
				if(player.Position == position){
					Console.Write($"[ {player.Name[0]} ]");
					isPlayerHere = true;
					break;
				}
			}

			if(!isPlayerHere){
				Console.Write("[   ]");
			}
		}

		public void drawStateBoard(List<Player> player){
			for(int row = 9; row >= 0; row--){
				if(row % 2 == 0){
					for(int col = 0; col < 10; col++){
						int position = row * 10 + col + 1;
						drawCell(player, position);
					}
				} else{
					for(int col = 9; col >= 0; col--){
						int position = row * 10 + col + 1;
						drawCell(player, position);
					}
				}

				Console.WriteLine();
			}
		}

		public void drawPlacementBoard(){
			for(int row = 9; row >=0; row--){
				if(row % 2 == 0){
					for(int col = 0; col < 10; col++){
						int placement = row * 10 + col + 1;
						Console.Write($"[ {placement:D2} ]");
					}
				} else{
					for(int col = 9; col >= 0; col--){
						int placement = row * 10 + col + 1;
						Console.Write($"[ {placement:D2} ]");
					}
				}

				Console.WriteLine();
			}
		}

		public void displayBoards(List<Player> player){
			Console.WriteLine("Board State: ");
			drawStateBoard(player);

			Console.WriteLine("\nBoard Placements:");
			drawPlacementBoard();
		}
	}
}
