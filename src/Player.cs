using System;
using System.Collections.Generic;

namespace SnakeNLadderGame{

	public class Player{
		public string Name {get; set;}
		public int Position {get; set;}
		public string Status {get; set;}
		public int StatusCounter {get; set;}
		public Dictionary<int,string> Skills {get; set;}
		private Dictionary<string,string> SkillUsage {get; set;}

		public Player(string name){
			Name = name;
			Position = 0;
			Status = "Normal";

			Skills = new Dictionary<int,string>{
				{1, "Stun"}, {2, "Ladder Time!"}, {3, "Oh No! Snake"}
			};

			SkillUsage = new Dictionary<string, string>{
				{"Stun", "Time freezes to those that oppose you..."}, 
				{"Ladder Time!", "Lady luck smiles at you, you are granted greater teleportation..."},
				{"Oh No! Snake", "You inflicted a greater curse towards your foes..."}
			};
		}

		public void useSkill(int skillNumber, int currentPlayerIndex, List<Player> Players){
			if(Skills.ContainsKey(skillNumber)) {
				string skillUsed = Skills[skillNumber];

				switch(skillUsed){
					case "Stun":
						Console.WriteLine($"{SkillUsage[skillUsed]}");
						Console.Write($"Who dares oppose you?....Select player's number [1 - {Players.Count}]: ");
						int playerNumber = Convert.ToInt32(Console.ReadLine());
						Player foe = Players[playerNumber - 1];
						Console.WriteLine($"\nUsed [Stun] to player [{foe.Name}]");
						foe.Status = "Stunned";
						Console.WriteLine($"[{foe.Name}] is now {foe.Status}");
						break;

					case "Ladder Time!":
						Console.WriteLine($"{SkillUsage[skillUsed]}");
						Player currentPlayer = Players[currentPlayerIndex];
						Console.Write("Where would you like to go? [1-70]: ");
						int newPosition = Convert.ToInt32(Console.ReadLine());
						int newRow = (newPosition - 1) / 10;

						if(newRow > 6) {
							Console.WriteLine("Greed is a sin... Teleportation lost its effect...");
							break;
						}
						else{
							currentPlayer.Position = newPosition;
							Console.WriteLine($"Player [{currentPlayer.Name}] moved to position {newPosition}...");
							break;
						}

					case "Oh No! Snake":
						break;
				}
			}
		}

		public bool checkStatus(Player player){
			bool isStunned = false;
			if(player.Status.Equals("Stunned")) isStunned = true;
			else isStunned = false;
			
			return isStunned;
		}

		/*private bool checkCooldown(){
			
		}*/
	}
}