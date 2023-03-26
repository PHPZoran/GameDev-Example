using Godot;
using System;

public class Action : Node
{

private string _method;
private object[] _properties;
[Signal]
delegate void QuestNotify();

public override void _Ready(){
	
}
public Action(){
	
}
public Action(string method,object[] properties){
	_method = method;
	_properties = properties;
}

public string Method{
	get {return _method;}
	set {_method = value;}
}	
public object[] Properties{
	get{return _properties;}
	set{_properties = value;}
}

public void SetVariable(Player player,String variable,int number){
		player.SetVariable(variable,number);
}
public void IncrementVariable(Player player,String variable,int number){
	int original = player.FindVariable(variable);
	number += original;
	player.SetVariable(variable,number);
}

public void StartQuest(Player player,string key){
	GD.Print("Starting Quest");
	Quest quest = new Quest();
	try{
	quest = quest.GetQuestData(key);
	player.AddQuest(key,quest);
	EmitSignal("QuestNotify");
	}
	catch{
		GD.Print("Mismatching quest key.");
	}
} 
public void SetQuestStage(Player player,string key,int stage){
	try{
		player.SetStage(key,stage);
	} 
	catch{
		GD.Print("Mismatching quest key.");
	}
}
public void CompleteQuest(Player player,string key){
	GD.Print("This fired");
	player.CompleteQuest(key);
	EmitSignal("QuestNotify");
}
public void CreateItem(Player player,String variable,int number){
	
	//Get item data with variable.
	GD.Print("Creating Item...");
	Item item = new Item();
	item = item.Data(variable);
	bool inserted = false;
	int slot = 0;
	if(item.StackSize < number && item.StackSize > 1){
		CreateItem(player,variable,number-item.StackSize);
		item.Quantity = item.StackSize;
	}
	else if(item.StackSize < 2 && number > 1){
		CreateItem(player,variable,number-1);
	}
	while(!inserted && slot < 30){
		if(player.CheckSlotEmpty(slot)){
			player.PlaceItem(item,slot);
			inserted = true;
			GD.Print("Inserting...");
		}
		slot++;
	}
	if(inserted != true){
		GD.Print("No room. Implement Inventory overflow and signal gamescreen later.");
	}
}
	// Called when the node enters the scene tree for the first time.


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
