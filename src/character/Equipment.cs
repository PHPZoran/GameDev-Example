using Godot;
using System;

public class Equipment : NinePatchRect
{
private void OnEquipmentChange(string character)
{
	if(GetParent().Name == "DataTabs"){
		GetNode<Stats>("../LABELSTATS/" + character + "Stats").Refresh();
	}
}

}

















