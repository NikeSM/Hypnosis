using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class Session {

	private string _name;
	private List<TextSound> Texts = new List<TextSound> ();
	private Img Image = new Img();
	private List<MusicSound> Music = new List<MusicSound>();

	public Session()
	{
		_name = "";
	}
	public Session(string name)
	{
		_name = name;
	}
	public string Name
	{
		get{return _name;}
		set{_name = value;}
	}
	public List<TextSound>  getTexts
	{
		get{return Texts;}
		set{Texts = value;}
	}

	public List<MusicSound> getMusic
	{
		get{return Music;}
		set{Music = value;}
	}

	public Img getImage
	{
		get{return Image;}
		set{Image = value;}
	}

	public void addElement(TextSound ts){
		Texts.Add (ts);
	}

	public void addElement(MusicSound ms){
		Music.Add (ms);
	}
	public void addElement(Img img){
		Image = img;
	}
	public void refreshSession(){
		Texts = new List<TextSound>();
		Image = new Img();
    	Music = new List<MusicSound>();
	}
}

