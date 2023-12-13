
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SaveInfoAttribute : MonoBehaviour {
	public enum user_type {number,str};
	public user_type type = user_type.number;
	public string attribueName = "";
	int updateVersion = 0;
	public static int version = 1;
	Text text; 
	void Start () {
		text = GetComponent<Text> ();
	}

	public void updateInfo(){
		string info = "";
		if (PlayerPrefs.HasKey (attribueName)) {
			if (type == user_type.number)
				info = PlayerPrefs.GetInt (attribueName)+"";
			if(type == user_type.str)
				info = PlayerPrefs.GetString (attribueName);
			if (info != null && !info.Equals ("") && !info.Equals (null))
				text.text = info;
			Debug.Log ("Save attr " + attribueName + info);
		}
	}

	void Update () {
		if (updateVersion < version) {
			updateVersion = version;
			updateInfo ();
		}
	}
}
