using Godot;
using System;

public class AppearanceArrow : TextureButton
{
	[Signal]
	delegate void ChangeAppearance(string name,int adjustment);
	
	public override void _Ready()
	{
		
	}

	private void OnPressed()
	{
		int num = 1;
		if(Name.Contains("Left")){
			num = -1;
		}
		EmitSignal("ChangeAppearance",GetParent().Name,num);
	
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}




