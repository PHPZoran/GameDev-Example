using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CreatorPanel : Control
{
	private Player player;
	private Character character;
	private string foldername;
	[Signal]
	delegate void CallForPopUp();
	[Signal]
	delegate void ReselectOriginCall();
	[Signal]
	delegate void AppearancePopupCall();
	
	
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
	}

	private void OnClassPressed()
	{
		try
		{
			EmitSignal("CallForPopUp");
		}
		catch
		{
			GD.Print("Node not found");
		}
	}
	

	private void OnReselectOrigin()
	{
		try
		{
			EmitSignal("ReselectOriginCall");
		}
		catch
		{
			GD.Print("Node not found");
		}
	}

private void OnNameTextChanged(string source)
{
	if(source.Contains("Player")){
		character = player.main;
	}
	else{
		character = player.companion;
	}
	if(source.Contains("First"))
	{
		character.FirstName = GetNode<TextEdit>("VBoxContainer/CharacterFirstName/" + source).Text.Replace("\r", string.Empty).Replace("\n", string.Empty);
	}
	else
	{ 
		character.LastName = GetNode<TextEdit>("VBoxContainer/CharacterLastName/" + source).Text.Replace("\r", string.Empty).Replace("\n", string.Empty);
	}	
}


	private void OnAppearanceSelect()
	{
		EmitSignal("AppearancePopupCall");
	}
	private void OnGameStart()
	{
		CheckOrDefaultNames();
		GetNode<ConfirmationDialog>("ConfirmationDialog").Popup_();
	}
	private void OnConfirm()
	{
		foldername = Regex.Replace((player.main.FirstName + " " + player.main.LastName), @"[\/?:*""><|]+", "", RegexOptions.Compiled);
		if(ResourceLoader.Exists("user://Saves/" + foldername + "/GameData.tres")){
			GetNode<ConfirmationDialog>("OverwriteDialog").RectPosition = GetViewport().GetMousePosition();
			GetNode<ConfirmationDialog>("OverwriteDialog").Popup_();		
		}
		else{
			AutoSave();
		}
	}
	public void AutoSave()
	{
		GameData data;
		data = (GameData)GD.Load("res://src/core/GameData.tres");	
		data.SaveName = foldername;	
		data.PrepareDirectory();
		data.SaveInfo(player);
		data.SaveGame(player);
		CheckAndSplitPoints();
		StartGame();
	}
	private void OnOverwriteDialogConfirmed()
	{
		AutoSave();
	}

	private void CheckOrDefaultNames()
	{
		if(player.main.FirstName == "")
		{
			player.main.FirstName = "Corrin";
			GetNode<TextEdit>("VBoxContainer/CharacterFirstName/PlayerFirst").Text = player.main.FirstName;
		}
		if(player.main.LastName == "")
		{
			player.main.LastName = "Snowstep";
			GetNode<TextEdit>("VBoxContainer/CharacterLastName/PlayerLast").Text = player.main.LastName;
		}
		if(player.companion.FirstName == "")
		{
			player.companion.FirstName = "Kai";
			GetNode<TextEdit>("VBoxContainer/CharacterFirstName/CompanionFirst").Text = player.companion.FirstName;
		}
		if(player.companion.LastName == "")
		{
			player.companion.LastName = "Greenshield";
			GetNode<TextEdit>("VBoxContainer/CharacterLastName/CompanionLast").Text = player.companion.LastName;
		}
	} 
	private void CheckAndSplitPoints()
	{
		if(player.StartPoints > 0)
		{
			player.companion.StatPoints = player.StartPoints/2;
			player.main.StatPoints = player.StartPoints/2 + player.StartPoints % 2;
		}
	}
	private void StartGame()
	{
		var global = (Global)GetNode("/root/Global");
		global.GotoScene("res://src/core/UI.tscn");
	}
}











