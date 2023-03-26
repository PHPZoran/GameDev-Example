using Godot;
using System;

public class LoadScreen : Node2D
{
	private string[] kingdoms = new string[4] {"SPRING","SUMMER","AUTUMN","WINTER"};
	public RandomNumberGenerator random = new RandomNumberGenerator();
   //LoadScreen:
	//Display random message, load next screen when timer finishes. load as much as you can while timer is running.
	
	public override void _Ready()
	{
		random.Randomize();
		GenerateRandomMessage();
		GetNode<Timer>("LoadTimer").Start();
		LoadData();
	}
	public void GenerateRandomMessage()
	{
		int kingdomnum = random.RandiRange(0, 3);
		string kingdom = kingdoms[kingdomnum];
		GetNode<AnimatedSprite>("AnimatedLoadImage").Animation = kingdom;
		int number = random.RandiRange(1,2);
		GetNode<Label>("Message").Text = Tr("LOADMESSAGE" + kingdom + number);
	}
	public async void LoadData()
	{
			var global = (Global)GetNode("/root/Global");
		if(ResourceLoader.Exists("user://Saves/" + global.LoadFile + "/GameData.tres")){
			Player player = (Player)GetNode("/root/Player");
			GameData data = new GameData();
			data = (GameData)ResourceLoader.Load("user://Saves/" + global.LoadFile + "/GameData.tres","",true); 
			data.LoadGame(player);
			await ToSignal(GetNode<Timer>("LoadTimer"), "timeout");
			global.LoadFile = "";
			global.GotoScene("res://src/core/UI.tscn");
		}
		else
		{
			//Some error, recover and return to main.
			await ToSignal(GetNode<Timer>("LoadTimer"), "timeout");
			global.GotoScene("res:src/menu//Main.tscn");
		}
	}
}



