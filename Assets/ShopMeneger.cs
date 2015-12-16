using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;




public class ShopMeneger : MonoBehaviour {

	public Objects objects;
	public Images images;
	public ButtonListFactory buttonListFactory;

	private MySlider slider = null;

	private GameObject current_button = null;

	void Start() {
		slider = new MySlider(objects.slider_shop_slider, objects.timer_shop_timer, false);
		objects.panel_shop_music.SetActive (false);
		create_list_text (CurrentState.DataTexts);
		create_list_music (CurrentState.DataMusic);
		tool_controller(false, false, false);
		objects.panel_shop_background.SetActive (false);
	}
	public void resetSlider() {
		foreach (Transform child in objects.scroll_shop_text.transform) {
			child.GetComponent<Image>().sprite = images.listButton;
		}
		foreach (Transform child in objects.scroll_shop_music.transform) {
			child.GetComponent<Image>().sprite = images.listButton;
		}
		slider.Stop();
		current_button = null;
	}	

	public void pauseSlider() {
		slider.Pause ();
	}	

	UnityEngine.Events.UnityAction changeSelectButton(GameObject button, Item ts, GameObject parent) {
		return () => {
			bool buy = ts.Buy;
			bool load = ts.Load;
			string content = ts.Content;
			foreach (Transform child in parent.transform) {
				child.GetComponent<Image>().sprite = images.listButton;
			}
			if(current_button != button && button != null){
				current_button = button;
				slider.setCurrentSound(content);
				slider.Play();
				button.GetComponent<Image>().sprite = images.selected_listButton;
				tool_controller(!buy, !load && buy, load);
				tool_controller(!buy, !load && buy, load);
				tool_controller(!buy, !load && buy, load);
			}else{
				slider.Stop();
				current_button = null;
				tool_controller(false, false, false);
			}
			
		};
	}
	
	public void tool_controller(bool buy, bool download, bool delete) {
		enable_button (objects.button_shop_download, objects.image_shop_download, download);		
		enable_button (objects.button_shop_buy, objects.image_shop_buy, buy);		
		enable_button (objects.button_shop_delete, objects.image_shop_delete, delete);		
	}

	public void enable_button(Button button, Image image, bool enabled) {
		if (enabled) {
			button.enabled = true;
			image.color = new Color32(255, 255, 255, 255);		
		} else {
			button.enabled = false;
			image.color = new Color32(170, 170, 170, 255);
		}

	}

	public void create_list_text (List<TextSound> arr) {
		this.buttonListFactory.create (arr.Cast<Item>().ToList(), objects.scroll_shop_text, (ButtonListFactory.CreateAction) delegate(GameObject button, Item elem) { 
			return changeSelectButton (button, elem, objects.scroll_shop_text); 
		}, true);
	}
	public void create_list_music (List<MusicSound> arr) {
		this.buttonListFactory.create (arr.Cast<Item>().ToList(), objects.scroll_shop_music, (ButtonListFactory.CreateAction) delegate(GameObject button, Item elem) { 
			return changeSelectButton (button, elem, objects.scroll_shop_music);
		}, true);
	}

}
