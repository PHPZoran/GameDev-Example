using Godot;
using System;
using System.Collections.Generic;

public class Race 
{
	private string _name = "Human";
	private Dictionary<string,int> _minstats;
	private static Dictionary<string,Race> _playableraces = new Dictionary<string,Race>(){
		{"Archfey",new Race("Archfey",new Dictionary<string,int>(){{"Might",7},{"Agility",14},{"Vitality",7},{"Magic",14},{"Willpower",9},{"Trickery",11}})},
		{"Elf",new Race("Elf",new Dictionary<string,int>(){{"Might",10},{"Agility",12},{"Vitality",9},{"Magic",11},{"Willpower",10},{"Trickery",10}})},
		{"Human",new Race("Human",new Dictionary<string,int>(){{"Might",11},{"Agility",9},{"Vitality",11},{"Magic",7},{"Willpower",14},{"Trickery",10}})},
		{"Katari",new Race("Katari",new Dictionary<string,int>(){{"Might",14},{"Agility",11},{"Vitality",12},{"Magic",9},{"Willpower",7},{"Trickery",9}})}
	};
	
	public Race(){
		
	}
	//For Custom Race
	public Race(string name){
		_name = name;
	}
	//For Created Races
	public Race(string name, Dictionary<string,int> minstats){
		_name = name;
		_minstats = minstats;
	}
	
	public Race SetRace(string name){
		
		return _playableraces[name];
	}
	
	public string Name{
		get{return _name;}
		set{_name = value;}
	}
	public int GetMinValue(string stat)
	{
		return _minstats[stat];
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
