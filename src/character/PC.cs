using Godot;
using System;
using System.Collections.Generic;

public class PC : Character
{
private Profession _profession = new Profession();	
private int _level;
private int _nextLevel;
private int _xp;
private int _statpoints;
private int _skillpoints;
private int _might = 0;
private int _agility = 0;
private int _vitality = 0;
private int _magic = 0;
private int _willpower = 0;
private int _trickery = 0;
private Dictionary<string,Skill> _skillset = new Dictionary<string,Skill>();
private static Dictionary<int,int> experiencetable = new Dictionary<int,int>(){
	{1,0},
	{2,1000}
};
public Dictionary<string, Item> _equipment = new Dictionary<string, Item>(){
		{"Helmet",null},
		{"Armor",new Item()},
		{"WeaponRight",null},
		{"WeaponLeft",null},
		{"Necklace",null},
		{"Ring",null}
		};
public PC(){
		
	}
public PC(PC character)
{
	FirstName = character.FirstName;
	LastName = character.LastName;
}

public Profession Profession{
	get{return _profession;}
	set{_profession = value;}
}
public int XP{
	get{return _xp;}
	set{_xp = value;}
} 
public int SkillPoints{
	get{return _skillpoints;}
	set{_skillpoints = value;}
}
public int StatPoints{
	get{return _statpoints;}
	set{_statpoints = value;}
} 
public int Level{
	get{return _level;}
	set{_level = value;}
}
public int NextLevel{
	get{return _nextLevel;}
	set{_nextLevel = value;}
}
public int Might{
	get {return _might;}
	set {_might = value; /*DetermineDamage();*/}
}
public int Agility{
	get {return _agility;}
	set {_agility = value;}
}
public int Vitality{
	get {return _vitality;}
	set {_vitality = value;}
}
public int Magic{
	get {return _magic;}
	set {_magic = value;}
}
public int Willpower{
	get {return _willpower;}
	set {_willpower = value;}
}
public int Trickery{
	get {return _trickery;}
	set {_trickery = value;}
}
public void Equip(string key, Item item){
	try{
		_equipment[key] = item;
		if(key == "WeaponRight" || key == "WeaponLeft"){
			DetermineDamage();
		} 
		else if(key == "Armor" || key == "Helmet"){
		//	DetermineArmor();
		GD.Print(_equipment[key].Name);
		}
	}
	catch{
		GD.Print("Key invalid");
	}
}
public void DetermineDamage(){
if(this.GetEquipment("WeaponRight") != null && this.GetEquipment("WeaponLeft") != null)
	{
		if(this.GetEquipment("WeaponRight").Category == Category.Weapon && this.GetEquipment("WeaponLeft").Category == Category.Weapon){
			this.DamageMin = (this.GetEquipment("WeaponRight").DamageMin + this.GetEquipment("WeaponLeft").DamageMin)/2 + this.Might;
			this.DamageMax = (this.GetEquipment("WeaponRight").DamageMax + this.GetEquipment("WeaponLeft").DamageMax)/2 + this.Might;
		}
		else if(this.GetEquipment("WeaponLeft").Category == Category.Weapon){
			this.DamageMin = this.GetEquipment("WeaponLeft").DamageMin + this.Might;
			this.DamageMax = this.GetEquipment("WeaponLeft").DamageMax + Might;
		}
		else{
			this.DamageMin = this.GetEquipment("WeaponRight").DamageMin + this.Might;
			this.DamageMax = this.GetEquipment("WeaponRight").DamageMax + Might;
		}
	}
	else if(this.GetEquipment("WeaponRight") != null){
		this.DamageMin = this.GetEquipment("WeaponRight").DamageMin + this.Might;
		this.DamageMax = this.GetEquipment("WeaponRight").DamageMax + Might;
	}
	else if(this.GetEquipment("WeaponLeft") != null){
		this.DamageMin = this.GetEquipment("WeaponLeft").DamageMin + this.Might;
		this.DamageMax = this.GetEquipment("WeaponLeft").DamageMax + Might;
	}	
	else{
		this.DamageMin = Might;
		this.DamageMax = Might;
	}
} 
	
public void DetermineArmor(){
	PierceAC = 0;
	MeleeAC = 0;
	MagicAC = 0;
	if(this.GetEquipment("Armor") != null){
		PierceAC += this.GetEquipment("Armor").PierceAC;
		MeleeAC += this.GetEquipment("Armor").MeleeAC;
		MagicAC += this.GetEquipment("Armor").MagicAC;
	}
	if(this.GetEquipment("Helmet") != null){
		PierceAC += this.GetEquipment("Helmet").PierceAC;
		MeleeAC += this.GetEquipment("Helmet").MeleeAC;
		MagicAC += this.GetEquipment("Helmet").MagicAC;
	}
}	
public Item GetEquipment(string key){
	try{return _equipment[key];}
	catch{return null;}
}
public void InitializeHP()
{
	MaxHealth = this.Profession.HPBase + (int)Math.Floor((this.Vitality - 10)/2.0);
	CurrentHealth = MaxHealth;
}
public void InitializeEnergy()
{
	MaxEnergy = this.Profession.EnergyBase + (int)Math.Floor((this.Vitality - 10)/2.0);
	CurrentEnergy = MaxEnergy;	
}	
public Dictionary<string,Skill> Skillset{
	get{return _skillset;}
}
public Dictionary<string,Item> Equipment{
	get{return _equipment;}
	set{_equipment = value;}
}
}

