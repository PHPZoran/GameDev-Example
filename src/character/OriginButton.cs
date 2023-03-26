using Godot;
using System;

public class OriginButton : Button
{
	[Signal]
	delegate void SignalOriginPressed(string name);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

private void OnOriginButtonPressed()
{
	EmitSignal("SignalOriginPressed", Name);
}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}


