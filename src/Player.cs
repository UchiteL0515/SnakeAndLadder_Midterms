using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{

	public class Player{
		public string Name {get; set;}
		public int Position {get; set;}
		public Dictionary<int,string> Skills {get; set;}

		public Player(string name){
			Name = name;
			Position = 0;

			Skills = new Dictionary<int,string>{
				
			}
		}
	}
}
