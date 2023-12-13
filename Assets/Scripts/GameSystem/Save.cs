using UnityEngine;
using System.Collections;
using System;

public class Save {
	public static string best_score = "best_score";
	public static string time_energy = "time_energy";
	public static string time_energy_server = "time_energy_server";
	public static string is_sign_up = "is_sign_up";
	public static string user_info = "user_info";
	public static string config = "config";
	public static string pack_info = "pack_info";
	public static string pack_use = "pack_use";
	public static string sound = "sound";
	public static string tutorial = "tutorial";
	public static string downloaded_level = "downloaded_level";
	public static string count = "count";

	public static string current_level = "current_level";
	public static string current_score = "current_score";
	public static string current_hint = "current_hint";
	public static string current_random = "current_random";

	public static string country_key = "a";
	public static int userVersion = 0;

	public static int GetBestScore(int level,int score){
		if(PlayerPrefs.HasKey(best_score+level))
		return PlayerPrefs.GetInt (best_score + level);
			else return score;
	}
	public static void saveScore(int score,int star,int level){
		if (PlayerPrefs.GetInt (best_score + level) < score) {
			PlayerPrefs.SetInt (best_score + level, score);
		}
		if(PlayerPrefs.GetInt(StringUtils.number_star_level + level) < star){
			PlayerPrefs.SetInt (StringUtils.number_star_level + level, star);
		}
		PlayerPrefs.SetInt (count + level, PlayerPrefs.GetInt (count + level, 0)+1);
		PlayerPrefs.Save ();
	}
	public static void setEnergyItem(int number){
		if (number < 0)
			number = 0;
		PlayerPrefs.SetInt (ItemController.item_key + ItemController.ItemType.energy.GetHashCode (), number);
		PlayerPrefs.Save ();
		ItemController.change ();
		SaveInfoAttribute.version++;
	}
	public static bool signUpParse(){
		if (!PlayerPrefs.HasKey (is_sign_up)) {
			PlayerPrefs.SetInt (is_sign_up, 1);
			return true;
		}
		return false;
	}

	public static bool isShowClassicUI(){
		bool show = true;
		return show;
	}

	public static void savePackInfo(int data){
		PlayerPrefs.SetString (pack_info + data,"1");
		PlayerPrefs.Save ();
	}

	public static bool isBuy(int packId){
		string data = PlayerPrefs.GetString (pack_info + packId);
		return false;
	}

	public static void usePack(int pack){
		PlayerPrefs.SetInt (pack_use,pack);
	}

	public static int getUsedPack(){
		return PlayerPrefs.GetInt (pack_use, isShowClassicUI()? 100:1);
	}

	public static void setCountryKey(string key)
    {
		country_key = key;
		PlayerPrefs.SetString("country_key", key);
		PlayerPrefs.Save();
	}

	public static string getCountryKey()
    {
		return PlayerPrefs.GetString("country_key");
    }

	public static bool useHintItem(int number){
		if (ItemController.getNumHintItem () < number)
			return false;
		PlayerPrefs.SetInt (ItemController.item_key + ItemController.ItemType.hint.GetHashCode (),ItemController.getNumHintItem() -  number);
		PlayerPrefs.Save ();
		ItemController.change ();
		SaveInfoAttribute.version++;
		return true;
	}

	public static bool useRandomItem(int number){
		if (ItemController.getNumRandomItem () < number)
			return false;
		PlayerPrefs.SetInt (ItemController.item_key + ItemController.ItemType.random.GetHashCode (),ItemController.getNumRandomItem() -  number);
		PlayerPrefs.Save ();
		ItemController.change ();
		SaveInfoAttribute.version++;
		return true;
	}

	public static bool getSound(){
		if (PlayerPrefs.GetInt (sound, 1) == 1) {
			return true;
		}
		return false;
	}

	public static void turnSound(bool isOn){
		if (isOn)
			PlayerPrefs.SetInt (sound, 1);
		else
			PlayerPrefs.SetInt (sound, 0);
	}

	public static void setTutorial(int tutorial) {
		PlayerPrefs.SetInt("tutorial", tutorial);
		PlayerPrefs.Save ();
	}

	public static int getTutorialStatus() {
		int tutorial = PlayerPrefs.GetInt("tutorial");
		return tutorial;
	}

	public static void setTutorialStore(int tutorial) {
		PlayerPrefs.SetInt("tutorial_store", tutorial);
		PlayerPrefs.Save ();
	}

	public static int getTutorialStore() {
		int tutorial = PlayerPrefs.GetInt("tutorial_store", 0);
		return tutorial;
	}

	public static void setDownloadedLevel(int tutorial) {
		PlayerPrefs.SetInt(downloaded_level, tutorial);
		PlayerPrefs.Save ();
	}

	public static int getDownloadedLevel() {
		int tutorial = PlayerPrefs.GetInt(downloaded_level,GameConfig.num_level);
		return tutorial;
	}

	public static string getString(string key){
		return PlayerPrefs.GetString(key,"[]");
	}

	public static void setString(string key,string val){
		PlayerPrefs.SetString(key,val);
		PlayerPrefs.Save ();
	}

	public static void setRateStatus(int status) {
		PlayerPrefs.SetInt (StringUtils.rate_status, status);
		PlayerPrefs.Save ();
	}

	public static int getRateStatus() {
		if (PlayerPrefs.HasKey (StringUtils.rate_status)) {
			return PlayerPrefs.GetInt (StringUtils.rate_status);
		} else {
			return Const.STATUS_RATE_REMIND;
		}
	}

	public static void countLevelPass() {
		int numLevelPass = PlayerPrefs.GetInt("number_level_pass", 0);
		numLevelPass++;
		PlayerPrefs.SetInt("number_level_pass", numLevelPass);
	}

	public static bool canShowRate() {
		int numLevelPass = PlayerPrefs.GetInt("number_level_pass", 0);
		return numLevelPass >= GameConfig.number_level_pass_to_show_rate ? true : false;
	}

	public static void setPlayerCoin(int player_coin) {
		// SaveInfoAttribute.version++;
		player_coin = Math.Max(player_coin, 0);
		PlayerPrefs.SetInt(StringUtils.player_coin, player_coin);
		PlayerPrefs.Save();
	}

	public static void addPlayerCoin(int coin) {
		Debug.Log("addPlayerCoin :: " + coin);
		int playerCoin = getPlayerCoin() + coin;
		setPlayerCoin(playerCoin);
	}

	public static int getPlayerCoin() {
		return PlayerPrefs.GetInt(StringUtils.player_coin, 0);
	}

	public static void changeUserItemVersion() {
		int version = getUserItemVersion();
		if (version > 1000000000) version = 0;
		PlayerPrefs.SetInt(StringUtils.user_item_version, version + 1);
		PlayerPrefs.Save();
	}

	public static void setUserItemVersion(int version) {
		PlayerPrefs.SetInt(StringUtils.user_item_version, version);
		PlayerPrefs.Save();
	}

	public static int getUserItemVersion() {
		if (PlayerPrefs.HasKey(StringUtils.user_item_version)) {
			return PlayerPrefs.GetInt(StringUtils.user_item_version);
		} else {
			return 0;
		}
	}

	//public static void setItemShopData(GSData itemShopData) {
	//	PlayerPrefs.SetString ("item_shop_data", itemShopData.JSON);
	//	PlayerPrefs.Save ();
	//}

	//public static GSRequestData getItemShopData() {
	//	if (PlayerPrefs.HasKey (StringUtils.item_shop_data)) {
	//		return new GSRequestData (PlayerPrefs.GetString (StringUtils.item_shop_data));
	//	}
	//	return null;
	//}

	public static void setShopVersion(int shop_version) {
		PlayerPrefs.SetInt(StringUtils.shop_version, shop_version);
		PlayerPrefs.Save();
	}

	public static int getShopVersion() {
		if (PlayerPrefs.HasKey(StringUtils.shop_version)) {
			return PlayerPrefs.GetInt(StringUtils.shop_version);
		}
		return 0;
	}

	public static void setConfigVersion(int config_version) {
		PlayerPrefs.SetInt(StringUtils.config_version, config_version);
		PlayerPrefs.Save();
	}

	public static int getConfigVersion() {
		if (PlayerPrefs.HasKey(StringUtils.config_version)) {
			return PlayerPrefs.GetInt(StringUtils.config_version);
		}
		return 0;
	}

	public static void setOneSignalPlayerID (string playerID) {
		PlayerPrefs.SetString (StringUtils.one_signal_player_id, playerID);
		PlayerPrefs.Save ();
	}

	public static string getOneSignalPlayerID() {
		if (PlayerPrefs.HasKey (StringUtils.one_signal_player_id)) {
			return PlayerPrefs.GetString (StringUtils.one_signal_player_id);
		}
		return "";
	}

	public static void buyRemoveAdsPack(int pack_id, int day) {
		PlayerPrefs.SetInt (StringUtils.remove_ads_pack_id, pack_id);
		PlayerPrefs.SetString (StringUtils.time_buy_remove_ads, System.DateTime.Now.ToString());
		PlayerPrefs.SetInt (StringUtils.time_remove_ads, day);
	}

	public static int getTimeRemoveAdsRemain() {
		DateTime timeBuy = DateTime.Parse(PlayerPrefs.GetString (StringUtils.time_buy_remove_ads));
		double timeRemain = DateTime.Now.Subtract (timeBuy).TotalSeconds;
		int day = PlayerPrefs.GetInt (StringUtils.time_remove_ads);
		return (int) (day * 86400 - timeRemain);
	}

	public static bool canShowAds() {
		if (PlayerPrefs.HasKey (StringUtils.time_buy_remove_ads) && PlayerPrefs.HasKey (StringUtils.time_remove_ads)) {
			if (getTimeRemoveAdsRemain () > 0) {
				return false;
			} else {
				return true;
			}
		} else {
			return true;
		}
	}

	public static int getRemoveAdsPackId() {
		if (PlayerPrefs.HasKey (StringUtils.remove_ads_pack_id)) {
			return PlayerPrefs.GetInt (StringUtils.remove_ads_pack_id);
		}
		return 0;
	}

	public static void saveGameMode(int mode) {
		PlayerPrefs.SetInt("game_mode", mode);
		PlayerPrefs.Save();
	}

	public static int getGameMode() {
		//default is 0 : normal mode
		return PlayerPrefs.GetInt("game_mode", 0);
	}

	public static void saveLevel(int level) {
		PlayerPrefs.SetInt(current_level, level);
		PlayerPrefs.Save();
	}

	public static int getCurrentLevel() {
		if (PlayerPrefs.HasKey(current_level)) {
			return PlayerPrefs.GetInt(current_level);
		}
		return 1;
	}

	public static void saveScore(int score) {
		PlayerPrefs.SetInt(current_score, score);
		PlayerPrefs.Save();
	}

	public static int getCurrentScore() {
		if (PlayerPrefs.HasKey(current_score)) {
			return PlayerPrefs.GetInt(current_score);
		}
		return 0;
	}

	public static void saveMapData(string data) {
		PlayerPrefs.SetString("map_data", data);
		PlayerPrefs.Save();
	}

	public static string getMapData() {
		if (PlayerPrefs.HasKey ("map_data")) {
			return PlayerPrefs.GetString ("map_data");
		}
		return "";
	}

	public static bool canContinueMap() {
		if (PlayerPrefs.HasKey("map_data") && PlayerPrefs.GetString("map_data") != "") {
			return true;
		}
		return false;
	}

	public static bool canContinue() {
		if (PlayerPrefs.HasKey ("can_continue") && PlayerPrefs.GetInt ("can_continue") > 0) {
			return true;
		}
		return false;
	}

	public static void setContinueStatus(int canContinue) {
		PlayerPrefs.SetInt("can_continue", canContinue);
		PlayerPrefs.Save ();
	}

	public static void saveHighScore(int score) {
		if (getHighScore() < score) {
			PlayerPrefs.SetInt("high_score", score);
			PlayerPrefs.Save();
		}
	}

	public static int getHighScore() {
		if (PlayerPrefs.HasKey("high_score")) {
			return PlayerPrefs.GetInt("high_score");
		}
		return 0;
	}

	public static void saveTutorialChangeUI(int count) {
		PlayerPrefs.SetInt("tutorial_change_ui", count);
		PlayerPrefs.Save();
	}

	public static int getTutorialChangeUI() {
		if (PlayerPrefs.HasKey("tutorial_change_ui")) {
			return PlayerPrefs.GetInt("tutorial_change_ui");
		}
		return 0;
	}

	public static void cacheString(string key, string content) {
		PlayerPrefs.SetString(key, content);
		PlayerPrefs.Save();
	}

	public static string getStringFromCache(string key) {
		return PlayerPrefs.GetString(key, "");
	}

	public static void setCountPlayAgain(int count) {
		PlayerPrefs.SetInt("count_play_again", count);
		PlayerPrefs.Save();
	}

	public static int getCountPlayAgain() {
		return PlayerPrefs.GetInt("count_play_again", 0);
	}

	//------------------ for event -------------------//
	public static void saveCurrentEventID(int event_id) {
		PlayerPrefs.SetInt("event_id", event_id);
		PlayerPrefs.Save();
	}

	public static int getCurrentEventID() {
		return PlayerPrefs.GetInt("event_id", 0);
	}

	public static void saveLevelEvent(int level) {
		PlayerPrefs.SetInt(current_level + "_event", level);
		PlayerPrefs.Save();
	}

	public static int getCurrentLevelEvent() {
		if (PlayerPrefs.HasKey(current_level + "_event")) {
			return PlayerPrefs.GetInt(current_level + "_event");
		}
		return 1;
	}

	public static void saveScoreEvent(int score) {
		PlayerPrefs.SetInt(current_score + "_event", score);
		PlayerPrefs.Save();
	}

	public static int getCurrentScoreEvent() {
		if (PlayerPrefs.HasKey(current_score + "_event")) {
			return PlayerPrefs.GetInt(current_score + "_event");
		}
		return 0;
	}

	public static void saveMapDataEvent(string data) {
		PlayerPrefs.SetString("map_data_event", data);
		PlayerPrefs.Save();
	}

	public static string getMapDataEvent() {
		if (PlayerPrefs.HasKey ("map_data_event")) {
			return PlayerPrefs.GetString ("map_data_event");
		}
		return "";
	}

	public static bool canContinueMapEvent() {
		if (PlayerPrefs.HasKey("map_data_event") && PlayerPrefs.GetString("map_data_event") != "") {
			return true;
		}
		return false;
	}

	public static bool canContinueEvent() {
		if (PlayerPrefs.HasKey ("can_continue_event") && PlayerPrefs.GetInt ("can_continue_event") > 0) {
			return true;
		}
		return false;
	}

	public static void setContinueEventStatus(int canContinue) {
		PlayerPrefs.SetInt("can_continue_event", canContinue);
		PlayerPrefs.Save ();
	}

	public static void setCountPlayEventAgain(int count) {
		PlayerPrefs.SetInt("count_play_event_again", count);
		PlayerPrefs.Save();
	}

	public static int getCountPlayEventAgain() {
		return PlayerPrefs.GetInt("count_play_event_again", 0);
	}

	public static void saveHighScoreEvent(int event_id, int score) {
		if (getCurrentEventID() == event_id) {
			if (getHighScoreEvent(event_id) < score) {
				PlayerPrefs.SetInt("high_score_event", score);
				PlayerPrefs.Save();
			}
		} else {
			saveCurrentEventID(event_id);
			PlayerPrefs.SetInt("high_score_event", score);
			PlayerPrefs.Save();
		}
	}

	public static int getHighScoreEvent(int event_id) {
		if (getCurrentEventID() == event_id) {
			return PlayerPrefs.GetInt("high_score_event", 0);
		}
		return 0;
	}
}
