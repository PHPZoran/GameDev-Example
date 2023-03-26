using Godot;
using System;
using System.Collections.Generic;

public class LoadGame : PopupDialog
{
	GameData data = new GameData();
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var directory = new Directory();
		var scene = GD.Load<PackedScene>("res://src/menu/LoadGameSave.tscn");
			directory.Open("user://");
			if(!directory.DirExists("user://Saves")){
				directory.MakeDir("Saves");
			}	
		directory.Open("user://Saves");	
		directory.ListDirBegin(true);
		string folder;
		do
		{
			folder = directory.GetNext();
			if(folder != "user://Saves" && folder != "")
				{
					
					File info = new File();
					if(ResourceLoader.Exists("user://Saves/" + folder + "/GameData.res") && info.FileExists("user://Saves/" + folder + "/SaveInfo.sav")){
						Dictionary<string,string> textinfo = data.LoadInfo(folder);
						var instance = scene.Instance<LoadGameSave>();
						GetNode("ScrollContainer/LoadGameContainer").AddChild(instance);	
						instance.PrepareSlot(textinfo,folder);
					}
				}
		}
		while(folder != "");
	}
	private void OnClosePressed()
	{
		Visible = false;
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



