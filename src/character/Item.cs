using Godot;
using System;
using System.Collections.Generic;

public enum Category{
		Miscellanious,
		Helmet,
		Armor,
		Weapon,
		Necklace,
		Ring,
		Shield,
		Potion,
		Consumable
	}
public enum DamageType{
	Melee,
	Magic,
	Piercing
}
public class Item 
{
	public readonly Category Category;
	public readonly DamageType DamageType;
	public string _name;
	private const string dir = "assets/images/icons/items/";
	private string _visual;
	private int _stacksize;
	private int _quantity;
	private bool _identified;
	private int _damagemin;
	private int _damagemax;
	private int _pierceAC;
	private int _meleeAC;
	private int _magicAC;
	public readonly static Dictionary<String, Item> data = new Dictionary<String, Item>{
	{"Sword",new Item("Sword",Category.Weapon,dir + "Bow.png",1,1)},
	{"Crossbow",new Item("Crossbow",Category.Weapon,dir + "Bow.png",1,1,1,10)},
	{"Leather",new Item("Leather",Category.Armor,dir + "Leather.png",1,1,0,0,1,1)},
	};
	
	public Item(){
	}
	public Item(string name){
		_name = name;
		Category = Category.Weapon;
		_visual = dir + "Bow.png";
		_stacksize = 0;
		_quantity = 1;
		_damagemin = 1;
		_damagemax = 6;
	}
	public Item(string name, Category category){
		Category = category;
		_name = name;
		_visual = dir + "Bow.png";
		_stacksize = 0;
		_quantity = 1;
		_damagemin = 1;
		_damagemax = 8;
	}
	public Item(string name, Category category, string texture, int stacksize, int quantity, int damagemin = 0, int damagemax = 0, int pierce = 0, int melee = 0, int magic = 0){
		Category = category;
		_name = name;
		_visual = texture;
		_stacksize = stacksize;
		_quantity = quantity;
	}
	public Item(Item item){
		Category = item.Category;
		_name = item.Name;
		_visual = item.Visual;
		_stacksize = item.StackSize;
		_quantity = item.Quantity;
	}
	public string Visual{
		get{return _visual;}
		//temp
		set{_visual = value;}
	}
	
	public string Name{
		get{return _name;}
		set{_name = value;}
	} 
	
	public int StackSize{
		get{return _stacksize;}
		set{_stacksize = value;}
	}
	
	public int Quantity{
		get{return _quantity;}
		set{_quantity = value;}
	}
	public Item Data(string key){
		return data[key];
	}
	
	public int DamageMin{
		get{return _damagemin;}
		set{_damagemin = value;}
	}
	public int DamageMax{
		get{return _damagemax;}
		set{_damagemax = value;}
	} 
	public int PierceAC{
	get{return _pierceAC;}
	set{_pierceAC = value;}
}
public int MeleeAC{
	get{return _meleeAC;}
	set{_meleeAC = value;}
}
public int MagicAC{
	get{return _magicAC;}
	set{_magicAC = value;}
}
	//public string ItemName{
//	get{return _name;}
		//set{_name = value;}
	//}
	// Called when the node enters the scene tree for the first time.


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
