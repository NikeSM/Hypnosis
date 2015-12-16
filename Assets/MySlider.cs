using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

public class MySlider {
	public  Slider  slider;
	public  Text  timer;
	public  AudioSource  audioSource = null;
	public  string current_sound = "";
	public  bool isClick = false;
	


	public MySlider (GameObject s, GameObject t, bool click) {
		slider = s.GetComponent<Slider> ();
		timer = t.GetComponent<Text> ();
		audioSource = s.GetComponent<AudioSource> ();
		isClick = click;
		
		slider.onValueChanged.AddListener(changedSliderValue);
	}
	
	

	public  void setCurrentSound(string ts){
		audioSource.timeSamples = 0;
		timer.text = "0:00";
		current_sound = ts;
		slider.value = 0;
		audioSource.clip = (AudioClip) Resources.Load(ts);
	}

	public  void Play(){
		audioSource.Play();
	}
	public  void Pause(){
		audioSource.Pause();
	}
	public  void Stop(){

			timer.text = "0:00";
			audioSource.Stop();
			audioSource.clip = null;
			slider.value = 0;
			current_sound = null;
	}
	public  bool isPlaying(){
		if (audioSource != null) {
			return audioSource.isPlaying;
		} else {
			return false;
		}

	}
	public bool isEnd() {
		return 1 - slider.value < 0.005;
	}
	public  void refreshSlider(){
		if (isPlaying ()) {
			slider.value = audioSource.timeSamples / audioSource.clip.length / audioSource.clip.frequency;
			int time = audioSource.timeSamples / audioSource.clip.frequency;
			string sec = "0";
			if (time % 60 < 10) {
				sec = sec + (time % 60).ToString ();
			} else {
				sec = (time % 60).ToString ();
			}
			timer.text = (time / 60).ToString () + ":" + sec;

		} else {

			if(audioSource != null && audioSource.clip != null){
				if(audioSource.timeSamples == 0){

					setCurrentSound(current_sound);
				}
			}
		}
	}
	public  void changedSliderValue(float newValue) {

		if(isClick){
			if (audioSource != null && audioSource.clip != null && newValue > 0 && 1 - newValue > 0.001) {
				audioSource.timeSamples = (int)System.Math.Round (newValue * audioSource.clip.length * audioSource.clip.frequency);
				audioSource.Play ();
			} else {
				if(1 - newValue < 0.001){

					setCurrentSound(current_sound);
				}
			}

		}
	}

	public void mute(){
		audioSource.mute = true;
	}
	
	public void unmute(){
		audioSource.mute = false;
	}
	public bool isMute() {
		return audioSource.mute;
	}
	public void changeVolume(float volume) {
		audioSource.volume = volume;

	}

}