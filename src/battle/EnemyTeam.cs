using Godot;
using System;
using System.Collections.Generic;
public class EnemyTeam : VBoxContainer
{
	[Signal]
delegate void DeclareEnemyList(List<Character> enemies);

private Dialogue dialogue = new Dialogue();
	public override void _Ready()
	{
		InitializeTeam();
		EmergeEnemy();
	}
	private void InitializeTeam(){
			var globalData = (Global)GetNode("/root/Global");
			var player = (Player)GetNode("/root/Player");
			var line = dialogue.GetLine(player.lastText);
			Beastiary enemies = new Beastiary();
			globalData.currentEnemies = new List<Character>();
			foreach(var reply in line.Keys()){
				var enemy = enemies.GetCreature(reply);
				globalData.currentEnemies.Add(enemy);
			}
			EmitSignal("DeclareEnemyList", globalData.currentEnemies);
	}
	private void EmergeEnemy(){
		GD.Print("HI");
		var scene = GD.Load<PackedScene>("res://src/battle/CharacterHealth.tscn");
		var enemies = (Global)GetNode("/root/Global");
		GD.Print("Count: " + enemies.currentEnemies.Count);
		foreach(var i in enemies.currentEnemies){
			var instance = scene.Instance<CharacterHealth>();
			AddChild(instance);	
			instance.SetupCharacter(i);
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
