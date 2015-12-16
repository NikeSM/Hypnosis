using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public static class DataBase {
	public static List<TextSound> DataTexts = new List<TextSound>();
	public static List<Img> DataImages = new List<Img>();
	public static List<MusicSound> DataMusic = new List<MusicSound>();


	private static string pathText = "Text/";
	private static string pathMusic = "Music/";
	private static string pathImage = "Image/";



	public static void get_list_text () {
		TextSound T1 = new TextSound(100, TextSound.Categories.Full, TextSound.Types.Feer);
		T1.Name = "Антистресс";
		T1.Id = 1;
		T1.Buy = true;
		T1.Load = true;
		if (T1.Load == true) {
			T1.Content = pathText + T1.Name;
		}
		DataTexts.Add(T1);
		
		TextSound T2 = new TextSound(1000, TextSound.Categories.Finish, TextSound.Types.Helth);
		T2.Name = "Развитие воображения";
		T2.Id = 2;
		T2.Buy = true;
		T2.Load = true;
		if (T2.Load == true) {
			T2.Content = pathText + T2.Name;
		}
		DataTexts.Add(T2);
		
		TextSound T3 = new TextSound(10, TextSound.Categories.Start, TextSound.Types.Student);
		T3.Name = "Гармонизация организма";
		T3.Id = 3;
		T3.Buy = true;
		T3.Load = true;
		if (T3.Load == true) {
			T3.Content = pathText + T3.Name;
		}
		DataTexts.Add(T3);

		TextSound T4 = new TextSound(10, TextSound.Categories.Start, TextSound.Types.Student);
		T4.Name = "Творчество и баланс";
		T4.Id = 3;
		T4.Buy = true;
		T4.Load = true;
		if (T4.Load == true) {
			T4.Content = pathText + T4.Name;
		}
		DataTexts.Add(T4);

		TextSound Song = new TextSound(10, TextSound.Categories.Start, TextSound.Types.Student);
		Song.Name = "Song";
		Song.Id = 4;
		Song.Buy = true;
		Song.Load = true;
		if (Song.Load == true) {
			Song.Content = pathMusic + Song.Name;
		}
		DataTexts.Add(Song);
		
	}
	public static void get_list_music () {
		MusicSound Sigmund = new MusicSound(100);
		Sigmund.Name = "Song";
		Sigmund.Id = 1;
		Sigmund.Buy = true;
		Sigmund.Load = true;
		if (Sigmund.Load == true) {
			Sigmund.Content = pathMusic + Sigmund.Name;
		}
		DataMusic.Add(Sigmund);
		
		MusicSound Mayonnaise = new MusicSound(1000);
		Mayonnaise.Name = "Sleep Away";
		Mayonnaise.Id = 2;
		Mayonnaise.Buy = false;
		Mayonnaise.Load = false;
		if (Mayonnaise.Load == true) {
			Mayonnaise.Content = pathMusic + Mayonnaise.Name;
		}
		DataMusic.Add(Mayonnaise);
		
		MusicSound Isolation = new MusicSound(10);
		Isolation.Name = "Maid with the Flaxen Hair";
		Isolation.Id = 3;
		Isolation.Buy = true;
		Isolation.Load = false;
		if (Isolation.Load == true) {
			Isolation.Content = pathMusic + Isolation.Name;
		}
		DataMusic.Add(Isolation);
	}
	public static void get_list_images () {

		Img Track = new Img (20, 0.01f);
		Track.Name = "1";
		Track.Id = 1;
		Track.Buy = true;
		Track.Load = true;
		Track.Content = pathImage + "Track/Track";
		DataImages.Add (Track);

		Img Circle = new Img (45, 0.05f);
		Circle.Name = "2";
		Circle.Id = 2;
		Circle.Buy = true;
		Circle.Load = true;
		Circle.Content = pathImage + "Circle/Circle";
		DataImages.Add (Circle);

		Img Circles = new Img (59, 0.05f);
		Circles.Name = "3";
		Circles.Id = 3;
		Circles.Buy = true;
		Circles.Load = true;
		Circles.Content = pathImage + "Circles/Circles";
		DataImages.Add (Circles);

	}



}
