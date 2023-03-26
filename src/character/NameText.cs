using Godot;
using System;

public class NameText : TextEdit
{
	
	[Signal]
	delegate void TextChanged(string source);
	
	public override void _Ready()
	{
		
	}
	

	private void OnTextChanged()
	{
		EmitSignal("TextChanged",this.Name);
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

