
using Godot;
using System;

public class Icon : TextureRect
{
	[Signal]
	delegate void EquipmentChange(string character);
	
	public Slot slot = null;// = new Item();
	private Player player;
	public Slot GetSlot(){
		return this.slot;
	}
	
	public override void _Ready()
	{
		player = (Player)GetNode("/root/Player");
		GetSlotData();
		if(this.Name == "ItemIcon" && GetNodeOrNull("../../../../") != null){
			this.Connect(nameof(EquipmentChange), GetNode("../../../../"), "OnEquipmentChange");
		}
		

		
	}

public void unsetItem(){
	this.slot.Item = null;
//	GetNode<TextureRect>("Item").QueueFree();
}
public override void _GuiInput(InputEvent @event)
{
	// If right click, then equip item
	
	if (@event.IsActionReleased("ui_right")) {
		this.AcceptEvent();
	
		if (slot.Item != null) { 
			this.unsetItem();
		//	var parent = (BagContent) this.GetParent();
		//	parent.emitEquip(item);
		}
	}

	// If left click, force drag and drop
	if (@event.IsActionReleased("ui_left")) {
		this.AcceptEvent();
		var slot = this.GetSlot();

		if (slot != null && slot.Item != null) {
			this.unsetItem();
			var preview = this.getDragPreview(slot);
			this.ForceDrag(slot, preview);
		}
	}
}



public Control getDragPreview(Slot slot = null)
{
	if (slot != null && slot.Item != null && slot.Item.Visual != null) {
		var preview = new Control();
		var sprite = new Sprite(); 
		var img = (Texture)GD.Load(slot.Item.Visual);
		var size = img.GetSize();
		sprite.Texture = img;
		
		var th = 75;
		var tw = 79;
		var scale = new Vector2((size.x/(size.x/tw))/35, (size.y/(size.y/th))/35);

		sprite.Scale = scale;
		preview.AddChild(sprite);

		return preview;
	}
	return null;
}


public override object GetDragData(Vector2 position)
{
	if(slot.Item != null){
		var preview = this.getDragPreview(slot);
		if(preview != null){
			this.SetDragPreview(preview);
			slot.holder = this;
		}
	
		return slot;
	}
	return null;
}

public override bool CanDropData(Vector2 position, object _slot)
{
	Slot slot = (Slot)_slot;
		if(GetParent().Name.StartsWith("Slot") || GetParent().Name.StartsWith("Remove")){
			return true;
		}
		else if(this.Name.Contains(slot.Item.Category.ToString())){
			return true;
		}
		else{
			this.CallDeferred("force_drag", slot, this.getDragPreview(slot));
			return false;
		}
	} 

public override void DropData(Vector2 position, object _slot)
{
	Slot slot = (Slot)_slot;
	player.lostItem = null;
	if (Input.IsActionPressed("default_action")) { 
		this.CallDeferred("force_drag", slot, this.getDragPreview(slot));
	} else {	
		if(this.slot.Item == null || (this.slot.Item.Name == slot.Item.Name && this.slot.Item.Quantity + slot.Item.Quantity <= this.slot.Item.StackSize && this.slot.holder != slot.holder))
		{
			int increment = (this.slot.Item != null) ? this.slot.Item.Quantity : 0;
			if(GetParent().Name != "Remove"){
			setItemData(this,slot.Item,increment);
			this.slot.Item = slot.Item;
			this.slot.Item.Quantity += increment;
		}
			if(slot.holder != null){
		
			setItemData(slot.holder,null);
			var original = (Icon)slot.holder;
			original.slot.Item = null;
			}
		}
		
		else{
		
			if(slot.holder == null){
				switchItem(slot);
			}
			else if(slot.holder != this.slot.holder){
				swapItem(slot);
			}
		}
	}
}

public void switchItem(Slot slot){
	Slot temp = new Slot(new Item(this.slot.Item));
	temp.holder = null;
	if(this.slot.Item.Name == slot.Item.Name && this.slot.Item.StackSize > 1){
		stackItem(temp.Item,slot.Item);
	}
	setItemData(this,slot.Item);
	this.slot.Item = slot.Item;
	this.slot.holder = this;
	player.lostItem = temp.Item;
	this.CallDeferred("force_drag", temp, this.getDragPreview(temp));
}

public void stackItem(Item stacker, Item stacked){
	int remainder = stacked.Quantity + stacker.Quantity - stacker.StackSize;
		stacker.Quantity = stacker.Quantity + stacked.Quantity - stacker.StackSize;
		stacked.Quantity = stacked.StackSize;
}
public void swapItem(Slot slot)
{	
		var original = (Icon)slot.holder;
	if(this.slot.Item.Name == original.slot.Item.Name && this.slot.Item.StackSize > 1){
		stackItem(this.slot.Item,slot.Item);
	}
	if(validSlot(slot.holder,this.slot.Item)){
		setItemData(slot.holder,this.slot.Item);
		if(original != null){
		original.slot = this.slot;
		original.slot.holder = slot.holder;
		}
	}
	else{
		string key = slot.holder.Name.Substring(5);
		if(slot.holder.GetParent().Name.Contains("Player")){
			player.main.Equip(key,null); 
			EmitSignal("EquipmentChange", "Player");
		}
		else{
			player.companion.Equip(key,null);
			EmitSignal("EquipmentChange", "Companion");
		}
			original.slot = new Slot();
			slot.holder.Texture = null;
		this.slot.holder = null;
		player.lostItem = this.slot.Item;
		this.CallDeferred("force_drag", this.slot, this.getDragPreview(this.slot));
	}
		setItemData(this,slot.Item);
		this.slot = slot;
		this.slot.holder = this;
	
}
public bool validSlot(TextureRect node,Item item){
	if(node.GetParent().Name.StartsWith("Slot")){
		return true;
	}
	if(node.Name.Contains(item.Category.ToString())){
		return true;
	}
	else{
		return false;
	}
}
//Handles setting item data from item.
public void setItemData(TextureRect node,Item item,int increment = 0){
		int slotnum;
		if(node != null){
		if(node.GetParent().Name.StartsWith("Slot")){
			slotnum = Int32.Parse(node.GetParent().Name.Substring(4)) -1;
			player.inventory[slotnum] = item;
	}
	else if(node.Name.StartsWith("Equip")){
		string key = node.Name.Substring(5);
		if(node.GetParent().Name.Contains("Player")){
			player.main.Equip(key,item);
			EmitSignal("EquipmentChange", "Player");
		}
		else{
			player.companion.Equip(key,item);
			EmitSignal("EquipmentChange", "Companion");
		}
	}
	if(item != null){
		node.Texture = (Texture)GD.Load(item.Visual);
	}
	else{
		node.Texture = null;
	}
	
	node.GetNode<Label>("Quantity").Text = (item != null && item.Quantity + increment > 1) ? (increment + item.Quantity).ToString() : null;
	}
}
public void GetSlotData(){
	if(GetParent().Name == "Slot3"){
			slot = new Slot(new Item("Potion",Category.Potion));
			slot.holder = this;
			slot.Item.Visual = "assets/images/icons/items/Potion Bottle.png";
			slot.Item.Name = "Potion";
			slot.Item.StackSize = 5;
			slot.Item.Quantity = 3;
			player.inventory[2] = slot.Item;
			this.Texture = (Texture)GD.Load(slot.Item.Visual);	
		}
		else if(GetParent().Name == "Slot2"){
			slot = new Slot(new Item("Potion",Category.Potion));
			slot.holder = this;
			slot.Item.Visual = "assets/images/icons/items/Potion Bottle.png";
			slot.Item.Name = "Potion";
			slot.Item.StackSize = 5;
			slot.Item.Quantity = 1;
			player.inventory[1] = slot.Item;
			this.Texture = (Texture)GD.Load(slot.Item.Visual);
		}
		else if(GetParent().Name == "Slot20"){
			slot = new Slot(new Item("Leather",Category.Armor));
			slot.holder = this;
			slot.Item.Visual = "assets/images/icons/items/Leather.png";
			slot.Item.Name = "Leather";
			slot.Item.StackSize = 1;
			slot.Item.Quantity = 1;
			slot.Item.PierceAC = 1;
			slot.Item.MeleeAC = 1;
			player.inventory[19] = slot.Item;
			this.Texture = (Texture)GD.Load(slot.Item.Visual);
		}
		else if(this.Name == "EquipWeaponRight"){
			slot = new Slot(new Item("Bow",Category.Weapon));
			slot.Item.Visual = "assets/images/icons/items/Bow.png";
			string key = Name.Substring(5);
			slot.holder = this;
			slot.Item.Name = "Bow";
			slot.Item.Quantity = 1;
			if(GetParent().Name.Contains("Player")){
				player.main.Equip(key,slot.Item);
			}
			else{
				player.companion.Equip(key,slot.Item);
			}
			this.Texture = (Texture)GD.Load(slot.Item.Visual);
				
		}
		else if(GetParent().Name.StartsWith("Slot")){
			int slotnum = Int32.Parse(GetParent().Name.Substring(4)) -1;
			slot = new Slot();
			slot.Item = player.inventory[slotnum];
			
		}
		else if(this.Name.StartsWith("Equip")){
			string key = Name.Substring(5);
			slot = new Slot(new Item());
			if(GetParent().Name.Contains("Player")){
				slot.Item = player.main.GetEquipment(key);
			}
			else{
				slot.Item = player.companion.GetEquipment(key);
			}
			
				
		}	
		if(slot != null && slot.Item != null && GetParent().Name != "Remove" ){
			if(slot.Item.Visual != null){
				this.Texture = (Texture)GD.Load(slot.Item.Visual);
				GetNode<Label>("Quantity").Text = (slot.Item.Quantity > 1) ? slot.Item.Quantity.ToString() : null;
			}
			else{
				//Clear slot, we have no item texture to show.
				slot = new Slot();
			}
			
		}
}
}

