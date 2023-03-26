using Godot;
using System;

public class ButtonSex : TextureButton
{
	private readonly static Texture male = (Texture)GD.Load("assets/images/Textures/Buttons/Male.png");
	private readonly static Texture female = (Texture)GD.Load("assets/images/Textures/Buttons/Female.png");
	private readonly static Texture maleHover = (Texture)GD.Load("assets/images/Textures/Buttons/MaleHover.png");
	private readonly static Texture femaleHover = (Texture)GD.Load("assets/images/Textures/Buttons/FemaleHover.png");
	private Character character;
	private Player player;
	
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		if(this.Name.Contains("Companion")){
			character = player.companion;
		}
		else{
			character = player.main;
		}
	}
	
	private void OnButtonSexToggled(bool button_pressed)
	{
		if(button_pressed){
			this.TextureNormal = female;
			this.TextureHover = femaleHover;
			
			character.Sex = Sex.Female;
		}
		else{
			this.TextureNormal = male;
			this.TextureHover = maleHover;
			character.Sex = Sex.Male;
			
			//Else change companion to male.
		}
	}

}


