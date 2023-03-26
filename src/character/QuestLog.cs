using Godot;
using System;

public class QuestLog : VBoxContainer
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public void SetupText(Quest quest){
		var label = GetNode<Label>("QuestText");
		label.Text = quest.Task;
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
