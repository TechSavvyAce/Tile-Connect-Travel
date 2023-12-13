using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RatePopup : Popup {

	// Use this for initialization
	void Start () {
		//base.init ();
	}

	public void RateNow() {
		Save.setRateStatus (Const.STATUS_RATED);
		hidePopup ();
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_main)) {
			GameStatic.handleQuitApp();
		}
		GameStatic.openStore ();
	}

	public void NotRate() {
		Save.setRateStatus (Const.STATUS_NOT_RATE);
		hidePopup ();
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_main)) {
			GameStatic.handleQuitApp();
		}
	}

	public void RemindLater() {
		Save.setRateStatus (Const.STATUS_RATE_REMIND);
		hidePopup ();
		if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_main)) {
			GameStatic.handleQuitApp();
		}
	}
}
