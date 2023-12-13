using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class TimeCountDown : MonoBehaviour {
	public Text text;
	public bool isTimeFormat = false;
	public string prefixTxt="";
	public string endTxt="";
	public float time = 0;
	bool _isRunning = false;
	Action<bool> callback;

	public float updateInterval = 1F;
	private float lastInterval;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		if(time > 0) StartRun((int)time,null);
	}

	public void StartRun(int cdTime,Action<bool> callback){
		this.time = cdTime;
		lastInterval = Time.realtimeSinceStartup;
		_isRunning = true;
		text = GetComponent<Text> ();
		updateInfo ();
		this.callback = callback;
	}

	public void setText(string text){
		this.text = GetComponent<Text> ();
		this.text.text = text;
	}

	public void Stop(){
		_isRunning = false;
	}

	void end(){
		if(callback != null) callback(true);
	}

	public int GetTime(){
		return (int)time;
	}

	void Update(){
		if(_isRunning && time < 0){
			Stop();
			end();
		}
		if(_isRunning){
			float timeNow = Time.realtimeSinceStartup;
			if (timeNow > lastInterval + updateInterval) {
				time -= timeNow - lastInterval;
				updateInfo ();
				lastInterval = timeNow;
			}

		}
	}

	void updateInfo(){
		text.text =prefixTxt + (isTimeFormat ? CommonFunction.getTimeFromSecond((int)time) : (int) time+"")+ endTxt;
	}
}
