using Godot;
using System;
using System.Collections.Generic;

public class Options : PopupDialog
{
	Global settings;
	Dictionary<int,string> language = new Dictionary<int,string>(){
		{0,"en"},
		{1,"es"},
		{2,"ja"}
	};
	Dictionary<string,int> rlanguage = new Dictionary<string,int>(){
		{"en",0},
		{"es",1},
		{"ja",2}
	};
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		settings = (Global)GetNode("/root/Global");
		var config = new ConfigFile();
		

	// Load data from a file.
		Error err = config.Load("user://settings.cfg");

	// If the file didn't load, ignore it.
		if (err != Error.Ok)
		{
			return;
		}	
		// Iterate over all sections.
		foreach (String setting in config.GetSections())
		{
	// Fetch the data for each section.
			Text.auto = (bool)config.GetValue(setting, "autocontinue");
			Text.instant = (bool)config.GetValue(setting, "instanttext");
			settings.NSFW = (bool)config.GetValue(setting, "nsfw");
			TranslationServer.SetLocale((string)config.GetValue(setting, "language"));
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), (float)config.GetValue(setting, "music"));
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), (float)config.GetValue(setting, "sfx"));
			GetNode<CheckButton>("ButtonAutoContinue").Pressed = Text.auto;
			GetNode<CheckButton>("ButtonInstantText").Pressed = Text.instant;
			GetNode<CheckButton>("ButtonAdultContent").Pressed = settings.NSFW;
			GetNode<OptionButton>("SelectLanguage").Selected = rlanguage[(string)config.GetValue(setting, "language")];
			GetNode<HSlider>("MusicSlider").Value = (float)config.GetValue(setting, "music");
			GetNode<HSlider>("SFXSlider").Value = (float)config.GetValue(setting, "sfx");
		}
		
	}
	

	private void OnAutoToggle(bool button_pressed)
	{
		Text.auto = button_pressed;
	}
	private void OnInstantToggle(bool button_pressed)
	{
		Text.instant = button_pressed;
	}
	private void OnAdultToggle(bool button_pressed)
	{
		settings.NSFW = button_pressed;
	}
	private void OnNewLanguageSelected(int index)
	{
		TranslationServer.SetLocale(language[index]);
		GD.Print(TranslationServer.GetLocale());
	}
	private void OnMusicSliderChange(float value)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), value);
	}
	
	private void OnSFXSliderChange(float value)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), value);
	}
	private void OnDone()
	{
		var config = new ConfigFile();
		config.SetValue("Setting", "language", language[GetNode<OptionButton>("SelectLanguage").Selected]);
		config.SetValue("Setting", "music", GetNode<HSlider>("MusicSlider").Value);
		config.SetValue("Setting", "sfx", GetNode<HSlider>("SFXSlider").Value);
		config.SetValue("Setting", "nsfw", GetNode<CheckButton>("ButtonAdultContent").Pressed);
		config.SetValue("Setting", "autocontinue", GetNode<CheckButton>("ButtonAutoContinue").Pressed);
		config.SetValue("Setting", "instanttext", GetNode<CheckButton>("ButtonInstantText").Pressed);
		config.Save("user://settings.cfg");
		Visible = false;
	}

}














