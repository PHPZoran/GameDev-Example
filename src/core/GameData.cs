
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MonoCustomResourceRegistry;
using Godot;

[RegisteredType(nameof(GameData))]
public class GameData : Resource
{ 
	[Export]
	private string _savename;
	[Export]
	public string main;
	[Export]
	private string ally;
	[Export]
	private Origin origin;
	[Export]
	public string lastText;
	[Export]
	public bool merciless;
	[Export]
	private int gold;
	[Export]
	private Godot.Collections.Dictionary<string, int> variable;
	[Export]
	private string activequests;
	[Export]
	private string completedquests;
	[Export]
	private string inventory;
	public GameData()
	{
		//player = new Player();	
	}
	public GameData(Player player){
		_savename = Regex.Replace((player.main.FirstName + " " + player.main.LastName), @"[\/?:*""><|]+", "", RegexOptions.Compiled);
	}
	public GameData(string name)
	{
		_savename = name;
	}
	public void SaveInfo(Player player){
		Dictionary<string,string> data = new Dictionary<string,string>()
		{
			{"characters",player.main.FirstName + " " + player.main.LastName + " & " + player.companion.FirstName + " " + player.companion.LastName},
			{"time","Morning, Day 1 Month 1, Year 1294"},
			{"location","Hart Estate"},
		};
		var file = new Godot.File();
		string save = JsonConvert.SerializeObject(data);
		file.Open("user://Saves/" + _savename + "/SaveInfo.sav", Godot.File.ModeFlags.Write);
		file.StoreLine(save);
		file.Close(); 	
	}
	public void SaveGame(Player player)
	{
		GD.Print(player.LastText);
		GD.Print("Last saved player txt above this");
		this.main = JsonConvert.SerializeObject(player.main);
		this.ally = JsonConvert.SerializeObject(player.companion);
		this.origin = player.origin;
		lastText = player.LastText;
		GD.Print(lastText);
		GD.Print("Last text displayed above this");
		this.merciless = player.merciless;
		this.gold = player.Gold;
		this.variable = player.variable;
		this.activequests = JsonConvert.SerializeObject(player.ActiveQuests);
		this.completedquests = JsonConvert.SerializeObject(player.completedquests);
		this.inventory = JsonConvert.SerializeObject(player.inventory);
		ResourceSaver.Save("user://Saves/" + _savename + "/GameData.tres", this);	
			TakeOverPath("user://Saves/" + _savename + "/GameData.tres");
	} 
	
	public void LoadGame(Player player)
	{	
		GD.Print("About to display player last text");
		GD.Print(player.lastText);
		GD.Print("We displayed player text in LoadGame");
		GD.Print(lastText);
		GD.Print("This is the last text saved");
		player.main = JsonConvert.DeserializeObject<PC>(main);
		player.companion = JsonConvert.DeserializeObject<PC>(ally);
		player.origin = origin;
		player.lastText = lastText;
		player.merciless = merciless;
		player.Gold = gold;
		player.variable = variable;
		player.inventory = JsonConvert.DeserializeObject<Item[]>(inventory);
		player.ActiveQuests = JsonConvert.DeserializeObject<Dictionary<string,Quest>>(activequests);
		player.completedquests = JsonConvert.DeserializeObject<Dictionary<string,Quest>>(completedquests);
	}
	public void PrepareDirectory()
	{
		var directory = new Godot.Directory();
			directory.Open("user://");
			if(!directory.DirExists("user://Saves")){
				directory.MakeDir("Saves");
			}	
			if(!directory.DirExists("user://Saves/" + _savename))
			{
				directory.MakeDir("user://Saves/" + _savename);
			}
	}
	public Dictionary <string,string> LoadInfo(string folder)
	{
		_savename = folder;
		var file = new Godot.File();
		file.Open("user://Saves/" + _savename + "/SaveInfo.sav", Godot.File.ModeFlags.Read);
		string text = file.GetAsText();
		Dictionary <string,string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
		return dict;
	}
	
	public string SaveName{
		get{return _savename;}
		set{_savename = value;}
	}
} 

