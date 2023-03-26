using Godot;
using System;
using System.Collections.Generic;

public class Quest{
	
	string _task;
	int _stage;
	int _xp;
	int _gold;
	private readonly static Dictionary<String, Quest> questdata = new Dictionary<String, Quest>(){
			{"QuestInnerNameNeverShown",new Quest("KeyShouldBeInCSVFile",250,100)}
			};
	public Quest(){
		_task = "";
		_stage = 0;
	}
	public Quest(Quest quest)
	{
		_task = quest.Task;
		_stage = quest.Stage;
		_xp = quest.XP;
		_gold = quest.Gold;
	}
	public Quest(string task,int experience = 0, int gold = 0, int stage = 0){
		_task = task;
		_stage = stage;
		_xp = experience;
		_gold = gold;
	}
	public Quest GetQuestData(string key){
		return questdata[key];
	}
	public string Task{
		get{return _task;}
		set{_task = value;}
	}
	
	public int Stage{
		get{return _stage;}
		set{_stage = value;}
	}
	public int Gold{
		get{return _gold;}
		set{_gold = value;}
	}
	public int XP{
		get{return _xp;}
		set{_xp = value;}
	}
}
