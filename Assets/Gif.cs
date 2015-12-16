using UnityEngine;
using UnityEngine.UI;
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq.Expressions;


public class Gif : MonoBehaviour {
	private Img current_gif = null;
	private Image current_place = null;
	private bool isPlay = false;

	public Images objects;

	private int current = 1;
	private float timeLimit = 0; 

	
	void Update() {
		if (isPlay && current_gif != null && current_place != null) {
			if (timeLimit > 0) {	
				timeLimit -= Time.deltaTime;
			} else {
				timeLimit = current_gif.Delta;
				changeImage ();
			}
		}
	}
	
	public void changeImage(){
		current_place.sprite = Resources.Load<Sprite> (current_gif.Content + " (" + current + ")" );
		current++;
		if (current > current_gif.Size) {
			current = 1;
		}
	}

	public void start (Image cp, Img cg) {
		stop ();
		current_gif = cg;
		current_place = cp;
		isPlay = true;
		current = 1;
		timeLimit = current_gif.Delta;
	}

	public void stop () {
		if (current_place != null) {
			current_place.sprite = objects.main_background;
		}
		current = 1;
		current_gif = null;
		current_place = null;
		isPlay = false;
		timeLimit = 0;
	}
}
