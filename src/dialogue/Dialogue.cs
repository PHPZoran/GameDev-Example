using Godot;
using System;

using System.Collections.Generic;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(Dialogue))]
public class Dialogue : Resource
{

/*           DIALOGUE LINES: A Breakdown.
*The Dictionary below stores all of the dialogue lines in the game.
*The Dialogue line key both accesses the line and is the key to the actual text from the csv file.
*The first part of dialogue line is a speaker. It may just point to a png at some point.
*The next part is its state. This determines if the responses are location or normal based, or if a response shoudl be considered at all (ongoing, battle.) 
*After state is where the lin goes to next. This will be null if a response is required.
*After the go to is the condition array. These can be one or more conditions that determine if the line can fire. They are only checked if arrived via one or more keys.
*Actions handle any triggers we want to fire, such as gold increase, quest changes, etc.
*Finally, keys handle an array of where to traverse next. Each of these will have their condition checked. They are checked in order, so the last one should always be true and does not need any conditions.
			*/
	private static Godot.Collections.Dictionary<String, DialogueLine> _lines = new Godot.Collections.Dictionary<String, DialogueLine>(){
		{"STARTSPRING",new DialogueLine("HOME",State.location,"STARTLOCATION2",null,new Action[]{new Action("StartQuest",new object[]{"Player","QuestInnerNameNeverShown"})})},
		{"STARTSUMMER",new DialogueLine("HOME",State.location,"STARTLOCATION2",null,new Action[]{new Action("StartQuest",new object[]{"Player","QuestInnerNameNeverShown"})})},
		{"STARTAUTUMN",new DialogueLine("HOME",State.location,"STARTLOCATION2",null,new Action[]{new Action("StartQuest",new object[]{"Player","QuestInnerNameNeverShown"})})},
		{"STARTWINTER",new DialogueLine("HOME",State.location,"STARTLOCATION2",null,new Action[]{new Action("StartQuest",new object[]{"Player","QuestInnerNameNeverShown"})})},
		{"STARTLOCATION1",new DialogueLine("HOME",State.location,"STARTLOCATION2",null,new Action[]{new Action("StartQuest",new object[]{"Player","QuestInnerNameNeverShown"})})},
		{"STARTLOCATION2",new DialogueLine("HOME",State.location,"STARTDIALOGUE1")},
		{"STARTDIALOGUE1",new DialogueLine("PARENT",State.response,null,new Condition[]{new Condition("CheckRelationship",new object[]{"Player",Relationship.sibling})},null,new String[]{"STARTRESPONSE1","STARTRESPONSE2","STARTRESPONSE3","STARTRESPONSE4","STARTRESPONSE5"})},
		{"STARTRESPONSE1",new DialogueLine("Player",State.ongoing,"STARTLOCATION3",null,new Action[]{new Action("CompleteQuest",new object[]{"Player","QuestInnerNameNeverShown"})})},
		{"STARTRESPONSE2",new DialogueLine("Player",State.response,null)},
		{"STARTRESPONSE3",new DialogueLine("Player",State.response,null,null,new Action[]{new Action("SetVariable",new object[]{"Player","StartGift",2})})},
		{"STARTRESPONSE4",new DialogueLine("Player",State.response,null,null,new Action[]{new Action("SetVariable",new object[]{"Player","StartGift",3})})},
		{"STARTRESPONSE5",new DialogueLine("Player",State.response,null,null,new Action[]{new Action("SetVariable",new object[]{"Player","StartGift",4})})},
		{"STARTLOCATION3",new DialogueLine("PARENT",State.end,null,null,null,new String[]{"STARTGOUP1"})},
		{"STARTGOUP1",new DialogueLine("Home",State.ongoing,null,null,null,new String[]{"STARTMEETSIBLING","STARTUPROOM"})},
		{"STARTMEETSIBLING",new DialogueLine("Companion",State.ongoing,"STARTUPROOM",new Condition[]{new Condition("CheckRelationship",new object[]{"Player", Relationship.sibling})})},
		{"STARTUPROOM",new DialogueLine("HOMEUP",State.ongoing,null,null,null,new String[]{"STARTGETWEAPON1","STARTGETWEAPON2"})},
		{"STARTGETWEAPON1",new DialogueLine("HOMEBEDROOM",State.ongoing,"STARTCOMMOTION1",new Condition[]{new Condition("CheckAgilityGTMight",new object[]{"Player"})},new Action[]{new Action("CreateItem",new object[]{"Player","Crossbow",1})})},
		{"STARTGETWEAPON2",new DialogueLine("HOMEBEDROOM",State.ongoing,"STARTCOMMOTION1",null,new Action[]{new Action("CreateItem",new object[]{"Player","Sword",1})})},
		{"STARTCOMMOTION1",new DialogueLine("HOMEBEDROOM",State.response,null,null,null,new String[]{"STARTCOMMOTION2","STARTCOMMOTION3"})},
		{"STARTCOMMOTION2",new DialogueLine("Player",State.ongoing,"STARTAMBUSH1A")},
		{"STARTCOMMOTION3",new DialogueLine("Player",State.ongoing,"STARTAMBUSH1B")},
		{"STARTAMBUSH1A",new DialogueLine("ENEMY",State.ongoing,"STARTENEMYENTER")},
		{"STARTAMBUSH1B",new DialogueLine("ENEMY",State.ongoing,"STARTENEMYENTER")},
		{"STARTENEMYENTER", new DialogueLine("ENEMY",State.ongoing,"STARTCOMPANIONENTER")},
		{"STARTCOMPANIONENTER",new DialogueLine("ENEMY",State.ongoing,null,null,null,new String[]{"STARTCOMPANIONGREET1","STARTCOMPANIONGREET2"})},
		{"STARTCOMPANIONGREET1",new DialogueLine("COMPANION",State.ongoing,"STARTBATTLE1")},
		{"STARTCOMPANIONGREET2",new DialogueLine("COMPANION",State.ongoing,"STARTBATTLE1")},
		{"STARTBATTLE1",new DialogueLine("ENEMY",State.battle,"STARTPOSTBATTLE1",null,null,new String[]{"startassassin","startassassintest"})},
		{"STARTPOSTBATTLE1",new DialogueLine("Companion",State.ongoing,null,null,null,new String[]{null,null,null})},	
		{"STARTDEFEAT",new DialogueLine("ENEMY",State.end,null,null,null,null)}
	};

	public DialogueLine GetLine(string key)	
	
	{

		return _lines[key];
		
	}
	
	
	public Dialogue(){}

	//public DialogueLine handleLine(string key){
		//DialogueLine dialogue = _lines[key];
		//return dialogue;
	//}
}

