using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;

public class Saves :MonoBehaviour  {
	public Objects objects;
	public Images images;

	private List<Session> Sessions = new List<Session>();

	public void saveSession (Session ss) {

		string names = "";
		if(PlayerPrefs.HasKey("names")) {
			names = PlayerPrefs.GetString("names");
		}
		PlayerPrefs.SetString("names", names + "$" + ss.Name);
		int i;
		string s = "";
		for (i = 0; i < ss.getTexts.Count; i++) {
			s = s + "$" + ss.getTexts[i].Id.ToString();
		}
		PlayerPrefs.SetString(ss.Name + "text", s);
		s = "";
		for (i = 0; i < ss.getMusic.Count; i++) {
			s = s + "$" + ss.getMusic[i].Id.ToString();
		}
		PlayerPrefs.SetString(ss.Name + "music", s);
		s = "";
		s = s + ss.getImage.Id.ToString();
		PlayerPrefs.SetString(ss.Name + "image", s);
	}
	
	public void clickToSave(){	
		string txt = objects.panel_save_enterName.text;
		addSession (txt);
	}
	public void loadSessions () {
		
		Sessions = new List<Session>();
		int i, j;
		Session ss;
		string[] names = PlayerPrefs.GetString ("names").Split('$');
		for(i = 1; i < names.Length; i++) {
			ss = new Session (names[i]);
			string s = PlayerPrefs.GetString(names[i] + "text");
			string[] sp = s.Split('$');

			for(j = 1; j < sp.Length; j++){
				ss.addElement(DataBase.DataTexts.Find( x => x.Id == System.Int32.Parse(sp[j])));
			}
			s = PlayerPrefs.GetString(names[i] + "music");
			sp = s.Split('$');

			for(j = 1; j < sp.Length; j++){
				ss.addElement(DataBase.DataMusic.Find( x => x.Id == System.Int32.Parse(sp[j])));
			}

			s = PlayerPrefs.GetString(names[i] + "image");

			if(s != "") {
				ss.addElement(DataBase.DataImages.Find( x => x.Id == System.Int32.Parse(s)));
			}
			Sessions.Add(ss);

		}
	}

	public void addSession(string sessionName) {
		Session ss = new Session(sessionName);

		foreach (Transform child in objects.scroll_cs_textResults.transform) {
			string name = child.GetComponentInChildren<Text>().text;
			TextSound txt = DataBase.DataTexts.Find(x => x.Name == name);
			ss.addElement(txt);
		}
		foreach (Transform child in objects.scroll_cs_musicResults.transform) {
			string name = child.GetComponentInChildren<Text>().text;
			MusicSound ms = DataBase.DataMusic.Find(x => x.Name == name);
			ss.addElement(ms);
		}
		if (CurrentState.current_image == null) {
			CurrentState.current_image = DataBase.DataImages[0];
		}
		ss.addElement (CurrentState.current_image);
		Sessions.Add (ss);
		saveSession (ss);
	}

	UnityEngine.Events.UnityAction destroyButton(GameObject button) {
		return () => {
			RectTransform panelRectTransform = button.transform.parent.gameObject.GetComponent<RectTransform>();
			panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
			Destroy(button);
			
		};
	}
	UnityEngine.Events.UnityAction loadCurrentSession(int i) {
		return () => {
			if (CurrentState.last_panel == "cs") {
				foreach (Transform child in objects.scroll_cs_textResults.transform) {
					GameObject.Destroy (child.gameObject);
					RectTransform panelRectTransform = objects.scroll_cs_textResults.GetComponent<RectTransform>();
					panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
				}
				foreach (Transform child in objects.scroll_cs_musicResults.transform) {
					GameObject.Destroy (child.gameObject);
					RectTransform panelRectTransform = objects.scroll_cs_musicResults.GetComponent<RectTransform>();
					panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
				}

			} else {
				if(CurrentState.last_panel == "main") { 
					foreach (Transform child in objects.scroll_main_text.transform) {
						GameObject.Destroy (child.gameObject);
						RectTransform panelRectTransform = objects.scroll_main_text.GetComponent<RectTransform>();
						panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
					}
					foreach (Transform child in objects.scroll_main_music.transform) {
						GameObject.Destroy (child.gameObject);
						RectTransform panelRectTransform = objects.scroll_main_music.GetComponent<RectTransform>();
						panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
					}
				}
			}
			CurrentState.current_panel = CurrentState.last_panel;
			CurrentState.last_panel = "load";
			showSession(i);
			objects.panel_load_background.SetActive(false);
			};
	}
	public void selectSession(){
		loadSessions ();
		GameObject parentObject = objects.scroll_load_list;
		foreach (Transform child in parentObject.transform) {
			GameObject.Destroy (child.gameObject);
			RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
			panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
		}

		int i;
		for (i = 0; i < Sessions.Count; i++) {
			GameObject button = (GameObject)Instantiate (Resources.Load ("ListButton"));
			button.GetComponentInChildren<Text> ().text = Sessions [i].Name;
			button.transform.SetParent(parentObject.transform);
			UnityEngine.Events.UnityAction action = loadCurrentSession(i);
			RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
			panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);
			button.GetComponent<Button>().onClick.AddListener(action);
		}
	}


	public void showSession(int id) {
		Session ss = Sessions [id];
		int i;
		GameObject button;
		GameObject parentObject;
		if (CurrentState.current_panel == "cs") {
			parentObject = objects.scroll_cs_textResults;

			for (i = 0; i < ss.getTexts.Count; i++) {
				button = (GameObject)Instantiate (Resources.Load ("ListButton"));
				RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
				panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);
				button.GetComponentInChildren<Text> ().text = ss.getTexts [i].Name;
				button.transform.SetParent(parentObject.transform);
				UnityEngine.Events.UnityAction action = destroyButton (button);
			
				button.GetComponent<Button> ().onClick.AddListener (action);
			}
			parentObject = objects.scroll_cs_musicResults;
			for (i = 0; i < ss.getMusic.Count; i++) {
				button = (GameObject)Instantiate (Resources.Load ("ListButton"));
				RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
				panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);
				button.GetComponentInChildren<Text> ().text = ss.getMusic [i].Name;
				button.transform.SetParent(parentObject.transform);
				UnityEngine.Events.UnityAction action = destroyButton (button);
			
				button.GetComponent<Button> ().onClick.AddListener (action);
			}
			string img_name = ss.getImage.Name;
			objects.panel_cs_image.SetActive(true);
			foreach (Transform child in objects.scroll_cs_images.transform) {
				if(child.GetComponentInChildren<Text> ().text == img_name){
					child.GetComponent<Image> ().sprite = images.selected_listButton;
					CurrentState.current_image = ss.getImage;
				}
			}
			objects.panel_cs_image.SetActive(false);
			objects.panel_cs_music.SetActive(false);
			objects.panel_cs_text.SetActive(true);
		} else {
			if(CurrentState.current_panel == "main") { 
				objects.text_main_sessionName.text = ss.Name;
				parentObject = objects.scroll_main_text;
				for (i = 0; i < ss.getTexts.Count; i++) {
					button = (GameObject)Instantiate (Resources.Load ("ListButton"));
					RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
					panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);
					button.GetComponentInChildren<Text> ().text = ss.getTexts [i].Name;
					button.transform.SetParent(parentObject.transform);
				}
				parentObject = objects.scroll_main_music;
				for (i = 0; i < ss.getMusic.Count; i++) {
					button = (GameObject)Instantiate (Resources.Load ("ListButton"));
					RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
					panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);
					button.GetComponentInChildren<Text> ().text = ss.getMusic [i].Name;
					button.transform.SetParent(parentObject.transform);
				}
				CurrentState.current_session = ss;
				objects.panel_ps_background.SetActive(false);
				CurrentState.new_session = true;
			}

		}
	}


}
