using System;

public enum SkillLevel{
	Novice,
	Expert,
	Master,
	Legend
}

public class Skill
{

	int points = 0;

	SkillLevel skilllevel;

	string _skillname;

public Skill(SkillLevel level)
{
	
}

public Skill(string skillname,SkillLevel level)
{
	_skillname = skillname;
	skilllevel = level;
}

public Skill()
{

}

public string Name{
	get{return _skillname;}
	set{_skillname = value;}
}

}
