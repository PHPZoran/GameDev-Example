using Godot;
using System;
using System.Collections.Generic;

public enum Orientation{
	Hetero,
	Bi,
	Homo,
	None
}
public enum Relationship{
	sibling,
	friend,
	stranger,
	rival
}

public enum Sex{
	Male,
	Female
}

public class Character
{
	
//All characters have:
//First and last name.
public string _firstname = "";
private string _lastname;

private Race _race = new Race();
private int currentHealth;
private int maxHealth;
private string _faction;
private int _initiative = 0;
private int _damagemin;
private int _damagemax;
private string _location;
private int _age;
private Sex _sex;
private Orientation _orientation;
private int _gold;
private int _currentEnergy;
private int _maxEnergy;
private int _pierceAC;
private int _meleeAC;
private int _magicAC;
private int _fireRes;
private int _waterRes;
private int _earthRes;
private int _airRes;
private int _mentalRes;
private int _physicalRes;

private Relationship _relationship;
		
		public Character(){
			
		}
	public Character(string first,string last,int health,int mindamage,int maxdamage, int initiative, string location = null,string faction = "Enemy"){
		_firstname = first;
		_lastname = last;
		currentHealth = health;
		maxHealth = health;
		//Turn the below into a double.
		_initiative = initiative;
		_damagemin = mindamage;
		_damagemax = maxdamage;
		_location = location;
		_faction = faction;
		
	}



//This is for Defeats, the first living creature determines the location.
//It could double for setting an NPC location.
public string Location{
	get{return _location;}
}
public string FullName{
	get {if(LastName != null){return _firstname + " " + _lastname;}else{return _firstname;}}
}
public int Initiative{
	get {return _initiative;}
	set {_initiative = value;}
}

public string FirstName{
	get {return _firstname;}
	set {_firstname = value;}
}
public string LastName{
	get {return _lastname;}
	set{_lastname = value;}
}

public int DamageMin{
	get {return _damagemin;}
	set {_damagemin = value;}
}
public int DamageMax{
	get {return _damagemax;}
	set {_damagemax = value;}
}
  public string Faction   // property
  {
	get { return _faction; }
	set { _faction = value; }
  }
  public int CurrentHealth   // property
  {
	get { return currentHealth; }
	set { currentHealth = value; }
  }
public int MaxHealth{
	get{return maxHealth;}
	set{maxHealth = value;}
} 
public int CurrentEnergy{
	get{return _currentEnergy; }
	set{_currentEnergy = value;}
}
public int MaxEnergy{
	get{return _maxEnergy;}
	set{_maxEnergy = value;}
}
public int PierceAC{
	get{return _pierceAC;}
	set{_pierceAC = value;}
}
public int MeleeAC{
	get{return _meleeAC;}
	set{_meleeAC = value;}
}
public int MagicAC{
	get{return _magicAC;}
	set{_magicAC = value;}
}
public int Gold{
	get{return _gold;}
	set{_gold = value;}
}

public int Age{
	get{return _age;}
	set{_age = value;}
}
public Orientation Orientation{
	get{return _orientation;}
	set{_orientation = value;}
}

public Sex Sex{
	get{return _sex;}
	set{_sex = value;}
}
public int FireRes{
	get{return _fireRes;}
	set{_fireRes = value;}
}
public int WaterRes{
	get{return _waterRes;}
	set{_waterRes = value;}
}
public int EarthRes{
	get{return _earthRes;}
	set{_earthRes = value;}
}
public int AirRes{
	get{return _airRes;}
	set{_airRes = value;}
}
public int MentalRes{
	get{return _mentalRes;}
	set{_mentalRes = value;}
}
public int PhysicalRes{
	get{return _physicalRes;}
	set{_physicalRes = value;}
}
public Race Race{
	get{return _race;}
	set{_race = value;}
}
public Relationship Relationship{
		get{return _relationship;}
		set{_relationship = value;}
	} 
}
