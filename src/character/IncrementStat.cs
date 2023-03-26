using Godot;
using System;
using System.Reflection;

public class IncrementStat : TextureButton
{
	
	[Signal]
	delegate void IncrementAttribute(string name);
	
	public override void _Ready()
	{
		
	}
	private void OnIncrementButtonPressed()
	{
		EmitSignal("IncrementAttribute", GetParent().Name);
	}
	private void ToggleOff(string name)
	{
		if(GetNode("../../../").Name == name)
		{
			this.Visible = false;
		}	
		
	}
	private void ToggleOff()
	{
		this.Visible = false;
		
	}
	private void ToggleOn()
	{
		this.Visible = true;
		
	}	
	private void ToggleOn(string name)
	{
		if(GetNode("../../../").Name == name){
			this.Visible = true;
		}
	}
	private void DetermineToggle(string name, PC character)
	{
		if(GetNode("../../../").Name == name){
			PropertyInfo attribute = typeof(PC).GetProperty(GetParent().Name);
			int value = (int)attribute.GetValue(character, null);
			if(value == character.Race.GetMinValue(GetParent().Name) * 2){
				ToggleOff();
			}
			else{
				ToggleOn();
			}
		}
	}


}



