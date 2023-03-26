using Godot;
using System;
using System.Collections.Generic;

public class Profession
{
	private string _classname;
	public Dictionary<string,Skill> skillset;
	private static Dictionary<string,Profession> template = new Dictionary<string,Profession>(){
		{"Soldier",new Profession("Soldier",20,5,8,4,new Dictionary<string,Skill>(){{"Sword",new Skill(SkillLevel.Expert)},{"Shield",new Skill(SkillLevel.Expert)},{"Armsmaster",new Skill(SkillLevel.Expert)},{"Light Armor",new Skill(SkillLevel.Expert)},{"Medium Armor",new Skill(SkillLevel.Expert)},{"Heavy Armor",new Skill(SkillLevel.Expert)},{"Bodybuilding",new Skill(SkillLevel.Expert)}})},
		{"Thief",new Profession("Thief",18,3,5,3,new Dictionary<string,Skill>(){{"Dagger",new Skill(SkillLevel.Expert)},{"Thievery",new Skill(SkillLevel.Expert)},{"Armsmaster",new Skill(SkillLevel.Expert)}})},
		{"Warrior Priest",new Profession("Warrior Priest",16,4,6,4,new Dictionary<string,Skill>(){{"Mace",new Skill(SkillLevel.Expert)},{"Compulsion Magic",new Skill(SkillLevel.Expert)},{"Armsmaster",new Skill(SkillLevel.Expert)}})},
		{"Archer",new Profession("Archer",16,4,5,3,new Dictionary<string,Skill>(){{"Archery",new Skill(SkillLevel.Expert)},{"Light Armor",new Skill(SkillLevel.Expert)},{"Medium Armor",new Skill(SkillLevel.Expert)}})},
		{"Apprentice",new Profession("Apprentice",12,3,8,4,new Dictionary<string,Skill>(){{"Elemental Magic",new Skill(SkillLevel.Expert)},{"Staff",new Skill(SkillLevel.Expert)},{"Meditation",new Skill(SkillLevel.Expert)}})}
	};
	private int _hpbase;
	private int _hplevel;
	private int _energybase;
	private int _energylevel;
	//instance class
	public Profession(){
		
	}
	//Preset class
	public Profession(string classname, int hpbase, int hplevel, int energybase, int energylevel, Dictionary<string,Skill> skills)
	{
		_classname = classname;
		_hpbase = hpbase;
		_hplevel = hplevel;
		_energybase = energybase;
		_energylevel = energylevel;
		skillset = skills;
	}
	//Dictionary Class
	public Profession GetClass(string classname){
		return template[classname];
	}

	public string ClassName{
		get{return _classname;}
		set{_classname = value;}
	}
	public int HPBase{
		get{return _hpbase;}
	} 
	public int EnergyBase{
		get{return _energybase;}
	}
		public Dictionary<string,Skill> Skillset{
		get{return skillset;}
		set{skillset = value;}
	}

	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
