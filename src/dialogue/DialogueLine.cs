using Godot;
using System;

	public enum State{
		location,//Location Text. Displays location options, location display. 
		end,//Marks end of dialogue. Displays location options, but last speaker display.
		response,//defines text as wanting a response. Response itself can be an End or ongoing.
		ongoing,//Ongoing, continues on and uses the responses as checks for where to go next, if valid.
		battle //Prepares battle transition
	}
public class DialogueLine : Reference
{
	
	public readonly State state;
	private string _speaker;
	private string _goto;
	private Condition[] condition;
	private Action[] action;
	private string[] _keys;
	public Condition[] GetCondition(){
		return condition;
	}
	public Action[] GetAction(){
		return action;
	}
	//Change to keys(?)
	public string[] Keys(){
		return _keys;
	}
	public string GoTo(){
		return _goto;
	}
	public DialogueLine(){}
		public DialogueLine(string speaker, State state,string nextline = null, Condition[] condition = null, Action[] action = null,string[] keys = null){
		this.state = state;
		_speaker = speaker;
		_goto = nextline;
		this.condition = condition;
		this.action = action;
		_keys = keys;
		
	}
}

