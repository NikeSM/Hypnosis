using UnityEngine;

public class MusicSound : Item 
{
	private int _duration;
	public MusicSound()
	{
		_duration = 0;
	}
	public MusicSound(int d)
	{
		_duration = d;	
	}
	public int Duration
	{
		get{return _duration;}
		set{_duration = value;}
	}
}