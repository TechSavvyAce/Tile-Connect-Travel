using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class ItemController : MonoBehaviour {
	public enum ItemType {hint,random,energy};
	public ItemType itemType;
	public static string item_key {
		get{
			if (GameStatic.currentMode == Const.GAME_MODE_EVENT) {
				return "num_item_type_event_";
			} else {
				return "num_item_type_";
			}
		}
		set{
			item_key = value;
		}
	}
	[Serializable]
	public class EventUseEnergy : UnityEvent { }

	public static int updateVersion = 0;
	public int _updateVersion = 0;

	public EventUseEnergy OnEventUseEnergy;
	void Start () {
		updateItemUI ();
	}

	public static int getNumHintItem(){
		if (!PlayerPrefs.HasKey (item_key + ItemType.hint.GetHashCode ())) {
			PlayerPrefs.SetInt (item_key + ItemType.hint.GetHashCode (), GameConfig.num_item_hint_start);
			PlayerPrefs.Save ();
			change ();
		}
		return PlayerPrefs.GetInt (item_key + ItemType.hint.GetHashCode());
	}

	public static int getNumRandomItem(){
//		PlayerPrefs.SetInt (item_key + ItemType.random.GetHashCode (),1);
		if (!PlayerPrefs.HasKey (item_key + ItemType.random.GetHashCode ())) {
			PlayerPrefs.SetInt (item_key + ItemType.random.GetHashCode (), GameConfig.num_item_random_start);
			PlayerPrefs.Save ();
			change ();
		}
		return PlayerPrefs.GetInt (item_key + ItemType.random.GetHashCode());
	}

	public static int getNumEnergyItem(){
		if (!PlayerPrefs.HasKey (item_key + ItemType.energy.GetHashCode ())) {
			PlayerPrefs.SetInt (item_key + ItemType.energy.GetHashCode (), GameConfig.num_item_energy_start);
			PlayerPrefs.Save ();
			change ();
		}
		int num = PlayerPrefs.GetInt (item_key + ItemType.energy.GetHashCode ());
		return num > 0 ? num : 0 ;
	}

	public static void setHintItem(int number){
		GameStatic.currentHint = number;
		PlayerPrefs.SetInt (item_key + ItemType.hint.GetHashCode (), number);
		PlayerPrefs.Save ();
		change ();
	}

	public static void addHintItem(int number) {
		PlayerPrefs.SetInt (item_key + ItemType.hint.GetHashCode (), getNumHintItem() + number);
		PlayerPrefs.Save ();
		change ();
	}

	public static void setEnergyItem(int number){
		if (number < 0)
			number = 0;
		PlayerPrefs.SetInt (item_key + ItemType.energy.GetHashCode (), number);
		PlayerPrefs.Save ();
		change ();
	}

	public static bool addEnergyItem(){
		int num = getNumEnergyItem ();
		if (num < GameConfig.max_energy_offline) {
			PlayerPrefs.SetInt (item_key + ItemType.energy.GetHashCode (), num+1);
			PlayerPrefs.Save ();
			return true;
		}
		return false;
	}

	public static void addEnergyItem(int number){
		PlayerPrefs.SetInt (item_key + ItemType.energy.GetHashCode (), getNumEnergyItem() + number);
		PlayerPrefs.Save ();
		change ();
	}

	public bool useHintItem(){
		if (getNumHintItem () <= 0) {
			ToastManager.showToast (StringUtils.not_enough_item);
			return false;
		}
		setHintItem (getNumHintItem () - 1);
		return true;
	}

	public bool useRandomItem(){
		if (getNumRandomItem () <= 0) {
			ToastManager.showToast (StringUtils.not_enough_item);
			return false;
		}
		setRandomItem (getNumRandomItem () - 1);
		return true;
	}

	public bool useEnergyItem(){
		if(OnEventUseEnergy != null) OnEventUseEnergy.Invoke ();
		return USEEnergyItem ();
	}

	public static bool USEEnergyItem(){
		int num = getNumEnergyItem ();
		if (num <= 0) {
			ToastManager.showToast (StringUtils.not_enough_energy);
			return false;
		}
		updateTimeEnergy = true;

		Save.setEnergyItem (num- 1);
		if(GameStatic.energyBar!= null)
			GameStatic.energyBar.checkToStartOffline ();
		return true;
	}

	//void updateItemEnergyOnline(){
	//	GSRequestData data = new GSRequestData();
	//	data.Add ("get_server_time", true);
	//	GSM.sendUserRequest (data, res => {
	//		if(res == null){
	//			PlayerPrefs.SetInt (Save.time_energy,(int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
	//			return;
	//		}

	//		int startTime =(int) res.GetLong("data")/1000;
	//		PlayerPrefs.SetInt (Save.time_energy_server, startTime);
	//		startTime =(int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
	//		PlayerPrefs.SetInt (Save.time_energy, startTime);
	//		PlayerPrefs.Save ();
	//	});
	//}

	public static void setRandomItem(int number){
		GameStatic.currentRandom = number;
		PlayerPrefs.SetInt (item_key + ItemType.random.GetHashCode (), number);
		PlayerPrefs.Save ();
		change ();
	}

	public static void addRandomItem (int number) {
		PlayerPrefs.SetInt (item_key + ItemType.random.GetHashCode (),getNumRandomItem() + number);
		PlayerPrefs.Save ();
		change ();
	}



	public void updateItemUI(){
		if (itemType == ItemType.hint) {
			GetComponent<Text> ().text = getNumHintItem () + "";
		}
		if (itemType == ItemType.random) {
			GetComponent<Text> ().text = getNumRandomItem() + "";
		}
		if (itemType == ItemType.energy) {
			GetComponent<Text> ().text = getNumEnergyItem() + "";
		}
	}

	static bool updateTimeEnergy = false;
	void Update(){
        if (_updateVersion < updateVersion)
        {
            updateItemUI();
            _updateVersion = updateVersion;
            //if (GSM.user != null)
            //{
            //    if (itemType == ItemType.hint)
            //    {
            //        GSM.user.AddNumber(GSM.hint, getNumHintItem());
            //        Save.saveUser(GSM.user);
            //    }
            //    if (itemType == ItemType.random)
            //    {
            //        GSM.user.AddNumber(GSM.hint, getNumRandomItem());
            //        Save.saveUser(GSM.user);
            //    }
            //    if (itemType == ItemType.energy)
            //    {
            //        GSM.user.AddNumber(GSM.hint, getNumEnergyItem());
            //        Save.saveUser(GSM.user);
            //    }
            //}
        }
        //if (updateTimeEnergy && GS.Authenticated)
        //{
        //    updateTimeEnergy = false;
        //    updateItemEnergyOnline();
        //}
    }

	public static void change(){
		updateVersion++;
	}
}
