using Godot;
using System;
using System.Reflection;
public class DecrementStat : TextureButton
{
	[Signal]
	delegate void DecrementAttribute(string name);
	
	public override void _Ready()
	{
		
	}
	private void OnDecrementButtonPressed()
	{
		GD.Print("Decrementing!");
		EmitSignal("DecrementAttribute", GetParent().Name);
	}
	private void ToggleOff()
	{
		this.Visible = false;
		
	}
	private void ToggleOn()
	{
		this.Visible = true;
		
	}		
	private void DetermineToggle(string name, PC character)
	{
		if(GetNode("../../../").Name == name){
			PropertyInfo attribute = typeof(PC).GetProperty(GetParent().Name);
			int value = (int)attribute.GetValue(character, null);
			if(value == character.Race.GetMinValue(GetParent().Name)){
				ToggleOff();
			}
			else{
				ToggleOn();
			}
		}
	}
}


