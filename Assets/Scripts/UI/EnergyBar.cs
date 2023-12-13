using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnergyBar : MonoBehaviour {
	float totalTime = GameConfig.time_energy_recover;
	float currentTime = 0;
	bool isRun = false;
	Image fillImage;
	public ItemController itemController;
	public Text txtTime;

	void Start () {
		totalTime = GameConfig.time_energy_recover;
		fillImage = GetComponent<Image> ();
		checkToStartOffline ();
		updateItem ();
	}

	public void checkToStartOffline(){
		if(ItemController.getNumEnergyItem() < GameConfig.max_energy_offline)
			startBar ();
	}

	public float updateItemOffline(){
		long startTime =(long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		long lastTime = (long)PlayerPrefs.GetFloat (Save.time_energy,startTime);
		long deltaTime = startTime - lastTime;
		int numItem = (int)(deltaTime / GameConfig.time_energy_recover);
		long timeOver = (deltaTime - GameConfig.time_energy_recover * numItem);
		if (ItemController.getNumEnergyItem () + numItem >= GameConfig.max_energy_offline) {
			if (ItemController.getNumEnergyItem () < GameConfig.max_energy_offline) {
				Save.setEnergyItem (GameConfig.max_energy_offline);
			}
		} else {
			Save.setEnergyItem (ItemController.getNumEnergyItem () + numItem);
		}
		PlayerPrefs.SetFloat (Save.time_energy, startTime - timeOver);
		PlayerPrefs.Save ();
		if (ItemController.getNumEnergyItem() < GameConfig.max_energy_offline) {
			startBar ();
		}
		itemController.updateItemUI ();
		return timeOver;
	}

	float getOfflineTimeOver(){
		long startTime =(long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		long lastTime =(long) PlayerPrefs.GetFloat (Save.time_energy,startTime);
		long deltaTime = startTime - lastTime;
		long numItem = (long)(deltaTime / GameConfig.time_energy_recover);
		long timeOver = (deltaTime - GameConfig.time_energy_recover * numItem);
		return timeOver;
	}

	Coroutine corUpdateItem;
	public void updateItem(){
		currentTime = getOfflineTimeOver ();
		if (corUpdateItem != null)
			StopCoroutine (corUpdateItem);
		//checkNetworkNow (hasNetword =>{
		//	if(!hasNetword){
		//		updateItemOffline();
		//	}
		//});
	}


	void Update () {
		if (isRun) {
			currentTime += Time.smoothDeltaTime;
			fillImage.fillAmount = currentTime/totalTime;
			txtTime.text = CommonFunction.getTimeFromSecond ((int)(totalTime- currentTime));
			if(currentTime >= totalTime){
				timeOut();
			}
		}
	}

	public void startBar(){
		isRun = true;
		if(txtTime != null)
			txtTime.gameObject.SetActive (true);
	}

	public void pause(){
		isRun = false;
	}

	public void continueRun(){
		isRun = true;
	}

	public long getTimeRun(){
		return (long) (totalTime - currentTime);
	}

	void timeOut(){
		txtTime.gameObject.SetActive (false);
		pause ();
		updateItem ();
		itemController.updateItemUI ();
	}

	// void OnApplicationPause(bool pauseStatus) {
	// 	if (!pauseStatus)
	// 		updateItem ();
	// }

	//void checkNetworkNow(Action<bool> callback){
	//	StartCoroutine (checkNetworkConnectionNow(callback));
	//}

	//IEnumerator checkNetworkConnectionNow(Action<bool> callback) {
	//	WWW www = new WWW (GameConfig.network_ping_url);
	//	yield return www;
	//	if (www.error != null) {
	//		callback (false);
	//		CustomDebug.Log ("NO NETWORK CONNECTION");
	//	} 
	//	callback (true);
	//}
}
