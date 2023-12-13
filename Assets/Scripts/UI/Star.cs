using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Star : MonoBehaviour {
	public Sprite on;
	public Sprite off;
	
	public void SetActive(bool active){
		if (active)
			GetComponent<Image> ().sprite = on;
		else
			GetComponent<Image> ().sprite = off;
	}
}
