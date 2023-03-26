using Godot;
using System;

public class Condition : Node
{

private string _method;
private object[] _properties;


public Condition(){

}

public Condition(string method, object[] properties){
	Method = method;
	Properties = properties;
}

public string Method{
	get {return _method;}
	set {_method = value;}
}	
public object[] Properties{
	get{return _properties;}
	set{_properties = value;}
}
//Beastiary of keys.
//Dictionary of spawns that relate to these keys.
//Check if relationship equals. 

//*ALL conditions should return true or false.

public bool CheckRelationship(Player player,Relationship relationship){
try{
	
	if(player.companion.Relationship == relationship){
	return true;
	}
	return false;
}
catch{
	return false;
}
}
public bool CheckAgilityGTMight(Player player){
	if(player.main.Agility > player.main.Might){
		return true;
	}
	return false;
}
public bool CheckQuestStage(Player player,string key,int stage){
	try{
		Quest quest = player.GetQuest(key);
		if(stage >= quest.Stage){
			return true;
		} 
			return false;
		}
	catch{
		return false;
	}
	
}
public bool CheckQuestCompleted(Player player,string key){
	if(player.CheckQuestComplete(key)){
		return true;
	}
	return false;
}
}

