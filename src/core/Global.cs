using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{	
	public enum CombatStatus
{
  None,
  SingleAttack,
  AllAttack
}
	private string loadfile;
	private bool _auto = false;
	private bool _nsfw = true;
	public static CombatStatus combatstatus = CombatStatus.None;
	public List<Character> currentEnemies; 
	
	public Node CurrentScene { get; set; }
	
	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
	}
	public void GotoScene(string path)
{
	// This function will usually be called from a signal callback,
	// or some other function from the running scene.
	// Deleting the current scene at this point might be
	// a bad idea, because it may be inside of a callback or function of it.
	// The worst case will be a crash or unexpected behavior.

	// The way around this is deferring the load to a later time, when
	// it is ensured that no code from the current scene is running:

	CallDeferred(nameof(DeferredGotoScene), path);
}

public void DeferredGotoScene(string path)
{
	// Immediately free the current scene, there is no risk here.
	CurrentScene.Free();

	// Load a new scene.
	var nextScene = (PackedScene)GD.Load(path);

	// Instance the new scene.
	CurrentScene = nextScene.Instance();

	// Add it to the active scene, as child of root.
	GetTree().Root.AddChild(CurrentScene);

	// Optional, to make it compatible with the SceneTree.change_scene() API.
	GetTree().CurrentScene = CurrentScene;
}
public string LoadFile{
	get{return loadfile;}
	set{loadfile = value;}
}
public bool Auto{
	get{return _auto;}
	set{_auto = value;}
}
public bool NSFW{
	get{return _nsfw;}
	set{_nsfw = value;}
}
}

