using Godot;
using System;

public class Reply : Button
{
		[Signal]
delegate void ReplySelected(int selected);
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	this.Connect(nameof(ReplySelected), GetNode("/root/GameScreen"), "HandleReply");	
	}
	private void _on_ReplyButton_pressed()
	{
	EmitSignal("ReplySelected", GetIndex());
	}
	public void SetupText(string key,DialogueLine line = null){
		var stylebox = (StyleBox)GD.Load("res://src/themes/LocationBoxNormal.tres");
			
		//stylebox.GetStyleBox("normal");
		//var stylebox;
			this.Text = key;
			if(line.state == State.end || line.state == State.location){
				AddStyleboxOverride("normal", stylebox);
			}
		}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



