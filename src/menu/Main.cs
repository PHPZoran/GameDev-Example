using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class Main : Node
{
Player player;
enum Language { english, spanish, italian };
public Dictionary<string, string> descriptions = new Dictionary<string, string>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
	}
private void OnNewGame()
{
	GetNode<PopupDialog>("NewGameMenu").Popup_();
	GetNode<Label>("NewGameMenu/NewGameDescription").Text = Tr("DESCRIPTIONNEWGAME");

}

private void OnNewGameClose()
{
	var newGame = GetNode<PopupDialog>("NewGameMenu");
	newGame.Visible = false;
}
  
private void OnStoryModeSelected()
{
	player.Difficulty = Difficulty.Story;
	GetNode<Label>("NewGameMenu/NewGameDescription").Text = Tr("DESCRIPTIONSTORYMODE");
}
private void OnStrategistPressed()
{
	player.Difficulty = Difficulty.Strategist;
	GetNode<Label>("NewGameMenu/NewGameDescription").Text = Tr("DESCRIPTIONSTRATEGISTMODE");
}
private void OnStandardSelected()
{
	player.Difficulty = Difficulty.Standard;
	GetNode<Label>("NewGameMenu/NewGameDescription").Text = Tr("DESCRIPTIONSTANDARDMODE");
}

private void OnNewGameStart()
{
	
		var global = (Global)GetNode("/root/Global");
		
	global.GotoScene("res://src/character/Generator.tscn");
}

private void OnOptionsClick()
{
	var options = GetNode<PopupDialog>("OptionsMenu");
	options.Popup_();

}
private void OnExit()
{
	GetTree().Notification(MainLoop.NotificationWmQuitRequest);
}
	
	private void OnLoadPressed()
	{
		GetNode<PopupDialog>("LoadGameDialog").Popup_();
	}
	

	async private void OnSavePressed()
	{
		//Saving is going to be near instant, but to reassure the player, let's make a timer.
		GetNode<Timer>("SaveTimer").Start();
		Player player = (Player)GetNode("/root/Player");
		GetNode<PopupDialog>("SavingMessage").Popup_();
		string foldername = Regex.Replace((player.main.FirstName + " " + player.main.LastName), @"[\/?:*""><|]+", "", RegexOptions.Compiled);
		GameData data = new GameData();
		data = (GameData)GD.Load("res://src/core/GameData.res");
		data.SaveName = foldername;	
		data.PrepareDirectory();
		data.SaveInfo(player);
		data.SaveGame(player);
		await ToSignal(GetNode<Timer>("SaveTimer"), "timeout");
		GetNode<PopupDialog>("SavingMessage").Visible = false;
		
	}
	private void OnMenuPressed()
	{
		GetNode<Godot.ConfirmationDialog>("ConfirmationMenu").Popup_();

	}
	private void OnExitPressed()
	{
		GetNode<Godot.ConfirmationDialog>("ConfirmationExit").Popup_();
	}
	
	private void OnConfirmationMenuConfirmed()
	{
		var global = (Global)GetNode("/root/Global");
		global.GotoScene("res://src/menu/Main.tscn");
	}

}
































