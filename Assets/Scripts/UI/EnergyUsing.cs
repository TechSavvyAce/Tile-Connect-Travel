using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyUsing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (ItemController.getNumEnergyItem () <= 0) {
				GetComponent<Button> ().interactable = false;
		} else {
				GetComponent<Button> ().interactable = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
