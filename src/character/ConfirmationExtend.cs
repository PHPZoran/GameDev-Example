using Godot;
using System;

public class ConfirmationExtend : ConfirmationDialog
{
	//Attach this to all confirmation dialogs.
	public override void _Ready()
	{  	
		GetCancel().Text = Tr("LABELCANCEL");
		GetOk().Text = Tr("LABELOKAY");
	}

}
