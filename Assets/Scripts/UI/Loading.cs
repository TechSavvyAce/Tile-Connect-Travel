using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Loading : MonoBehaviour {
	static float TIME_OUT_DEF = 20;
	static float TIME_OUT = 20;
	static float _time;
	public Text text;
	public static GameObject loading;
	public GameObject _loading;
	static Action<bool> callback;
	// Use this for initialization
	void Start () {
		loading = _loading;
	}

	void Update(){
		if (isLoading()) {
			_time += Time.deltaTime;
			if(_time> TIME_OUT){
				if (callback != null) {
					callback (true);
					callback = null;
				}
				Loading.hideLoading();
			}
		}
	}
	
	public void setText(string text){
		this.text.text = text;
	}
	public static void showLoading(string text){
		TIME_OUT = TIME_OUT_DEF;
		_show_loading(text);
	}
	public static void showLoading(string text,float time){
		TIME_OUT = time;
		_show_loading(text);
	}

	static void _show_loading(string text){
		if (loading == null) {
			loading = GameObject.Find("Loading");
		}
		if (loading == null) {
			CustomDebug.Log ("Can not find loading");
			return;
		}
		loading.transform.Find("Active").gameObject.SetActive (true);
		loading.GetComponent<Loading> ().setText (text);
		_time = 0;
	}

	public static void showLoading(string text,Action<bool> call){
		TIME_OUT = TIME_OUT_DEF;
		callback = call;
		_show_loading (text);
	}
	public static void showLoading(string text,float time,Action<bool> call){
		TIME_OUT = time;
		callback = call;
		_show_loading (text);
	}

	public static void hideLoading(){
		if(loading != null)
		loading.transform.Find("Active").gameObject.SetActive (false);
		callback = null;
	}
	public static bool isLoading(){
		return loading != null && loading.transform.Find("Active").gameObject.activeSelf;
	}
}
