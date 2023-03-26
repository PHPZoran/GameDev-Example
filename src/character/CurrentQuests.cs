using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class CurrentQuests : NinePatchRect
{
	private Player player;
	int min = 1;
	int max = 8;
	bool loading = true;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		do{
			loading = FetchQuests();
		}
		while(loading);
	}
	
	public bool FetchQuests(){
		for(int i = min; i <= max; i++)
		{
			try{
				Quest quest = player.GetQuests().Values.ElementAt(i-1);
				VBoxContainer container = null;
				var scene = GD.Load<PackedScene>("res://src/character/Quest.tscn");
				var instance = scene.Instance<QuestLog>();
				if(max >= i + 4){
					container = (VBoxContainer)GetChild(0);
				}
				else{
					container = (VBoxContainer)GetChild(1);
				}
				container.AddChild(instance);	
				instance.SetupText(quest);
				}
			catch{
				GetNode<TextureButton>("NextButton").Disabled = true;
				return false;
				}
		}
		if(max == player.GetQuests().Count){
			GetNode<TextureButton>("NextButton").Disabled = true;
		}
		return false;
	}
	private void OnNextPressed()
	{
		loading = true;
		GetNode<TextureButton>("BackButton").Disabled = false;
		ClearQuests("QuestPage1");
		ClearQuests("QuestPage2");		
		min += 8;
		max += 8;
		do{
			loading = FetchQuests();
		}
		while(loading);
	}
	private void OnBackPressed()
	{
		loading = true;
		GetNode<TextureButton>("NextButton").Disabled = false;
		ClearQuests("QuestPage1");
		ClearQuests("QuestPage2");
		min -= 8;
		max -= 8;
		do{
			loading = FetchQuests();
		}
		while(loading);
		if(min == 1){
			GetNode<TextureButton>("BackButton").Disabled = true;
		}
	}
	private void ClearQuests(string name){
		var container1 = GetNode(name);
		foreach(VBoxContainer child in container1.GetChildren()){
			child.QueueFree();
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}






