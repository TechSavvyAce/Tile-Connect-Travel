using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class NumberIncrease : MonoBehaviour {
	public Text text;
	float valueTo;
	public float value;
	public int countJump = 20;
	public float timeJump = 0.2f;
	float deltaJump;
	float currentTime;
	Action action_done;

	void Start () {
		text = GetComponent<Text> ();
	}

	public int getValue(){
		value = int.Parse (text.text);
		return (int)value;
	}

	public void changeTo(float valueEnd,Action done = null){
		value = int.Parse (text.text);
		float delta = valueEnd - value;
		valueTo = valueEnd;
		deltaJump = delta / countJump;
		if (done != null)
			action_done = done;
	}
	
	void Update () {
		if (valueTo != value) {
			currentTime += Time.deltaTime;
			if (currentTime > Mathf.Abs (timeJump)) {
				currentTime = currentTime - Mathf.Abs (timeJump);
				if (Mathf.Abs (value - valueTo) <= Mathf.Abs (deltaJump)) {
					value = valueTo;
					if (action_done != null)
						action_done ();
				} else {
					value += deltaJump;
				}
				text.text = (int)value + "";
			}
		}
	}
}
