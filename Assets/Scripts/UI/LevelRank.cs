using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelRank : Popup {
	public static LevelRank ins;
	public Text level;
	public LoadingScene loading;
	public GameObject background;
	void Start () {
		//base.init ();
		ins = this;
	}

	public new void showPopup(){
		showWithoutDownload ();
	}

	public void showWithoutDownload(){
		base.showPopup ();
		level.text = "Level " + ListLevel.CurrentLevel;
		if (background != null) {
			background.SetActive (true);
		}
	}

	public new void  hidePopup(){
		base.hidePopup ();
		if (background != null) {
			background.SetActive (false);
		}
	}

	void Update () {
	
	}
	public void play(){
		loading.loadLevelGamplayNormal ();
		SoundSystem.ins.play_music_off ();
	}
}
