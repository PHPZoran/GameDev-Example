using Godot;
using System;

public class Appearance : GridContainer
{
	[Signal]
	delegate void SignalSpriteChange(string name, int adjustment);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void OnChangeAppearanceRequest(string name, int adjustment)
	{
		GD.Print(name);
		GD.Print(adjustment);
		EmitSignal("SignalSpriteChange",name,adjustment);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

