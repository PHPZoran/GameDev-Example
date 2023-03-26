using Godot;
using System;
using System.Collections.Generic;

public class Beastiary
{
//Beastiary has a dictionary that contains all of the characters except player and companion.
//Key->Character(first,last,health,mindam,maxdam,might,agility,endur,mag,will,trick,loc,fac)
private Dictionary<String, Character> _beast = new Dictionary<String, Character>(){
	{"startassassin",new Character("Assassin",null,6,12,12,3,"STARTDEFEAT")},
	{"startassassintest",new Character("Assassin",null,6,12,12,3,"STARTDEFEAT")}
};
public Beastiary(){
	
}
public Character GetCreature(string key){
	GD.Print(key);
	return _beast[key];
}
}
