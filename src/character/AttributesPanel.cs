using Godot;
using System;

public class AttributesPanel : PopupPanel
{
	Player player;
	public override void _Ready()
	{
		 player = GetNode<Player>("/root/Player");
	}

	private void OnSetPoints()
	{
		GetNode<Label>("PanelContainer/PointsContainer/Points").Text = player.StartPoints.ToString();
	}
	
	private void SendLabelInfo(string info)
	{
		GetNode<Label>("PanelContainer/DescriptionLabel").Text = Tr("DESCRIPTION" + info.ToUpper());
	}	
	private void OnPointsContainerEntered()
	{
		GetNode<Label>("PanelContainer/DescriptionLabel").Text = Tr("DESCRIPTIONPOINTS");
	}
}







