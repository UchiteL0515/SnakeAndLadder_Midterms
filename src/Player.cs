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
		private Dictionary<string,int> SkillCooldowns {get; set;}
		private Dictionary<string,int> LastUsedTurn {get; set;}

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

			SkillCooldowns = new Dictionary<string, int>{
				{"Stun", 3}, {"Ladder Time!", 10}, {"Oh No! Snake", 25}
			};
			
			LastUsedTurn = new Dictionary<string, int>{
				{"Stun", -99}, {"Ladder Time!", -99}, {"Oh No! Snake", -99}
			};
		}

		public void useSkill(int playerTurnCount, int skillNumber, int currentPlayerIndex, List<Player> Players){
			if(Skills.ContainsKey(skillNumber)) {
				string skillUsed = Skills[skillNumber];

				//debugging purposes
				Console.WriteLine($"[{Name}] Used {skillUsed} | Last Used Turn: {LastUsedTurn[skillUsed]}");

				if(checkCooldown(skillUsed, playerTurnCount)){
        			    int remainingCooldown = SkillCooldowns[skillUsed] - (playerTurnCount - LastUsedTurn[skillUsed]);
			            Console.WriteLine($"Skill [{skillUsed}] is still on cooldown! Wait {remainingCooldown} more turns.");
			            return;
        			}

				LastUsedTurn[skillUsed] = playerTurnCount;
				Player currentPlayer = Players[currentPlayerIndex];

				switch(skillUsed){
					case "Stun":
						Console.WriteLine($"{SkillUsage[skillUsed]}");
						Console.Write($"Who dares oppose you?....Select player's number [1 - {Players.Count}]: ");

						int playerNumber = Convert.ToInt32(Console.ReadLine());
						Player foe = Players[playerNumber - 1];

						Console.WriteLine($"\nUsed [Stun] to player [{foe.Name}]");
						foe.Status = "Stunned";
						foe.StatusCounter = 2;
						Console.WriteLine($"[{foe.Name}] is now {foe.Status}");
						break;

					case "Ladder Time!":
						Console.WriteLine($"{SkillUsage[skillUsed]}");
						currentPlayer = Players[currentPlayerIndex];

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
						Console.WriteLine($"{SkillUsage[skillUsed]}");
						Console.Write($"Who has incurred your wrath?...Select player's number [1 - {Players.Count}]: ");

						playerNumber = Convert.ToInt32(Console.ReadLine());
						foe = Players[playerNumber - 1];
						Console.WriteLine($"\nUsed [Oh No! Snake] to player [{foe.Name}]");

						foe.Position -= 50;
						if(foe.Position < 0){
							foe.Position = 1;
							Console.WriteLine($"[{foe.Name}] reached beyond the bottom tiles and is moved to position 1...");
						} else{
							Console.WriteLine($"[{foe.Name}] moved 50 tiles down...");
						}

						playerNumber = 0;
						foe = null;
						break;
				}
			}
		}

		public bool checkStatus(){
			return Status.Equals("Stunned");	
		}

		public bool checkCooldown(string skill, int playerTurnCount){
			int lastUsedTurn = LastUsedTurn[skill];
			int cooldown = SkillCooldowns[skill];

			return (playerTurnCount - lastUsedTurn) < cooldown;
		}
	}
}
