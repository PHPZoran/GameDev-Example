using Godot;
using System;

public class GameInfo : Reference
{
 private string _charInfo;
 private string _timeInfo;
 private string _locationInfo;

public GameInfo(){
	
}
public GameInfo(string charInfo, string timeInfo, string locationInfo){
	_charInfo = charInfo;
	_timeInfo = timeInfo;
	_locationInfo = locationInfo;
}
public string CharInfo{
	get{return _charInfo;}
	set{_charInfo = value;}
}
public string TimeInfo{
	get{return _timeInfo;}
	set{_timeInfo = value;}
}
public string LocationInfo{
	get{return _locationInfo;}
	set{_locationInfo = value;}
}
}
