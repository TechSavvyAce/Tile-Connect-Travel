using UnityEngine;
using System.Collections;

public class TabHeader : MonoBehaviour {
	public bool active = false;
	public GameObject objActive;
	public GameObject objInActive;
	void Start(){
		setActive (active);
	}

	public void setActive(bool isActive){
		active = isActive;
		objActive.SetActive (active);
		objInActive.SetActive (!active);
	}
}
