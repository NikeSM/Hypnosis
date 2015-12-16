using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonListFactory : MonoBehaviour {
	public delegate UnityEngine.Events.UnityAction CreateAction(GameObject button, Item elem);
	public void create(List<Item> arr, GameObject parentObject, CreateAction actionFactory, bool condition) {
		GameObject button;
		for(int i = 0; i < arr.Count; i++) {
			if (condition || arr[i].Buy && arr[i].Load) {
				button = (GameObject) Instantiate(Resources.Load("ListButton"));
				button.GetComponentInChildren<Text>().text = arr[i].Name;
				button.transform.SetParent(parentObject.transform);

				RectTransform panelRectTransform = parentObject.GetComponent<RectTransform>();
				panelRectTransform.sizeDelta = new Vector2( panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + CurrentState.button_size);

				button.GetComponent<Button>().onClick.AddListener(actionFactory (button, arr[i]));
			}
		}
	}
}
