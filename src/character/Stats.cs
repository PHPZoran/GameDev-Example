using Godot;
using System;
using System.Reflection;

public class Stats : Control
{ 
	private PC character;
	private Player player;
	// private int a = 2;
	// private string b = "text";

	// Called when the nsode enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		//player.FirstName = "Ronald";
		//player.companion.FirstName = "Heather";
		character = GetCharacter();
		SetValues(character);
		if(character.StatPoints > 0){
			GetTree().CallGroup("IncrementButtons", "ToggleOn",this.Name);
		}
	}

private void OnIncrementOfAttribute(string name)
{
	PropertyInfo attribute = typeof(PC).GetProperty(name);
	int value = (int)attribute.GetValue(character, null) + 1;
	attribute.SetValue(character,value);
	GetNode<Label>("AttributeContainer/" + name + "/Value").Text = value.ToString();
	character.StatPoints -= 1;
	//object value = pinfo.GetValue(YourInstantiatedObject, null);
	if(character.StatPoints == 0){
		GetTree().CallGroup("IncrementButtons", "ToggleOff",this.Name);
	} 
	Refresh();
}
public void Refresh(){
	SetValues(character);
}
private void SetValues(PC character){
	
	GetNode<Label>("NameAndPoints/Name").Text = character.FullName;
	GetNode<Label>("NameAndPoints/Points").Text = Tr("LABELATTRIBUTEPOINTS") + ": " + character.StatPoints.ToString();
	GetNode<Label>("AttributeContainer/Might/Value").Text = character.Might.ToString();
	GetNode<Label>("AttributeContainer/Agility/Value").Text = character.Agility.ToString();
	GetNode<Label>("AttributeContainer/Vitality/Value").Text = character.Vitality.ToString();
	GetNode<Label>("AttributeContainer/Magic/Value").Text = character.Magic.ToString();
	GetNode<Label>("AttributeContainer/Willpower/Value").Text = character.Willpower.ToString();
	GetNode<Label>("AttributeContainer/Trickery/Value").Text = character.Trickery.ToString();
	GetNode<Label>("AttributeContainer/Health/Value").Text = character.CurrentHealth.ToString() + "/" + character.MaxHealth.ToString();
	GetNode<Label>("AttributeContainer/Energy/Value").Text = character.CurrentEnergy.ToString() + "/" + character.MaxEnergy.ToString();
	GetNode<Label>("AttributeContainer/PierceArmor/Value").Text = character.PierceAC.ToString();
	GetNode<Label>("AttributeContainer/MeleeArmor/Value").Text = character.MeleeAC.ToString();
	GetNode<Label>("AttributeContainer/MagicArmor/Value").Text = character.MagicAC.ToString();
	GetNode<Label>("AttributeContainer/Damage/Value").Text = character.DamageMin.ToString() + "/" + character.DamageMax.ToString();
	GetNode<Label>("AttributeContainer/DamageType/Label").Text = "LABELTYPE" + ": " + DetermineDamageType();
	GetNode<Label>("AttributeContainer2/Class/Value").Text = "Class: "; // + character.Profession.ClassName.ToString()
	GetNode<Label>("AttributeContainer2/Level/Value").Text = character.Level.ToString();
	GetNode<Label>("AttributeContainer2/XP/Value").Text = character.XP.ToString();
	GetNode<Label>("AttributeContainer2/NextLevel/Value").Text = character.NextLevel.ToString();
	GetNode<Label>("AttributeContainer2/Age/Value").Text = character.Age.ToString();
	GetNode<Label>("AttributeContainer2/Sex/Value").Text = character.Sex.ToString();
	GetNode<Label>("AttributeContainer2/Orientation/Value").Text = character.Orientation.ToString();
	GetNode<Label>("AttributeContainer2/Race/Value").Text = character.Race.Name.ToString();
	GetNode<Label>("AttributeContainer2/Fire/Value").Text = character.FireRes.ToString();
	GetNode<Label>("AttributeContainer2/Water/Value").Text = character.WaterRes.ToString();
	GetNode<Label>("AttributeContainer2/Earth/Value").Text = character.EarthRes.ToString();
	GetNode<Label>("AttributeContainer2/Air/Value").Text = character.AirRes.ToString();
	GetNode<Label>("AttributeContainer2/Mental/Value").Text = character.MentalRes.ToString();
	GetNode<Label>("AttributeContainer2/Physical/Value").Text = character.PhysicalRes.ToString();
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

private string DetermineDamageType()
{
	if(character.GetEquipment("WeaponRight") != null && character.GetEquipment("WeaponLeft") != null)
	{
		if(character.GetEquipment("WeaponRight").Category == Category.Shield){
			return character.GetEquipment("WeaponLeft").DamageType.ToString();	
		}
		else if(character.GetEquipment("WeaponLeft").Category == Category.Shield){
			return character.GetEquipment("WeaponRight").DamageType.ToString();	
		}
		else if(character.GetEquipment("WeaponRight").DamageType == character.GetEquipment("WeaponLeft").DamageType){
			return character.GetEquipment("WeaponRight").DamageType.ToString();	
		}
		else{
			return character.GetEquipment("WeaponRight").DamageType.ToString() + "/" + character.GetEquipment("WeaponLeft").DamageType.ToString();	
		}
	}
	else if(character.GetEquipment("WeaponRight") != null){
		return character.GetEquipment("WeaponRight").DamageType.ToString();	
	}
	else if(character.GetEquipment("WeaponLeft") != null){
		return character.GetEquipment("WeaponLeft").DamageType.ToString();	
	}
	
	return "Melee";
	
}

}








