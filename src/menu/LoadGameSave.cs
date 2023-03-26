using Godot;
using System;
using System.Collections.Generic;

public class LoadGameSave : Control
{
	string foldername;
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public void PrepareSlot(Dictionary<string,string> data,string folder)
	{
		foldername = folder;
		string characters = data["characters"];
		if(characters.Length > 36){ 
			GD.Print("Hi");
			characters =  characters.Substring(0, 33);
			characters += "...";
			GD.Print(characters);
		}
		GetNode<Label>("LabelSave").Text = folder;
		GetNode<Label>("LabelCharacters").Text = characters;
		GetNode<Label>("LabelLocation").Text = data["location"];
		GetNode<Label>("LabelTime").Text = data["time"];
	}
	private void OnButtonLoadPressed()
	{
		var global = (Global)GetNode("/root/Global");
		global.LoadFile = foldername;
		global.GotoScene("res://src/menu/LoadScreen.tscn");
	}
	private void OnButtonDeletePressed()
	{
		var deletePop = GetNode<ConfirmationDialog>("ContainerButton/ButtonDelete/DialogDelete");
		deletePop.RectPosition = GetViewport().GetMousePosition();
		deletePop.Popup_();
	}
	private void OnDialogDeleteConfirm()
	{
		if(foldername != "")
		{
			var directory = new Directory();
			directory.Remove("user://Saves/" + foldername + "/GameData.res");
			directory.Remove("user://Saves/" + foldername + "/SaveInfo.sav");
			directory.Remove("user://Saves/" + foldername);	
		}
		QueueFree();
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}









