using Godot;
using System;

public class CharacterSprite : Node2D
{
	
	private readonly Color origin = new Color("#f9e8c7");
	
	Color new_color = new Color("#663931");
	Color second_color = new Color("#f9e8c7");
	private Sprite body;
	private Sprite hands;
	private Sprite eyes;
	private Sprite hair;
	private Color[] skincolor = new Color[2]{new Color("#663931"),new Color("#f9e8c7")};
	public override void _Ready()
	{
		body = GetNode<Sprite>("Body");
		hands = GetNode<Sprite>("Hands");
		eyes = GetNode<Sprite>("Eyes");
		hair = GetNode<Sprite>("Hair");
		if(!GetTree().Root.HasNode("Generator")){
			//Load data based off of name.
		}
	}
	public void OnAppearanceChangeCall(string name,int adjustment){
		GD.Print(name);
		if(name == "SkinColor"){
			ChangeColor(body);
			ChangeColor(hands);
		}
		else if(name == "EyeColor")
		{
			ChangeColor(eyes);
		}
		else if(name == "HairStyle")
		{
			ChangeHair(hair);
		}
		else if(name == "HairColor")
		{
			ChangeColor(hair);
		}
	}
	public void ChangeColor(Sprite sprite){
		Image image = sprite.Texture.GetData();
	
		for(int y = 0; y < image.GetHeight(); y++){
			for(int x = 0; x < image.GetWidth(); x++){
			image.Lock();
				if(image.GetPixel(x,y) == origin){
					image.SetPixel(x,y,new_color);
				}
				else if(image.GetPixel(x,y) == new_color){
					image.SetPixel(x,y,second_color);
				}	
			}
		image.Unlock();
		ImageTexture new_texture = new ImageTexture();
  		new_texture.CreateFromImage(image, 0);
		sprite.Texture = new_texture;
		}
	}
	public void ChangeHair(Sprite sprite)
	{
		sprite.Texture = (Texture)GD.Load("res://assets/character/female/human/hair/hair2.png");
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}







