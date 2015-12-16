using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CreateLists : MonoBehaviour 
{
	public Images images;
	public Objects objects;
	public Gif Gif;
	private MySlider musicSlider = null;
	private MySlider textSlider = null;
	public ButtonListFactory buttonListFactory;

	public void add_listeners(){

		objects.button_cs_addText.onClick.AddListener (() => { add_to_res();});
		objects.button_cs_addMusic.onClick.AddListener (() => { add_to_res();});
	}
	

	public void setState (string state) {
		if (state == "last") {
			string tmp = CurrentState.last_panel;
			CurrentState.last_panel = CurrentState.current_panel;
			CurrentState.current_panel = tmp;
		} else {
			CurrentState.last_panel = CurrentState.current_panel;
			CurrentState.current_panel = state;
		}
	}

	public void resetSliders() {
		foreach (Transform child in objects.scroll_cs_text.transform) {
			child.GetComponent<Image>().sprite = images.listButton;
		}
		foreach (Transform child in objects.scroll_cs_music.transform) {
			child.GetComponent<Image>().sprite = images.listButton;
		}
		textSlider.Stop();
		musicSlider.Stop ();
		CurrentState.current_button = null;
		CurrentState.current_result_panel = null;
	}				
	public void pauseSliders() {
		musicSlider.Pause ();
		textSlider.Pause ();
	}	
	UnityEngine.Events.UnityAction changeSelectText(GameObject button, GameObject parent, TextSound txt) {
		return () => {
			string ac = "";
			if (txt != null){
				ac = txt.Content;
			}
			foreach (Transform child in objects.scroll_cs_text.transform) {
				child.GetComponent<Image>().sprite = images.listButton;
			}
			if(CurrentState.current_button != button && button != null){
				CurrentState.current_button = button;
				CurrentState.current_result_panel = parent;
				textSlider.setCurrentSound(ac);
				textSlider.Play();
				button.GetComponent<Image>().sprite = images.selected_listButton;

			}else{
				textSlider.Stop();
				CurrentState.current_button = null;
				CurrentState.current_result_panel = null;
			}
			
		};
	}

	UnityEngine.Events.UnityAction changeSelectMusic(GameObject button, GameObject parent, MusicSound ms) {
		return () => {
			string ac = "";
			if(ms != null) {
				ac = ms.Content;
			}
			foreach (Transform child in objects.scroll_cs_music.transform) {
				child.GetComponent<Image> ().sprite = images.listButton;
			}
			if (CurrentState.current_button != button && button != null) {
				CurrentState.current_button = button;
				CurrentState.current_result_panel = parent;
				musicSlider.setCurrentSound (ac);
				musicSlider.Play ();
				button.GetComponent<Image> ().sprite = images.selected_listButton;
				
			} else {
				musicSlider.Stop ();
				CurrentState.current_button = null;
				CurrentState.current_result_panel = null;
			}
		};
	}

	UnityEngine.Events.UnityAction changeSelectImage(GameObject button, Img img) {
		return () => {
			foreach (Transform child in objects.scroll_cs_images.transform) {
				child.GetComponent<Image> ().sprite = images.listButton;
			}
			if (CurrentState.current_button != button && button != null) {
				CurrentState.current_button = button;
				CurrentState.current_result_panel = null;
				CurrentState.current_image = img;
				Image i = objects.panel_cs_demo;
				Gif.start(i, img);
				button.GetComponent<Image> ().sprite = images.selected_listButton;
				
			} else {
				Gif.stop();
				CurrentState.current_button = null;
				CurrentState.current_result_panel = null;
				CurrentState.current_image = null;
			}

		};
	}
	UnityEngine.Events.UnityAction destroyButton(GameObject button) {
		return () => {
			RectTransform panelRectTransform = button.transform.parent.gameObject.GetComponent<RectTransform>();
			panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - CurrentState.button_size);
			Destroy(button);
		};
	}

	void Update() {
		musicSlider.refreshSlider ();
		textSlider.refreshSlider ();
	}

	
	public void CloneButton (GameObject button, GameObject parent) {
		if (button.activeInHierarchy) {
			GameObject parentObject = parent;
			GameObject clone = (GameObject)Instantiate (Resources.Load ("ListButton"));


			clone.GetComponentInChildren<Text> ().text = button.GetComponentInChildren<Text> ().text;
			clone.transform.SetParent(parentObject.transform);

			RectTransform panelRectTransform = parent.GetComponent<RectTransform>();
			panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);

			UnityEngine.Events.UnityAction action = destroyButton (clone);
		
			clone.GetComponent<Button> ().onClick.AddListener (action);
		}
	}


	public void add_to_res () {
		if (CurrentState.current_button != null) {
			CloneButton (CurrentState.current_button, CurrentState.current_result_panel);
		}
	}
	void Start() {
		musicSlider = new MySlider(objects.slider_cs_musicSlider, objects.timer_cs_musicTimer, true);
		textSlider = new MySlider(objects.slider_cs_textSlider, objects.timer_cs_textTimer, true);
		DataBase.get_list_text ();
		DataBase.get_list_music ();
		DataBase.get_list_images ();
		create_list_text (CurrentState.DataTexts);
		create_list_music (CurrentState.DataMusic);
		create_list_images (CurrentState.DataImages);
		add_listeners ();

		objects.panel_cs_image.SetActive (false);
		objects.panel_cs_music.SetActive (false);
		objects.panel_cs_text.SetActive (true);
	}


	public void create_list_text (List<TextSound> arr) {
		this.buttonListFactory.create (arr.Cast<Item>().ToList(), objects.scroll_cs_text, (ButtonListFactory.CreateAction) delegate(GameObject button, Item elem) { 
			return changeSelectText (button, objects.scroll_cs_textResults, (TextSound) elem); 
		}, false);
	}
	public void create_list_music (List<MusicSound> arr) {
		this.buttonListFactory.create (arr.Cast<Item>().ToList(), objects.scroll_cs_music, (ButtonListFactory.CreateAction) delegate(GameObject button, Item elem) { 
			return changeSelectMusic (button, objects.scroll_cs_musicResults, (MusicSound) elem);
		}, false);
	}
	public void create_list_images (List<Img> arr) {
		this.buttonListFactory.create (arr.Cast<Item>().ToList(), objects.scroll_cs_images, (ButtonListFactory.CreateAction) delegate(GameObject button, Item elem) { 
			return changeSelectImage (button, (Img) elem); 
		}, true);
	}

}