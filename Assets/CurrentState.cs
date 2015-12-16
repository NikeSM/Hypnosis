using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public static class CurrentState {
	public static string current_panel = "cs";
	public static string last_panel = "cs";

	public static bool new_session = false;
	public static Session current_session = null;

	public static GameObject current_button = null;

	public static Img current_image = null;
	public static GameObject current_state_button = null;
	public static GameObject current_result_panel = null;

	public static List<TextSound> DataTexts = DataBase.DataTexts;
	public static List<Img> DataImages = DataBase.DataImages;
	public static List<MusicSound> DataMusic = DataBase.DataMusic;

	public static Image demo_panel = null;

	public static float button_size = 55;
}
