using Godot;
using System;

public class CharacterHealth : VBoxContainer
{
	[Signal]
delegate void TargetSelected(int target);
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	this.Connect(nameof(TargetSelected), GetNode("/root/Battle"), "ActionSingleTarget");
		
	}
	public void SetupCharacter(Character entity){
		
		var name = GetNode<RichTextLabel>("CharacterName");
		name.Text = entity.FullName;
		var healthlabel = GetNode<Label>("CharacterHealth/HealthValue");
		var healthbar = GetNode<ProgressBar>("CharacterHealth");
		healthbar.MaxValue = entity.MaxHealth;
		healthbar.Value = entity.CurrentHealth;
		healthlabel.Text = entity.CurrentHealth.ToString() + "/" + entity.MaxHealth.ToString();	
	}
	

private void _on_CharacterHealth_gui_input(object @event)
{
	if(Input.IsActionPressed("default_action") && Global.combatstatus == Global.CombatStatus.SingleAttack){
		Global.combatstatus = Global.CombatStatus.None;
		Input.SetCustomMouseCursor(null);
		
		EmitSignal("TargetSelected", GetIndex());
		}
}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}







