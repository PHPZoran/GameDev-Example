using Godot;
using System;

public class Text : RichTextLabel
{
	public static bool instant = true;
	public static bool auto = true;
	public static bool signalready = false;
	[Signal]
	delegate void TextFinished();

public override void _Ready(){

}
public override void _Process(float delta){

	if(this.VisibleCharacters != this.GetTotalCharacterCount()){
		if(!instant){
			this.VisibleCharacters += 1;
			
		}
		else{
			this.VisibleCharacters = this.GetTotalCharacterCount();
			
		}
	}// && auto == true && signalready == true
	else if(this.VisibleCharacters >= this.GetTotalCharacterCount() && !signalready && auto){
		signalready = true;
		EmitSignal("TextFinished");
	}
}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
