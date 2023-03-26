using Godot;
using System;
using System.Collections.Generic;
public class PlayerTeam : VBoxContainer
{
	// ProgressBar _companion;
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Test Function
		InitializeTeam();
		ReadyPlayer1();
	//	var scene = GD.Load<PackedScene>("res://CharacterHealth.tscn");
	//	var instance = scene.Instance<CharacterHealth>();
	//	AddChild(instance);	
		//		var instance2 = scene.Instance<CharacterHealth>();
	//	AddChild(instance2);	
	//	instance.LabelTest("Companion");
	}
	private void ReadyPlayer1(){
		var scene = GD.Load<PackedScene>("res://src/battle/CharacterHealth.tscn");
		var player = (Player)GetNode("/root/Player");
		foreach(var i in player.party){
			var instance = scene.Instance<CharacterHealth>();
			AddChild(instance);	
			instance.SetupCharacter(i);
		}
		//var namelabel  = GetNode<RichTextLabel>("CharacterContainer/CharacterName");
		//var healthbar = GetNode<ProgressBar>("CharacterContainer/PlayerHealthBar");
		//var healthlabel = GetNode<Label>("PlayerHealthBar/PlayerHealthNum");
		//var player = (Player)GetNode("/root/Player");
	
		//player.SetFullName("John","Snow");
		//healthbar.MaxValue = player.MaxHealth;
		//healthbar.Value = player.CurrentHealth;
		//namelabel.Text = player.GetFullName();
		//healthlabel.Text = player.MaxHealth.ToString() + " Health";
	}
	private void InitializeTeam(){
			var player = (Player)GetNode("/root/Player");
		
		player.party = new List<Character>();
		player.party.Add(player.main);
		player.party.Add(player.companion);
		player.battleteam = player.party;
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
