using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

public class GameScreen : Control
{
	private Dialogue dialogue = new Dialogue();
	private Player player;
	private Condition condition = new Condition();
	private string currentText;
	private DialogueLine line = new DialogueLine();
	private Button continueButton;
	private Godot.Collections.Dictionary<int, DialogueLine> choices = new Godot.Collections.Dictionary<int,DialogueLine>();
	private VBoxContainer container;
	private RichTextLabel textBox;
	private string branch;
	private bool menu = false;
	private Godot.Collections.Dictionary<string, int> inputs = new Godot.Collections.Dictionary<string, int>(){
		{"I",0},
		{"Q",2},//Slot after Abilities
		{"S",1},
		{"Escape",4},//Options Menu. Test to see if it is ESC or Escape
		{"K",2},//Skills, after Stats
		{"A",3},//One after skills
		{"C",3}//One before Options
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		var global = (Global)GetNode("/root/Global");
		if(ResourceLoader.Exists("user://Saves/" + global.LoadFile + "/GameData.res")){
			GameData data;
			data = (GameData)GD.Load("user://Saves/" + global.LoadFile + "/GameData.res"); 
			data.LoadGame(player);
		}
		textBox = GetNode<RichTextLabel>("%Dialogue");
		continueButton = GetNode<Button>("%ContinueButton");
		container = GetNode<VBoxContainer>("%ResponseContainer");
		Condition condition = new Condition();
		if(player.lastText == null){
			GD.Print("Oops");
			player.lastText = "STARTLOCATION1";
			currentText = Tr("STARTLOCATION1");
		}
		else{
			currentText = Tr(player.lastText);
		}
		line = dialogue.GetLine(player.lastText);
		if(line.GetAction() != null){
			//handle action now.
			HandleAction();
		}
		
		ProcessText(currentText);
	}
	private void OnTextFinished()
	{
		GD.Print("Greetings!");	
		if(continueButton.Text == Tr("LABELCONTINUE")){
			_on_ContinueButton_pressed();	
			GD.Print("Hi");	
		}
	}
	private void ProcessText(string text){
		continueButton.Show();
		//textBox.VisibleCharacters = 0;
		if(text.Contains("<")){
			text = ParseTextToken(text);
		}
		if(line.state == State.battle){
			continueButton.Text = "LABELFIGHT!";
			
		}
		else if(line.state != State.ongoing && line.GoTo() == null){
			PrepareChoices();
			continueButton.Hide();
		}
		else{
			continueButton.Text = "LABELCONTINUE";
			Text.signalready = false;
			if(line.Keys() == null){
			//Currently not defined, probably removable.	
			}
			else{
			//Currently not defined, might be removable.
			}
		}
		textBox.Newline();
		textBox.Text += text;
		textBox.Newline();
	}
	private void PrepareChoices(){
		//Clear the dictionary in case it has values.
		choices.Clear();
		//if(line.state == State.end){
		//	HandleLocations();
	//	}
		
		var scene = GD.Load<PackedScene>("res://src/dialogue/Reply.tscn");
		if(line.state == State.end || line.state == State.location){
			container = GetNode<VBoxContainer>("LocationList");
			continueButton.Hide();
			}
		else{
			container = GetNode<VBoxContainer>("%ResponseContainer");
			continueButton.Show();
		}
		int counter = 0;
		try{
		foreach(var reply in line.Keys()){
			if(HandleCondition(reply)){
				var instance = scene.Instance<Reply>();
				container.AddChild(instance);	
				instance.SetupText(reply,line);
				choices.Add(counter,dialogue.GetLine(reply));
				counter++;	
				
			}
		}
		}
		catch{
			GD.Print("No choices for chosen state. Fix this.");
		}
		if(choices.Count == 0){
			GD.Print("No valid choices. Fix this.");
		}
			
	}

public void QuestNotification()
{
	GetNode<VBoxContainer>("QuestNotify").Visible = true;
	 var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	animationPlayer.Play("QuestNotify");
}

	private void HandleAction(){
		foreach(Action action in line.GetAction()){
			string methodName = action.Method;
	   		object[] methodarray = action.Properties;
			methodarray[0] = player;
		   	Type type = typeof(Action);
			MethodInfo method = type.GetMethod(methodName);
			Action a = GetNode<Action>("Action");
			try{
	   			method.Invoke(a,methodarray);	
			}
			catch{
				GD.Print("Not enough parameters in method call.");
			}
		}
	}
	private bool HandleCondition(String reply){
		var response = dialogue.GetLine(reply);
		
		if(response.GetCondition() == null){
			return true;	
		}
		else{ 
			foreach(Condition condition in response.GetCondition()){
				string methodName = condition.Method;
	   	 		object[] methodarray = condition.Properties;
				methodarray[0] = player;
	   			Type type = typeof(Condition);
				MethodInfo method = type.GetMethod(methodName);
			//Condition c = new Condition();
				try{
	   				if(!(bool)method.Invoke(condition,methodarray)){
						return false;
					}
				}
				catch{
					GD.Print("Not enough parameters in method call.");
					return false;
				}
			}
			return true;
		}
	}
	private string ParseTextToken(string text){
		if(text.Contains("<PRO_NAME>")){
			text = text.Replace("<PRO_NAME>",player.main.FullName);
		}
		if(text.Contains("<PRO_FIRST>")){
			text = text.Replace("<PRO_FIRST>",player.main.FirstName);
		}
		if(text.Contains("<PARENT>")){
			text = text.Replace("<PARENT>",player.RetrieveParent());
		}
		return text;
	}


private void _on_ContinueButton_pressed()
{		
	//Skips any remaining text.
		textBox.VisibleCharacters = textBox.GetTotalCharacterCount();
	//if(continueButton.Text == "End"){
	if(line.state != State.battle){
				HandleNextLine();
	}
	else{
		player.lastText = branch;
		var global = (Global)GetNode("/root/Global");
		global.GotoScene("res://src/battle/Battle.tscn");
		}
	
}
	public void HandleReply(int selected){
			line = choices[selected];
			//Clear the children of ResponseContainer 
			foreach(Button child in container.GetChildren()){
			child.QueueFree();
			}
			//Actions should be checked here.
			if(line.GetAction() != null){
			//handle action now.
				HandleAction();
			}
			if(line.state == State.ongoing){
				HandleNextLine();
			}
			//We have a split state!
			
			//Consider complexity. If we want multiple paths at this point,
			//we could consider responses with GoTo. 
		}	
private void HandleNextLine(){
				
				if(line.GoTo() != null){
					branch = line.GoTo();
					currentText = Tr(line.GoTo());
				}
				else{
					branch = BuildNextLine(line.Keys());
					currentText = Tr(branch);
				}
				try{
					line = dialogue.GetLine(branch);			
					//Do stuff on next line.
					if(line.GetAction() != null){
					//handle action now.
						HandleAction();
					}
					player.lastText = branch;
					ProcessText(currentText);
				}
				catch(KeyNotFoundException){
					GD.Print("Key Not Found");
				}
			
}
private string BuildNextLine(string[] branches){
	try{
	foreach(var reply in line.Keys()){
		if(HandleCondition(reply)){
			return reply;
			}
		}
		}
		catch(KeyNotFoundException){
			GD.Print("Key not found - Condition check block.");
			return null;
		}
		return null;
	}		
					
public override void _Input(InputEvent inputEvent)
{
	if(inputEvent.IsActionReleased("default_action")){
		bool inserted = false;
		int slot = 0;
		if(player.lostItem != null){ 
			//rescue the item if we can.
			while(!inserted && slot < 30){
		if(player.CheckSlotEmpty(slot)){
			player.PlaceItem(player.lostItem,slot);
			inserted = true;
			if(this.HasNode("DataTabs"))
			{ 
				TextureRect node = (TextureRect)GetNode("DataTabs/LABELINVENTORY/Inventory/GridContainer").GetChild(slot).GetChild(0);
				Item item = player.lostItem;
				node.Texture = (Texture)GD.Load(player.lostItem.Visual);
				if(player.lostItem.StackSize > 1 && player.lostItem.Quantity > 1){
					Label quantity = (Label)node.GetChild(0);
					quantity.Text = player.lostItem.Quantity.ToString();
				}
			}
		}
		slot++;
	}
	if(inserted != true){
		GD.Print("No room. Implement Inventory overflow and signal gamescreen later.");
	}
	player.lostItem = null;
}
		
}
	else if(inputEvent is InputEventKey eventKey && eventKey.Pressed && inputs.ContainsKey(inputEvent.AsText())){
		if (menu == false)
		{ 
			var scene = GD.Load<PackedScene>("res://src/character/DataTabs.tscn");
			var instance = scene.Instance<TabContainer>();
			AddChild(instance);	
			instance.CurrentTab = inputs[inputEvent.AsText()];
			menu = true;
		}
		else if(menu == true && GetNode<TabContainer>("DataTabs").CurrentTab == inputs[inputEvent.AsText()]){
			GetNode<TabContainer>("DataTabs").QueueFree();
			menu = false;
		} 
		else{
			GetNode<TabContainer>("DataTabs").CurrentTab = inputs[inputEvent.AsText()];
		}
	}
	else if(!this.HasNode("DataTabs") && inputEvent.IsActionPressed("skip")){
		if(Text.auto == true){
			Text.auto = false;
		} 
		else{
			Text.auto = true;
		}
		var config = new ConfigFile();
		Error err = config.Load("user://settings.cfg");
		config.SetValue("Setting", "autocontinue", Text.auto);
		config.Save("user://settings.cfg");
	}
}
private void OnAnimationPlayerFinish(String anim_name)
{
	if(anim_name == "QuestNotify"){
		GetNode<VBoxContainer>("QuestNotify").Visible = false;
	}
}
}























