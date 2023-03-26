// BotStats.cs
using System;
using Godot;
using System.Collections.Generic;
	public enum Origin{
		Spring,
		Summer,
		Autumn,
		Winter,
		FourKingdoms
	}
	public enum Difficulty{
		Story,//The player and their companion can never die. The execution defeat scenario will never play, always ensuring the player will be a survivor. 
		Standard,// Balanced around this. No alterations to game rules.
		Strategist//Enemies are much less likely to play other defeat scenarios except execution. A.I. is crueler.
	}
	[Serializable]
	public class Player : Node
	{
		public List<Character> party;
		//Combat party, doesn't need to be exported.
		public List<Character> battleteam;
		public int _startpoints = 25;
		public Origin origin;
		public PC main = new PC();
		public PC companion = new PC();
		public string lastText;
		public Difficulty difficulty;
		public bool merciless = false;
		public Item[] inventory = new Item[30];
		public Item lostItem = null;
		public int _gold;
		public Godot.Collections.Dictionary<String, int> variable = new Godot.Collections.Dictionary<String, int>(){
			{"Check",0}
			};
		//This was static prior. Reinsert if there are issues.
		private Dictionary<String, Quest> activequests = new Dictionary<String, Quest>();
		public Dictionary<String, Quest> completedquests = new Dictionary<String, Quest>();
	
		public Player(){

		}

		public Dictionary<String, Quest> GetQuests(){
			return activequests;
		}
		public Quest GetQuest(string key){
			return activequests[key];
		}
		public Dictionary<String, Quest> ActiveQuests{
			get{return activequests;}
			set{activequests = value;}
		}
		public bool CheckQuestComplete(string key){
			if(completedquests.ContainsKey(key)){
				return true;
			}
			return false;
		} 
		public void SetStage(string key,int stage){
			Quest quest = GetQuest(key);
			quest.Stage = stage;
		}
		public void CompleteQuest(string key){
			Quest quest;
			try{
				if(activequests.ContainsKey(key)){
					quest = activequests[key];
					activequests.Remove(key);
				}
				else{
					quest = new Quest();
					quest = quest.GetQuestData(key);
				}
				Gold += quest.Gold;
				main.XP += quest.XP;
				companion.XP += quest.XP;
			}
			catch{
				GD.Print("Error in quest implementation.");
			}
		}
		public void AddQuest(string key,Quest quest){
				activequests.Add(key,quest);
		}
		public int FindVariable(string key){
 			int value = 0;
	   		if (variable.TryGetValue(key, out value)){
				return value;
			}
			else{
				return 0;
			}
		}
		public void SetVariable(string key,int number){
			try{
					variable[key] = number;
			}
			catch{
				variable.Add(key,number);
			}
		}

		public Difficulty Difficulty{
			get{return difficulty;}
			set{difficulty = value;}
		}
		public string RetrieveParent(){
			string parent = "father";
			if(main.Sex == Sex.Male){
				parent = "mother";
			}
			return parent;
		} 
		public bool CheckSlotEmpty(int key){
			if(inventory[key] == null){
				return true;
			}
			return false;
		} 
		public void PlaceItem(Item item,int slot){
			inventory[slot] = item;
		}
		public void Load(Player data){
			main = data.main;
			companion = data.companion;
		}
	public int StartPoints{
		get{return _startpoints;}
		set{_startpoints = value;}
	} 
	public int Gold{
		get{return _gold;}
		set{_gold = value;}
	}
	public PC Main{
		get{return main;}
		set{main = value;}
	}
	public PC Companion{
		get{return companion;}
		set{companion = value;}
	}
	public string LastText{
		get{return lastText;}
		set{LastText = value;}
	}
		
	}

