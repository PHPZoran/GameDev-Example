using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Generator : Node
{
	private bool first_name_pass = false;
	private int points = 25;
	private Player player;
	private PC ally;
	private PC main;
	private bool initialize_attributes = false;
	private Dictionary<string,int> races = new Dictionary<string,int>(){
		{"Archfey",0},{"Elf",1},{"Human",2},{"Katari",3}
	};
 
Color origin = new Color("#eec39a");

Color new_color = new Color("#800080");


	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		//Default Values
		ally = player.companion;
		main = player.main;
		player.main.Sex = Sex.Male;
		ally.Sex = Sex.Female;
		player.StartPoints = 25;
		//Startup and pop up origin panel.
	
		GetNode<PopupPanel>("OriginPanel").Popup_();
	
	} 
	
	private void OnReselectOriginCall()
	{
		GetNode<Button>("BackMain").Visible = false;
		GetNode<PopupPanel>("OriginPanel").Popup_();
	}

	public int GetPoints(){
		return points;
	}
	public void DecrementPoints(){
		points -= 1;
	}
	public void IncrementPoints(){
		points += 1;
	}
	public void ResetPoints(){
		points = 50;
	}
public void OriginFinished(){
	string pClassName = "Archer";
	string cClassName = "Soldier";
	GD.Print(player.main.Race.Name);
	var playerRace = GetNode<OptionButton>("CharacterCreator/VBoxContainer/CharacterRace/PlayerRace");
	playerRace.Selected = races[player.main.Race.Name];
	var companionRace = GetNode<OptionButton>("CharacterCreator/VBoxContainer/CharacterRace/CompanionRace");
	companionRace.Selected = races[player.companion.Race.Name];
	var playerSurname = GetNode<TextEdit>("CharacterCreator/VBoxContainer/CharacterLastName/PlayerLast");
	playerSurname.Text = main.LastName;
	var companionSurname = GetNode<TextEdit>("CharacterCreator/VBoxContainer/CharacterLastName/CompanionLast");
	companionSurname.Text = ally.LastName;
	if(player.origin == Origin.FourKingdoms || player.origin == Origin.Winter){
		playerSurname.Readonly = false;
		companionSurname.Readonly = false;
	}
	if(player.origin == Origin.Summer){
		companionSurname.Readonly = true;
		playerSurname.Readonly = true;
		pClassName = "Soldier";
		cClassName = "Apprentice";
	}
	else if(player.origin == Origin.Spring){
		companionSurname.Readonly = false;
		playerSurname.Readonly = true;
		pClassName = "Apprentice";
		cClassName = "Warrior Priest";
	}
	else if(player.origin == Origin.Autumn)
	{
		playerSurname.Readonly = false;
		companionSurname.Readonly = false;
		pClassName = "Thief";
	}
	SetInitialValues(main,pClassName);
	SetInitialValues(ally,cClassName);
	GetNode<ClassSkillsStatsPanel>("AttributesPanel/PanelContainer/PlayerAttributes").SetInitialStatValues();
	GetNode<ClassSkillsStatsPanel>("AttributesPanel/PanelContainer/CompanionAttributes").SetInitialStatValues();
	initialize_attributes = true;	
	GetNode<Button>("BackMain").Visible = true;
	
}
private void _on_BackMainMenu_pressed()
{		
	

}

private void _on_First_Name_Box_text_changed()
{
	var input = GetNode<TextEdit>("PlayerColor/VBoxContainer/First Name Box");
	if(input.Text.Length > 0){
		first_name_pass = true;
		check_requirements();
	}
	
}
private bool check_player(){
		if(first_name_pass == true){
			var player_check = GetNode<CheckBox>("PlayerColor/VBoxContainer/PlayerCheck");
			player_check.SetPressedNoSignal(true);
		return true;	
	}
	return false;
}
private void check_requirements(){

	//Check player is true OR (Lone wolf is on or companion check is true.)
	if(check_player() == true){
		var start = GetNode<Button>("PlayerColor/VBoxContainer/Start Button");
		start.Disabled = false;
	}
}

private void _on_Start_Button_pressed()
{
	var confirm = GetNode<ConfirmationDialog>("PlayerColor/VBoxContainer/Start Button/ConfirmationDialog");
	confirm.Popup_();

}
private void _on_AppearanceView_pressed()
{
	var view = GetNode<PopupDialog>("Appearance");
	view.Popup_();
}

private void _on_Next_pressed()
{
	Image image = GetNode<Sprite>("Appearance/Detail").Texture.GetData();
	
		for(int y = 0; y < image.GetHeight(); y++){
		for(int x = 0; x < image.GetWidth(); x++){
			image.Lock();
		if(image.GetPixel(x,y) == origin){
		image.SetPixel(x,y,new_color);
		}		
		
	}
	image.Unlock();
	ImageTexture new_texture = new ImageTexture();
  new_texture.CreateFromImage(image, 0);
 GetNode<Sprite>("Appearance/Detail").Texture = new_texture;

	}
}

private void OnCallForPopUp()
{
	var pattributes = GetNode<ClassSkillsStatsPanel>("AttributesPanel/PanelContainer/PlayerAttributes");
	var cattributes = GetNode<ClassSkillsStatsPanel>("AttributesPanel/PanelContainer/CompanionAttributes");
	if(initialize_attributes){
		pattributes.SetInitialStatValues();
		cattributes.SetInitialStatValues();
		initialize_attributes = false;
	}
	cattributes.SetNames();
	pattributes.SetNames();
	GetNode<Label>("AttributesPanel/PanelContainer/DescriptionLabel").Text = "DESCRIPTIONDEFAULT";
	GetNode<PopupPanel>("%AttributesPanel").Popup_();
}


private void OnAttributePanelProceed()
{
	GetNode<PopupPanel>("AttributesPanel").Visible = false;
} 
private void SetInitialValues(PC character,string classname)
{
	character.Might = character.Race.GetMinValue("Might");
	character.Agility = character.Race.GetMinValue("Agility");
	character.Vitality = character.Race.GetMinValue("Vitality");
	character.Magic = character.Race.GetMinValue("Magic");
	character.Willpower = character.Race.GetMinValue("Willpower");
	character.Trickery = character.Race.GetMinValue("Trickery");
	Profession profession = new Profession();
	if(character.Profession.ClassName == null){
		character.Profession = profession.GetClass(classname);
	} 
	character.Skillset.Clear();
	character.Skillset.Add(character.Profession.Skillset.ElementAt(0).Key,character.Profession.Skillset.ElementAt(0).Value);
	character.Skillset.Add(character.Profession.Skillset.ElementAt(1).Key,character.Profession.Skillset.ElementAt(1).Value);
	character.Skillset.Add(character.Profession.Skillset.ElementAt(2).Key,character.Profession.Skillset.ElementAt(2).Value);
	character.InitializeHP();
	character.InitializeEnergy();
} 

private void OnMainReturnSelect()
{
	var global = (Global)GetNode("/root/Global");
	global.GotoScene("res://src/menu/Main.tscn");
}

private void OnCreatorAppearancePopupCall()
{
	GetNode<PopupPanel>("AppearancePanel").Popup_();
}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



















