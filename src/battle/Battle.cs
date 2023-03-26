using Godot;
using System;
using System.Collections.Generic;
using System.Linq;



public class Battle : Node
{
public RandomNumberGenerator random = new RandomNumberGenerator();
public List<Character> turnOrder;
public int turn;
private Dialogue dialogue = new Dialogue();
//Note: Will need enemyTeam to send over an unsorted list so we can get the order.
//The first member may not always be the fastest.
public List<Character> playerParty;
public List<Character> enemyParty;

	[Signal]
delegate void EnemyReady();

	public override void _Ready()
	{
		random.Randomize();
		var player = (Player)GetNode("/root/Player");
		playerParty = player.battleteam;
		turn = -1;
		initializeTurnOrder();
		determineTurn();
	}

	public void SetEnemyList(List <Character> enemies){
		var team = (Global)GetNode("/root/Global");
		enemyParty = team.currentEnemies;
		GD.Print("We did this");
	}
	private void initializeTurnOrder(){
		List<Character> playerOrder = getTeamOrder(playerParty); 
		var combat = (Global)GetNode("/root/Global");
		List<Character> enemyOrder = getTeamOrder(enemyParty); 
		GD.Print("Count members");
		GD.Print(enemyOrder.Count);
		if(playerOrder[0].Initiative >= enemyOrder[0].Initiative){
			turnOrder = getCombatOrder(playerOrder,enemyOrder);
		}
		else{
			turnOrder = getCombatOrder(enemyOrder,playerOrder);
		}
		
	}
	async private void determineTurn(){
		if(enemyParty.Count == 0){
			Victory();
		}
		else if(playerParty.Count == 0){
			Defeat();
		}
		else{
			turn++;
			if(turn == turnOrder.Count){
				turn = 0;
				initializeTurnOrder();
			}
			//If combatant is dead or in some state that is unable to fight, skip.
			//Just check for dead for now:
			if(turnOrder[turn].CurrentHealth < 1){
				determineTurn();
			}
			else{
			//Get info of the combatant.
				var display = GetNode<RichTextLabel>("TurnDisplay");
				var combatant = turnOrder[turn];
				display.Text = combatant.FullName + "'s turn.";
				var actionbar = GetNode<HBoxContainer>("ActionBar");
				if(combatant.Faction == "Enemy"){
					actionbar.Visible = false;
					GetNode<Timer>("DelayTimer").Start();
					await ToSignal(GetNode<Timer>("DelayTimer"), "timeout");
					actionEnemyTurn(combatant);
					determineTurn();
			}
			else{
				actionbar.Visible = true;
			}
			}
			}
	}
	async private void Victory(){
		var display = GetNode<RichTextLabel>("TurnDisplay");
		display.Text = "Victory!";
		//For now...
		var player = (Player)GetNode("/root/Player");
		var line = dialogue.GetLine(player.lastText);
		player.lastText = line.GoTo();
		//Set a timer, when it expires, go to the main game ui screen.
		GetNode<Timer>("DelayTimer").Start();
		await ToSignal(GetNode<Timer>("DelayTimer"), "timeout");
		var stage = (Global)GetNode("/root/Global");
		stage.GotoScene("res://src/core/UI.tscn");
	}
	async private void Defeat(){
		var display = GetNode<RichTextLabel>("TurnDisplay");
		display.Text = "Defeat...";
		//For now...
		//Set a timer, when it expires, go to the main menu screen.
		var player = (Player)GetNode("/root/Player");
		var stage = (Global)GetNode("/root/Global");
		GetNode<Timer>("DelayTimer").Start();
		await ToSignal(GetNode<Timer>("DelayTimer"), "timeout");
		if(player.merciless){
			stage.GotoScene("res://src/menu/Main.tscn");
			}
		else{
			player.lastText = enemyParty[0].Location;
			stage.GotoScene("res://src/core/UI.tscn");
		}
	}
	private void dealDamage(int target,int damage,string team,List<Character> selected){
		selected[target].CurrentHealth -= damage;
		
		var healthbar = GetNode(team).GetChild(target).GetNode<ProgressBar>("CharacterHealth");
		var healthlabel = healthbar.GetNode<Label>("HealthValue");
		healthbar.Value -= damage;
		healthlabel.Text = selected[target].CurrentHealth.ToString() + "/" + selected[target].MaxHealth.ToString();
		if(selected[target].CurrentHealth < 1){
			GetNode(team).GetChild(target).QueueFree();
			selected.RemoveAt(target);
		}
	}
	private void actionEnemyTurn(Character combatant){
		
		
		

		int target = random.RandiRange(0, playerParty.Count-1);
	
		//Calculate damage between min and max.
		int damage = random.RandiRange(combatant.DamageMin, combatant.DamageMax);
		dealDamage(target,damage,"PlayerTeam",playerParty);
	}
		public void ActionSingleTarget(int target){
			int damage = random.RandiRange(turnOrder[turn].DamageMin, turnOrder[turn].DamageMax);
			dealDamage(target,damage,"EnemyTeam",enemyParty);
			determineTurn();
		}	
private void _on_TempButton_pressed()
{
	//var arrow = ResourceLoader.Load("res://godot.png");
	//Input.SetCustomMouseCursor(arrow);
	
	//var combat = (Global)GetNode("/root/Global");
	Global.combatstatus = Global.CombatStatus.SingleAttack;
	
}

	private List<Character> getTeamOrder(List<Character> characters){
		IEnumerable<Character> query = characters.OrderByDescending(character => character.Initiative);
		List<Character> orderedTeam = query.ToList();
		return orderedTeam;
	}
	private List<Character> getCombatOrder(List<Character> teamfast,List<Character> teamslow){
		List<Character> combatOrder = new List<Character>();
		int counter = 0;
		int tfSize = teamfast.Count;
		int tsSize = teamslow.Count;
		while(counter < tfSize || counter < tsSize){
			if(counter < tfSize){
				combatOrder.Add(teamfast[counter]);
			}
			if(counter < tsSize){
				combatOrder.Add(teamslow[counter]);
			}
			counter++;
		}
		return combatOrder;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}












