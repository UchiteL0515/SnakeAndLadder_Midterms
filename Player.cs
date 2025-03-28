using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{

	public class Player{
		public string Name {get; set;}
		public int Position {get; set;}
		public List<string> Skills {get; set;}

		public Player(string name){
			Name = name;
			Position = 0;
		}
	}
}
