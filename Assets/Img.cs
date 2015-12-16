using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class Img : Item 
{
	private int _size;
	private float _delta;

	public Img()
	{
		_size = 0;
		_delta = 0;
	}
	public Img(int sz, float d)
	{
		_size = sz;	
		_delta = d;
	}
	public int Size
	{
		get{return _size;}
		set{_size = value;}
	}
	
	public float Delta
	{
		get{return _delta;}
		set{_delta = value;}
	}
}