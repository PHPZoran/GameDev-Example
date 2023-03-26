using Godot;
using System;

public class OriginPanel : PopupPanel
{
	private Player player;
	
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
	}


	private void OnOriginSubmitPressed()
	{
		this.Visible = false;
		var generator = (Generator)GetParent();
		generator.OriginFinished();
	}
	
private void OriginButton_SignalOriginPressed(string name)
	{
		GetNode<Label>("VBoxContainer/OriginText").Text = Tr("Origin" + name + "Description");
		GetNode<Label>("VBoxContainer/OriginRace").Text = Tr("Origin" + name + "Races");
		GetNode<Button>("VBoxContainer/OriginSubmit").Disabled = false;
		Enum.TryParse(name, out player.origin);
		if(player.origin != Origin.FourKingdoms){
			SetRace();
		} 
		player.lastText = "START" + name.ToUpper();
	}

private void SetRace(){

	switch(player.origin){
		case Origin.Spring: 
			player.main.Race = player.main.Race.SetRace("Archfey");
			player.companion.Race = player.companion.Race.SetRace("Elf");
			player.main.LastName = "Tyne";
			player.companion.LastName = "";
			break;
		case Origin.Summer: 
			player.main.Race = player.main.Race.SetRace("Human");
			player.companion.Race = player.companion.Race.SetRace("Archfey");
			player.main.LastName = "Hart";
			player.companion.LastName = "Tyne";
			break;
		case Origin.Autumn: 
			player.main.Race = player.main.Race.SetRace("Katari");
			player.companion.Race = player.companion.Race.SetRace("Human");
			player.main.LastName = "Eadric";
			player.companion.LastName = "";
			break;
		case Origin.Winter: 
			player.main.Race = player.main.Race.SetRace("Elf");	
			player.companion.Race = player.companion.Race.SetRace("Katari");
			player.main.LastName = "";	
			player.companion.LastName = "";
			break;	
		default:
			player.main.LastName = "Hart";
			player.companion.LastName = "";
			break;
	}
	GD.Print(player.main.Race.Name);
}

}













