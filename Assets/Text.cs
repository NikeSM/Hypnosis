using UnityEngine;

public class TextSound : Item 
{
	private int _duration;
	private Categories _category;
	private Types _type;

	public TextSound()
	{
		_duration = 0;
		_category = Categories.Full;
		_type = Types.Relax;
	}
	public TextSound(int d, Categories c, Types t)
	{
		_duration = d;
		_category = c;
		_type = t;
		
	}
	public int Duration
	{
		get{return _duration;}
		set{_duration = value;}
	}
	public Categories Category
	{
		get{return _category;}
		set{_category = value;}
	}
	public Types Type
	{
		get{return _type;}
		set{_type = value;}
	}

	public enum Categories
	{
		Start,
		Finish,
		Full
	}
	public enum Types
	{
		Helth,
		Feer,
		Student,
		Relax
	}
}