using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RedirectController : MonoBehaviour {
	public static string REDIRECT_BY_EVENT = "none";

	// Update is called once per frame
	void Update () {
		redirect ();
	}

	private void redirect() {
		if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_STORE)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_MESSAGE)) {
			LoadingScene.loadingScreen.loadMain ();
			//if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_main)) {
			//	MessageView.ins.show ();			
			//	MessageView.ins.reload ();			
			//} else {
			//	MessageView.showNewMessage = false;
			//}
//			StartCoroutine (openMessageView ());
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_EVENT)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
			if (SceneManager.GetActiveScene ().name.Equals (StringUtils.scene_online_menu)) {
			} else {
				LoadingScene.loadingScreen.loadOnlineMenu ();
				PlayerPrefs.SetInt ("isEvent", 1);
				PlayerPrefs.Save ();
			}
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_EVENT_REWARD)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_STORE_ITEM)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_STORE_UI)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_STORE_COIN)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		} else if (REDIRECT_BY_EVENT.Equals(Const.REDIRECT_OPEN_STORE_OTHER)) {
			REDIRECT_BY_EVENT = Const.REDIRECT_NONE;
		}
	}

	//IEnumerator openMessageView(){
	//	while (MessageView.ins == null && !SceneManager.GetActiveScene().name.Equals(StringUtils.scene_main) && MessageView.ins.gameObject == null)
	//		yield return new WaitForFixedUpdate ();
	//	yield return new WaitForFixedUpdate ();
	//	MessageView.ins.show ();
	//}
}
