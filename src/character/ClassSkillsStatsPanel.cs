using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public class ClassSkillsStatsPanel : Control
{
	private Player player;
	private PC character;
	private Profession profession = new Profession();
	private Dictionary <string,int> classes = new Dictionary<string,int>(){
		{"Soldier",0},{"Thief",1},{"Warrior Priest",2},{"Archer",3},{"Apprentice",4}
	};
	private Dictionary <int,string> rclasses = new Dictionary<int, string>(){
		{0,"Soldier"},{1,"Thief"},{2,"Warrior Priest"},{3,"Archer"},{4,"Apprentice"}
	};
	
		[Signal]
	delegate void SetPoints();
		[Signal]
	delegate void SendLabelInfo(string info);
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		character = GetCharacter();
		var itemList = GetNode<ItemList>("ClassList");
		itemList.SetItemText(0,Tr("LABELSOLDIER"));
		itemList.SetItemText(1,Tr("LABELTHIEF"));
		itemList.SetItemText(2,Tr("LABELWARRIORPRIEST"));
		itemList.SetItemText(3,Tr("LABELARCHER"));
		itemList.SetItemText(4,Tr("LABELAPPRENTICE"));
		
	}

	private void DecrementAttribute(string name)
	{
		PropertyInfo attribute = typeof(PC).GetProperty(name);
		int value = (int)attribute.GetValue(character, null) - 1;
		attribute.SetValue(character,value);
		GetNode<Label>("AttributeContainer/" + name + "/Value").Text = value.ToString();
		player.StartPoints += 1;
		EmitSignal("SetPoints");
		EvaluateAdjustmentVisiblity();
		if(name == "Vitality")
		{
			ConfigureHealthAndEnergy();
		}
	}
	
	private void IncrementAttribute(string name)
	{
		PropertyInfo attribute = typeof(PC).GetProperty(name);
		int value = (int)attribute.GetValue(character, null) + 1;
		attribute.SetValue(character,value);
		GetNode<Label>("AttributeContainer/" + name + "/Value").Text = value.ToString();
		player.StartPoints -= 1;
		EmitSignal("SetPoints");
		EvaluateAdjustmentVisiblity();
		if(name == "Vitality")
		{
			ConfigureHealthAndEnergy();
		}
	}
	private void EvaluateAdjustmentVisiblity(){
		if(player.StartPoints < 1){
			GetTree().CallGroup("IncrementButtons", "ToggleOff");
		}
		else{
			GetTree().CallGroup("IncrementButtons", "DetermineToggle",this.Name,character);
		} 
		if(player.StartPoints > 24){
			GetTree().CallGroup("DecrementButtons", "ToggleOff");
		} 
		else{
			GetTree().CallGroup("DecrementButtons", "DetermineToggle",this.Name,character);
		}
	}


	public void LabelHovered(string info)
	{
		EmitSignal("SendLabelInfo",info);
	}
	private void OnSkillSelected(int index)
	{
		//Set skill for player in skill slot
		string skillname = character.Profession.Skillset.ElementAt(index+2).Key;
		Skill skill = character.Profession.Skillset[skillname];
		character.Skillset.Remove(character.Skillset.ElementAt(2).Key);
		character.Skillset.Add(skillname,skill);
		//Give Player info on skill
		EmitSignal("SendLabelInfo",skillname);
		
	}
	private PC GetCharacter(){
	if(this.Name.Contains("Player")){
			character = player.main;
		}
		else{
			character = player.companion;
		}
		return character;
	} 
	public void SetInitialStatValues()
	{
		GetNode<Label>("AttributeContainer/Might/Value").Text = character.Race.GetMinValue("Might").ToString();
		GetNode<Label>("AttributeContainer/Agility/Value").Text = character.Race.GetMinValue("Agility").ToString();
		GetNode<Label>("AttributeContainer/Vitality/Value").Text = character.Race.GetMinValue("Vitality").ToString();
		GetNode<Label>("AttributeContainer/Magic/Value").Text = character.Race.GetMinValue("Magic").ToString();
		GetNode<Label>("AttributeContainer/Willpower/Value").Text = character.Race.GetMinValue("Willpower").ToString();
		GetNode<Label>("AttributeContainer/Trickery/Value").Text = character.Race.GetMinValue("Trickery").ToString();
		GetNode<Label>("ClassSkillsContainer/Health/Value").Text = character.MaxHealth.ToString();
		GetNode<Label>("ClassSkillsContainer/Energy/Value").Text = character.MaxEnergy.ToString();
		SetInitialSkillSelection();
	} 
	public void SetInitialSkillSelection()
	{
		GetNode<Label>("ClassSkillsContainer/Skill1").Text = Tr("LABEL" + character.Profession.Skillset.ElementAt(0).Key.ToUpper());
		GetNode<Label>("ClassSkillsContainer/Skill2").Text = Tr("LABEL" + character.Profession.Skillset.ElementAt(1).Key.ToUpper());
		var selections = GetNode<OptionButton>("ClassSkillsContainer/SkillChoices");
		selections.Clear();
		Dictionary<string,Skill> skills = character.Profession.Skillset;
		string skill1 = skills.ElementAt(0).Key;
		string skill2 = skills.ElementAt(1).Key;
		
		foreach (KeyValuePair<string,Skill> skill in skills)
 		{
			if(skill.Key != skill1 & skill.Key != skill2){
 				selections.AddItem(Tr("LABEL" + skill.Key.ToUpper()));
			}
			
 		}
		
	}
	public void SetNames(){
		GetNode<Label>("CharacterLabel").Text = character.FullName + " - " + Tr(character.Race.Name);
		GetNode<ItemList>("ClassList").Select(classes[character.Profession.ClassName],true);
	}
	
	private void OnClassListClassSelected(int index)
	{
		string classname = rclasses[index];
		character.Profession = profession.GetClass(classname);
		ConfigureHealthAndEnergy();
		ConfigureInitialSkills();
		EmitSignal("SendLabelInfo",classname);
	}
	private void ConfigureHealthAndEnergy()
	{
		character.InitializeHP();
		character.InitializeEnergy();
		GetNode<Label>("ClassSkillsContainer/Health/Value").Text = character.MaxHealth.ToString();
		GetNode<Label>("ClassSkillsContainer/Energy/Value").Text = character.MaxEnergy.ToString();
	}
	private void ConfigureInitialSkills()
	{
		character.Skillset.Clear();
		character.Skillset.Add(character.Profession.Skillset.ElementAt(0).Key,character.Profession.Skillset.ElementAt(0).Value);
		character.Skillset.Add(character.Profession.Skillset.ElementAt(1).Key,character.Profession.Skillset.ElementAt(1).Value);
		character.Skillset.Add(character.Profession.Skillset.ElementAt(2).Key,character.Profession.Skillset.ElementAt(2).Value);
		SetInitialSkillSelection();
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }











}


