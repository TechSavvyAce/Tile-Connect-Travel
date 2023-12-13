using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {
	public GameObject background;
	public Text loadingText;
	public Image fill;
	AsyncOperation async;

	public static LoadingScene loadingScreen;
	// Use this for initialization
	void Start () {
		loadingScreen = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (async != null) {
			loadingText.text = "Loading..." + (int)(async.progress*100) + "%";
			fill.fillAmount = async.progress;
		}
	}

	public void loadLevelMode(){
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_level))
			return;
		background.SetActive (true);
		int tutorial = Save.getTutorialStatus ();
		if (tutorial == 0) {
			// Save.setTutorial (1);
			Tutorial.TUTORIAL_MODE = 0;
			SceneManager.LoadScene ("Help");
		} else {
			async = SceneManager.LoadSceneAsync (StringUtils.scene_level);
		}
	}

	public void loadOnlineMenu(){
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_online_menu))
			return;
		#if ( UNITY_IOS )
		CustomDebug.Log("Version app current: " + GameConfig.APP_VERSION);
		if(GSM.CONFIG != null && GSM.CONFIG.GetInt(GSM.app_version_ios) > GameConfig.APP_VERSION){
			UpdateVersionController.ins.show("");
			return;
		}
		#endif
		#if ( UNITY_ANDROID )
		CustomDebug.Log("Version app current: " + GameConfig.APP_VERSION);
		//if(GSM.CONFIG.GetInt(GSM.app_version_android) > GameConfig.APP_VERSION){
		//UpdateVersionController.ins.show("");
		//return;
		//}
		#endif
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_online_menu);
	}


	public void loadMain(){
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_main))
			return;
		async = SceneManager.LoadSceneAsync (StringUtils.scene_main);
	}

	public void loadOnlineMode(){
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_on))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_on);
	}

	public void loadLevelGamplayNormal() {
		// ListLevel.CurrentLevel = GameStatic.currentLevel;
		GameStatic.loadNewGameRegular(0);
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_off))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_off);
        //GameStatic.mainMenu.hideBannerView();
    }

	public void loadLevelGamplayHard1() {
		GameStatic.loadNewGameRegular(1);
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_off))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_off);
		//GameStatic.mainMenu.hideBannerView ();
	}

	public void loadLevelGamplayHard2() {
		GameStatic.loadNewGameRegular(2);
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_off))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_off);
		//GameStatic.mainMenu.hideBannerView ();
	}

	public void loadLevelGamplayEvent() {
		GameStatic.loadNewGameEvent();
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_off))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_off);
		//GameStatic.mainMenu.hideBannerView ();
	}

	public void loadOldGame() {
		GameStatic.loadOldGameRegular();
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_off))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_off);
		//GameStatic.mainMenu.hideBannerView ();
	}

	public void loadOldGameEvent() {
		GameStatic.loadOldGameEvent();
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_play_off))
			return;
		background.SetActive (true);
		async = SceneManager.LoadSceneAsync (StringUtils.scene_play_off);
		//GameStatic.mainMenu.hideBannerView ();
	}
}
