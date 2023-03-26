using Godot;
using System;

public class StatLabel : Label
{
	[Signal]
	delegate void LabelHovered(string name);
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private void OnLabelFocus()
	{
		EmitSignal("LabelHovered", GetParent().Name);
	}
	
	private void OnMouseEntered()
	{
		EmitSignal("LabelHovered", GetParent().Name);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}






