using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeTextBar : MonoBehaviour {
	Text text;
	float time = 0;
	bool _isRunning = false;
	// Use this for initialization
	void Start () {
		time = 0;
		StartRun ();
	}
	
	public void StartRun(){
		this.time = 0;
		_isRunning = true;
		updateInfo ();
	}

	public void Stop(){
		_isRunning = false;
	}

	public int GetTime(){
		return (int)time;
	}

	void Update(){
		if(_isRunning)
		time += Time.deltaTime;
		updateInfo ();
	}

	void updateInfo(){
		text = GetComponent<Text> ();
		text.text = (int)time+"";
	}
}
