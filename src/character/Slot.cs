using Godot;
using System;

public class Slot : Node
{
	private Item _item;
	public TextureRect holder;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	public Slot(){
		
	}
	public Slot(Item item)
	{
		_item = item;
	}
	public Item Item{
		get{return _item;}
		set{_item = value;}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
