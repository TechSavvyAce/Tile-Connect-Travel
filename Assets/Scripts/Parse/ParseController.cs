using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class ParseController : MonoBehaviour {
	public static string facebook_id = "facebook_id";
	public static string facebook_name = "facebook_name";
	public static string user_name = "userName";
	public static string bonus_hint = "bonus_hint";
	public static string reset_bonus = "reset_bonus";
	public static string bonus_random = "bonus_random";
	public static string bonus_energy = "bonus_energy";
	public static string bonus_message = "bonus_message";
	public static string bonus_coin = "bonus_coin";
	public static string level_key = "level";
	public static string facebook_friend = "facebook_friend";
	public static string score_level = "score_level_";

	public static bool isGetData = false;

	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){
		if (_isShowBonus && GameStatic.messagePopup != null) {
			ItemController.addEnergyItem (num_bonus_energy);
			ItemController.addRandomItem (num_bonus_random);
			ItemController.addHintItem (num_bonus_hint);
			Save.addPlayerCoin (num_bonus_coin);
			GameStatic.itemEnergy.updateItemUI ();
			Debug.Log ("parse get bonus done");
			num_bonus_energy = 0;
			num_bonus_random = 0;
			num_bonus_hint = 0;
			num_bonus_coin = 0;
			GameStatic.messagePopup.showPopup (_textBonus);
			_isShowBonus = false;
		}
		if (_isCheckBonus) {
			_isCheckBonus = false;
			checkBonus ();
		}
	}
	bool _isShowBonus = false;
	string _textBonus ="";
	public static bool _isCheckBonus = false;
	int num_bonus_energy,num_bonus_hint,num_bonus_random,num_bonus_coin;
	void checkBonus(){
		string text = StringUtils.received_item_from_server+ " ";
		bool _isHasBonus = false;
		
		if (!_isHasBonus)
			text = "";
		_textBonus = text;
	}

	static bool _is_get_friend_level = false;
	public static void getFriendLevel(){
		_is_get_friend_level = true;
	}

	public static List<string> getListUser(int level){
		List<string> result = new List<string> ();
		return result;
	}

}
