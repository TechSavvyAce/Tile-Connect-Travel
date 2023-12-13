using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class UserInfoAttribue : MonoBehaviour {
	public enum user_type {number,str};
	public user_type type = user_type.number;
	public string attribueName = "";
	int updateVersion = 0;
	public static int version = 0;
	Text text; 
	void Start () {
		text = GetComponent<Text> ();
	}

	public void updateInfo(){
		string info = "";
		//if (GSM.user.ContainsKey (attribueName)) {
		//	object data = GSM.user.BaseData [attribueName];
		//	if (type == user_type.number)
		//		info = Convert.ToInt32 (data)+"";
		//	if(type == user_type.str)
		//		info =GSM.user.BaseData [attribueName] as string;
		//	if (info != null && !info.Equals ("") && !info.Equals (null))
		//		text.text = info;
		//}
	}
	// Update is called once per frame
	void Update () {
		if (updateVersion < version) {
			updateVersion = version;
			updateInfo ();
		}
	}
}
