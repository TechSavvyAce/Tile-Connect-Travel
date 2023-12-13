using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class MainMenu : MonoBehaviour {
	// public Button btnContinue;
	public GameObject btnContinue;
	// public GameObject btnDebugPostScore;
	// public GameObject btnDebugResetHighScore;
	public GameObject chooseModePopup;

	public GameObject backMap;
	public GameObject backAsia;
	public GameObject backEurope;
	public GameObject backAmerica;
	public GameObject backAustralia;
	public GameObject backAfrica;
	//public GameObject btnEvent;
	//public TimeCountDown timeEventRamain;

	// public BannerView bannerViewLeft;
	// public BannerView bannerViewRight;

	bool isShowChooseModeDialog = false;
	float currentTime;
	//	float timeReloadRequestBanner = 10;
	//	string admob_id_2003 = "ca-app-pub-3940256099942544/6300978111";
	//	string admob_id_2017 = "ca-app-pub-3940256099942544/6300978111";
	void Start(){
//		Save.setTutorial(0);
		// Save.setTutorialStore(0);
		btnContinue.SetActive(Save.canContinue());
		// btnDebugPostScore.SetActive (GameConfig.DEBUG_KEY);
		// btnDebugResetHighScore.SetActive (GameConfig.DEBUG_KEY);
		// updateButtonEventUI ();

		// Attach click events to buttons
	}
	void Update() {
		if (Save.getTutorialStatus() == 0) {
			Tutorial.TUTORIAL_MODE = 0;
			SceneManager.LoadScene ("Help");
			//AdsManager.ins.hideBannerView ();

		}
	}
	public void StartOfflineMode(){
		SceneManager.LoadScene ("LevelSence");
	}
	public void StartOnlineMode(){
		SceneManager.LoadScene ("OnlinePlay");
	}
	public void StartTutorial() {
		Tutorial.TUTORIAL_MODE = 0;
		SceneManager.LoadScene ("Help");
	}

	public void QuitGame()
    {
#if !UNITY_IOS
        if (Save.getRateStatus() == Const.STATUS_RATE_REMIND && Save.canShowRate())
        {
            GameStatic.ratePopup.showPopup();
        }
        else
        {
            GameStatic.handleQuitApp();
        }
#endif
    }

    //public void showLeaderBoard() {
    //	if (GameStatic.IsConnectedGooglePlayService) {
    //		CustomDebug.Log ("show LeaderBoard");
    //		Social.ShowLeaderboardUI ();
    //	} else {
    //		ToastManager.showToast ("Unable connect to google play service, will connect again");
    //		GameStatic.ConnectToGooglePlayService ();
    //	}
    //}

    public void showChooseModePopup(string country_key) {
		chooseModePopup.transform.parent.gameObject.SetActive(true);
		chooseModePopup.GetComponent<Animator>().Play("showpause");
		Save.setCountryKey(country_key);
	}

	public void showMapBack()
    {
		backMap.SetActive(true);
		backAsia.SetActive(false);
		backAfrica.SetActive(false);
		backAmerica.SetActive(false);
		backAustralia.SetActive(false);
		backEurope.SetActive(false);
	}

	public void showAsia()
    {
		backMap.SetActive(false);
		backAsia.SetActive(true);
		backAfrica.SetActive(false);
		backAmerica.SetActive(false);
		backAustralia.SetActive(false);
		backEurope.SetActive(false);
	}

	public void showAfrica()
	{
		backMap.SetActive(false);
		backAsia.SetActive(false);
		backAfrica.SetActive(true);
		backAmerica.SetActive(false);
		backAustralia.SetActive(false);
		backEurope.SetActive(false);
	}

	public void showAmerica()
	{
		backMap.SetActive(false);
		backAsia.SetActive(false);
		backAfrica.SetActive(false);
		backAmerica.SetActive(true);
		backAustralia.SetActive(false);
		backEurope.SetActive(false);
	}

	public void showAustralia()
	{
		backMap.SetActive(false);
		backAsia.SetActive(false);
		backAfrica.SetActive(false);
		backAmerica.SetActive(false);
		backAustralia.SetActive(true);
		backEurope.SetActive(false);
	}

	public void showEurope()
	{
		backMap.SetActive(false);
		backAsia.SetActive(false);
		backAfrica.SetActive(false);
		backAmerica.SetActive(false);
		backAustralia.SetActive(false);
		backEurope.SetActive(true);
	}

	public void hideChoo2seModePopup() {
        Invoke("hideParent", 0.2f);
        chooseModePopup.GetComponent<Animator>().Play("hidepause");
	}

    void hideParent()
    {
        chooseModePopup.transform.parent.gameObject.SetActive(false);
    }

    //public void hideBannerView() {
    //    AdsManager.ins.hideBannerView();
    //}

	public void endAnimaMain() {
		//AdsManager.ins.showBannerView();
		updateButtonEventUI();
		//GameStatic.networkConnectionStatus.checkNetwork (result => {
		//	if (result) {
		//		int rand = UnityEngine.Random.Range(0, 100);
		//		if (rand < GameConfig.probability_ads_visible) {
		//			GameStatic.adsPopup.loadAds ();	
		//		}
		//	}	
		//});
	}

	public void updateButtonEventUI() {
		//bool hasEvent = GSM.instance.eventData == null ? false : true;
		//btnEvent.SetActive (hasEvent);
		//if (hasEvent) {
		//	int status = (int) GSM.instance.eventData.GetInt("status");
		//	string eventName = (string) GSM.instance.eventData.GetString("event_name");
		//	if (status == EventInfo.STATUS_PREPARE) {
		//		timeEventRamain.setText("Preparing Time");
		//	} else if (status == EventInfo.STATUS_ONGOING) {
		//		timeEventRamain.StartRun ((int)(GSM.instance.eventData.GetLong ("time") / 1000),done=>{
		//			GSM.instance.getEventData();
		//		});
		//	} else {
		//		timeEventRamain.setText("Event ended");
		//	}
		//}
		// btnContinueEvent.SetActive (hasEvent && Save.canContinueEvent ());
	}

	public void showEventMainUI() {
		//GameStatic.eventArea.show ();
	}

    public void showMoreApp()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Mini+Games+House");
#elif UNITY_IOS
#endif
    }
}
