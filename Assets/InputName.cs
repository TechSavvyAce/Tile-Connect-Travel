using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InputName : MonoBehaviour {
	public InputField userName;

	void Start(){
		userName = GetComponentInChildren<InputField> ();
	}

	public void loadLastName(){
	}

	public void updateName(){
		if (userName.text != "") {
			gameObject.SetActive (false);
		} else {
			ToastManager.showToast (StringUtils.error);
		}
	}

	public void hide(){
		gameObject.SetActive (false);
	}

	public void show(){
		if (this == null)
			return;
		gameObject.SetActive (true);
	}
}
