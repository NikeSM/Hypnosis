using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class PlayManager : MonoBehaviour{


	public Session currentSession = null;

	public Objects objects;
	public Images images;
	public Gif gif;


	private MySlider  ts = null;
	private MySlider  ms = null;
	private Slider vs = null;
	private int currentMusic = -1;
	private int currentText = -1;
	

	void Start() {
		ts = new MySlider(objects.slider_main_textSlider, objects.timer_main_textTimer, false);
		ms = new MySlider(objects.slider_main_musicSlider, objects.timer_main_musicTimer, false);
		vs = objects.slider_main_volumeSlider;
		objects.panel_cs_background.SetActive(true);

		objects.panel_ps_background.SetActive (false);
		objects.panel_main_background.SetActive (false);
		objects.panel_shop_background.SetActive (false);
		objects.panel_load_background.SetActive (false);
		objects.panel_opt_background.SetActive (false);
		objects.panel_save_background.SetActive (false);
		objects.panel_menu_background.SetActive (false);
	}

	void Update() {
		ts.refreshSlider ();
		ms.refreshSlider ();
		if (CurrentState.new_session) {
			init ();
		}
		
		if (ms.isEnd()) {
			nextMusic();
		}
		if (ts.isEnd()) {
			nextText();
		}
	}

	public void onPlayClick() {
		if (ts.isPlaying() || ms.isPlaying()) {
			ts.Pause();
			ms.Pause();
			objects.button_main_play.sprite = images.playButton;
		} else {
			ts.Play();
			ms.Play();
			objects.button_main_play.sprite = images.pauseButton;
		}
	}


	private void mute_text(){
		ts.mute ();
		objects.button_main_muteText.sprite = images.muteButton;
	}
	private void mute_music(){
		ms.mute ();
		objects.button_main_muteMusic.sprite = images.muteButton;
	}

	private void unmute_text(){
		ts.unmute ();
		objects.button_main_muteText.sprite = images.unmuteButton;
	}
	private void unmute_music(){
		ms.unmute ();
		objects.button_main_muteMusic.sprite = images.unmuteButton;
	}

	public void music_mute_click(){
		if (ms.isMute() == false) {
			mute_music();
		} else {
			unmute_music();
		}
	}

	public void text_mute_click(){
		if (ts.isMute() == false) {
			mute_text();
		} else {
			unmute_text();
		}
	}

	public void init() {
		currentSession = CurrentState.current_session;
		if (currentSession != null) {
			setText (0);
			setMusic (0);
			CurrentState.new_session = false;
		}

	}

	
	public void setMusic(int i) {
		currentMusic = i;
		ms.setCurrentSound (currentSession.getMusic [i].Content);
		ms.Play ();
		objects.button_main_play.sprite = images.pauseButton;
		int j = 0;
		foreach (Transform child in objects.scroll_main_music.transform) {
			if(j != i){
				child.GetComponent<Image>().sprite = images.listButton;
			}else{
				child.GetComponent<Image>().sprite = images.selected_listButton;
			}
			j++;
		}
	}

	public void setText(int i) {
		currentText = i;
		ts.setCurrentSound (currentSession.getTexts [i].Content);
		ts.Play ();
		objects.button_main_play.sprite = images.pauseButton;
		int j = 0;
		foreach (Transform child in objects.scroll_main_text.transform) {

			if(j != i){
				child.GetComponent<Image>().sprite = images.listButton;

			}else{
				child.GetComponent<Image>().sprite = images.selected_listButton;
			}
			j++;
		}
	}

	public void nextMusic() {
		if (currentSession != null) {
			if (currentSession.getMusic.Count > currentMusic + 1) {
				setMusic (currentMusic + 1);
			} else {
				setMusic (0);
			}
		}

	}
	
	public void replay(){
		setText (0);
		setMusic (0);
	}

	public void nextText() {
		if (currentSession != null) {
			if (currentSession.getTexts.Count > currentText + 1) {
				setText (currentText + 1);
			}
		}
	}





	public void changedVolumeSlider() {
		float newValue = vs.value;
		if (newValue > 1) {
			ms.changeVolume(1);
			ts.changeVolume(2 - newValue);
		} else {
			ms.changeVolume(newValue);
			ts.changeVolume(1);
		}
	}
	
	public void onFullButtonClick () {
		Image img_front = objects.panel_ps_presentation;
		CurrentState.demo_panel = img_front;
		gif.start(CurrentState.demo_panel, currentSession.getImage);
		objects.panel_ps_background.SetActive (true);
	}



	public void onSmallButonClick(){
		CurrentState.demo_panel = null;
		objects.panel_ps_background.SetActive (false);
	}
}