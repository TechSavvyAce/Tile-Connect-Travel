using UnityEngine;
using System.Collections;

public class GameConfig {
	public static bool DEBUG_KEY = false; // Co bat che do debug test len hay khong
	public static int num_level = 29; //So luong level cua che do thuong
	public static int num_level_hard1 = 10; //So luong level cua che do hard1
	public static int num_level_hard2 = 40;//So luong level cua che do hard2
	//==========Server load==========//
	public static int num_level_event = 10; //So luong level cua che do event
    public static int[] list_level_event = { 1, 3, 5, 7, 9, 20, 22, 25, 30, 32 }; //Danh sach level cho vao che do event
    public static int default_num_hint_normal = 5; // So luong goi y cho level thuong
	public static int default_num_hint_hard1  = 8;	// So luong goi y cho level hard1
	public static int default_num_hint_hard2  = 10;	// So luong goi y cho level hard2
	public static int default_num_hint_event  = 10;	// So luong goi y cho level event
	public static int default_num_random_normal = 5; //Tuong tu cho so luong random lai map
	public static int default_num_random_hard1  = 8;
	public static int default_num_random_hard2  = 10;
	public static int default_num_random_event  = 10;
	public static int num_hint_bonus = 10;
	public static int num_random_bonus = 10;
	public static int num_coin_recover_life_base = 5; //So luong coin nho nhat dung de hoi sinh sau khi thua
	public static int probability_ads_visible = 100;
	public static int small_count = 20;	//So luong coin ban trong store cho cac goi
	public static int medium_count = 60;
	public static int big_count = 120;
	public static int coin_per_10000d = 10; // So luong coin ban trong Ngan luong cho moi 10k vnd
	public static int coin_revive_when_0 = 1;	// So luong coin hoi sinh khi nguoi choi co so coin < 5
	public static int number_level_pass_to_show_rate = 3; // So level nguoi choi vuot qua truoc khi hien phan rate len

	//==========Server load==========//
	// public static int num_level = 1;
	public static int APP_VERSION = 22;
	#if UNITY_ANDROID
	public static int store_id = 1;
	#else
	public static int store_id = 2;
	#endif
	public static float item_eat_time = 0.5f;
	//ads confi
    public static bool isShowStartAds = true;
    public static string admob_banner_android = "ca-app-pub-3940256099942544/6300978111";
    public static string admob_full_android = "ca-app-pub-3940256099942544/1033173712";

    public static string admob_banner_ios = "id_admod_banner_ios";
    public static string admob_full_ios = "id_admod_interstitial_ios";
    //Game Name
    //public static string game_name = "pikachu_2017";
	//==============Item Purchase=============//
	public static int num_energy_lite = 20;
	public static int num_random_lite = 10;
	public static int num_hint_lite = 10;
	public static int num_energy_normal = 50;
	public static int num_random_normal = 25;
	public static int num_hint_normal = 25;
	public static int num_energy_big = 120;
	public static int num_random_big = 60;
	public static int num_hint_big = 60;
	//==============Game parrams=============//
	public static int num_item_hint_start = 5;
	public static int num_item_random_start = 5;
	public static int num_item_energy_start = 10;
	public static int map_start_row = 9;
	public static int map_start_col = 16;
	public static int time_energy_recover = 900;
	public static int max_energy_offline = 3;
	public static int max_energy_online = 5;
	public static int bonus_facebook_invite = 25;

	//score bonus
	public static int bonus_score_star1 = 1; //So diem tang them cho nguoi choi khi ket thuc van theo tung star ma nguoi dat duoc (time x score)
	public static int bonus_score_star2 = 2;
	public static int bonus_score_star3 = 4;
	public static int bonus_victory     = 5000;
	//number pokemon kind
	public static int total_pokemon_kind = 30;
	//online config
	public static int[] list_online_level = {1001,1002,1003,1004,1005,1006,1007,1008,1009,1010};
	public static int num_friend_per_energy = 1000;
	public static int bonus_item_login_facebook = 2;
	public static int time_change_to_bot = 30;
	public static int time_wait_result = 5;

//	public static int[] list_online_level = {1004};
	public static float max_time_delay = 2f;
	public static float max_tranfer_caculate = 3;
	public static float time_resend_pack = 3;
	public static int online_map_start_row = 10;

	public static int map_margin_bottom = 5;
	//===============trophy leader board==============//
	public static int leader_board_max_player = 100;
	public static int leader_board_global = 1;
	public static int leader_board_by_country = 2;
	public static int leader_board_by_friends = 3;

	public static float[] default_random_time_1 = {6, 6, 6, 7, 6, 7, 5, 5, 5, 5, 4, 5, 4, 3, 3, 3};// con 1 cap
	public static float[] default_random_time_2 = {5, 5, 6, 6, 5, 5, 5, 6, 6, 5, 5, 5, 4, 3, 3, 2};// con 2 cap
	public static float[] default_random_time_3 = {4, 5, 5, 4, 4, 5, 5, 4, 5, 4, 4, 4, 3, 3, 3, 2};// con 3 cap
	public static float[] default_random_time_4 = {4, 4, 3, 4, 3, 4, 4, 3, 4, 4, 4, 3, 3, 3, 3, 2};// con 4 cap
	public static float[] default_random_time_5 = {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2};// con >= 5 cap
	public static long[] default_count_random_time_1 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};// con 1 cap
	public static long[] default_count_random_time_2 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};// con 2 cap
	public static long[] default_count_random_time_3 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};// con 3 cap
	public static long[] default_count_random_time_4 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};// con 4 cap
	public static long[] default_count_random_time_5 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};// con >= 5 cap
	public static float[] default_random_time_offset_1 = {3.5f, 3.5f, 3.3f, 3.3f, 3.2f, 3.1f, 3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 2.5f, 2.4f, 1.5f, 1.7f, 0.8f};
	public static float[] default_random_time_offset_2 = {3.2f, 3.2f, 3.0f, 3.0f, 2.5f, 2.5f, 2.8f, 2.8f, 2.8f, 2.5f, 2.3f, 2.2f, 2.1f, 2.0f, 2.0f, 0.8f};
	public static float[] default_random_time_offset_3 = {2.9f, 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 2.7f, 2.7f, 2.7f, 2.5f, 2.3f, 2.2f, 2.1f, 2.0f, 1.8f, 1.0f};
	public static float[] default_random_time_offset_4 = {2.5f, 2.5f, 2.3f, 2.3f, 2.4f, 2.4f, 2.4f, 2.3f, 2.3f, 2.3f, 2.2f, 2.1f, 2.0f, 2.0f, 1.2f, 1.2f};
	public static float[] default_random_time_offset_5 = {2.0f, 2.0f, 2.1f, 2.1f, 2.1f, 2.2f, 2.2f, 2.0f, 2.0f, 1.8f, 1.8f, 1.7f, 1.7f, 1.7f, 1.7f, 1.5f};

	//================network ping===============//
	//public static string network_ping_url   = "http://google.com";
	//public static int    network_ping_count = 3;
	//public static int    network_ping_time  = 5;

	//public static int request_time_out = 10000;
	//public static string link_web = "https://pikachu-web.firebaseapp.com/";
	//public static void updateConfig(){
		//num_hint_big = GSM.CONFIG.GetInt ("num_hint_big") == null ? num_hint_big : (int)GSM.CONFIG.GetInt ("num_friend_per_energy");
		//num_friend_per_energy = GSM.CONFIG.GetInt ("num_friend_per_energy") == null ? num_friend_per_energy : (int)GSM.CONFIG.GetInt ("num_friend_per_energy");
		//time_change_to_bot = GSM.CONFIG.GetInt ("time_change_to_bot") == null ? time_change_to_bot : (int)GSM.CONFIG.GetInt ("time_change_to_bot");
		//bonus_item_login_facebook = GSM.CONFIG.GetInt ("bonus_item_login_facebook") == null ? bonus_item_login_facebook : (int)GSM.CONFIG.GetInt ("bonus_item_login_facebook");
		//time_energy_recover = GSM.CONFIG.GetInt ("time_energy_recover") == null ? time_energy_recover : (int)GSM.CONFIG.GetInt ("time_energy_recover");
		//max_energy_offline = GSM.CONFIG.GetInt ("max_energy_offline") == null ? max_energy_offline : (int)GSM.CONFIG.GetInt ("max_energy_offline");
		//max_energy_online = GSM.CONFIG.GetInt ("max_energy_online") == null ? max_energy_online : (int)GSM.CONFIG.GetInt ("max_energy_online");
		//link_web = GSM.CONFIG.GetString ("link_web") == null ? link_web :(string) GSM.CONFIG.GetString ("link_web");

		// //random time
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_1) != null) {
		// 	default_random_time_1 = GSM.CONFIG.GetFloatList (StringUtils.random_time_1).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_2) != null) {
		// 	default_random_time_2 = GSM.CONFIG.GetFloatList (StringUtils.random_time_2).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_3) != null) {
		// 	default_random_time_3 = GSM.CONFIG.GetFloatList (StringUtils.random_time_3).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_4) != null) {
		// 	default_random_time_4 = GSM.CONFIG.GetFloatList (StringUtils.random_time_4).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_5) != null) {
		// 	default_random_time_5 = GSM.CONFIG.GetFloatList (StringUtils.random_time_5).ToArray();
		// }
		// //count random time
		// if (GSM.CONFIG.GetLongList (StringUtils.random_time_1) != null) {
		// 	default_count_random_time_1 = GSM.CONFIG.GetLongList (StringUtils.count_random_time_1).ToArray();
		// }
		// if (GSM.CONFIG.GetLongList (StringUtils.random_time_2) != null) {
		// 	default_count_random_time_2 = GSM.CONFIG.GetLongList (StringUtils.count_random_time_2).ToArray();
		// }
		// if (GSM.CONFIG.GetLongList (StringUtils.random_time_3) != null) {
		// 	default_count_random_time_3 = GSM.CONFIG.GetLongList (StringUtils.count_random_time_3).ToArray();
		// }
		// if (GSM.CONFIG.GetLongList (StringUtils.random_time_4) != null) {
		// 	default_count_random_time_4 = GSM.CONFIG.GetLongList (StringUtils.count_random_time_4).ToArray();
		// }
		// if (GSM.CONFIG.GetLongList (StringUtils.random_time_5) != null) {
		// 	default_count_random_time_5 = GSM.CONFIG.GetLongList (StringUtils.count_random_time_5).ToArray();
		// }
		// //random time offset
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_1) != null) {
		// 	default_random_time_offset_1 = GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_1).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_2) != null) {
		// 	default_random_time_offset_2 = GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_2).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_3) != null) {
		// 	default_random_time_offset_3 = GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_3).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_4) != null) {
		// 	default_random_time_offset_4 = GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_4).ToArray();
		// }
		// if (GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_5) != null) {
		// 	default_random_time_offset_5 = GSM.CONFIG.GetFloatList (StringUtils.random_time_offset_5).ToArray();
		// }
		//if (GSM.CONFIG.GetIntList ("list_online_level") != null) {
		//	list_online_level = GSM.CONFIG.GetIntList ("list_online_level").ToArray();
		//}
		////leader board config param
		//if (GSM.CONFIG.GetNumber(StringUtils.leader_board_global) != null) {
		//	leader_board_global = (int) GSM.CONFIG.GetNumber (StringUtils.leader_board_global);
		//}
		//if (GSM.CONFIG.GetNumber(StringUtils.leader_board_by_country) != null) {
		//	leader_board_by_country = (int) GSM.CONFIG.GetNumber (StringUtils.leader_board_by_country);
		//}
		//if (GSM.CONFIG.GetNumber(StringUtils.leader_board_by_friends) != null) {
		//	leader_board_by_friends = (int) GSM.CONFIG.GetNumber (StringUtils.leader_board_by_friends);
		//}
		////network ping config
		//if (GSM.CONFIG.ContainsKey(StringUtils.network_ping_url)) {
		//	network_ping_url = GSM.CONFIG.GetString (StringUtils.network_ping_url);
		//}
		//if (GSM.CONFIG.ContainsKey(StringUtils.network_ping_time)) {
		//	network_ping_time = (int) GSM.CONFIG.GetInt (StringUtils.network_ping_time);
		//}
		//if (GSM.CONFIG.ContainsKey(StringUtils.network_ping_url)) {
		//	network_ping_count = (int) GSM.CONFIG.GetInt (StringUtils.network_ping_count);
		//}
		//if(GSM.CONFIG.ContainsKey("list_level_event")){
		//	list_level_event= GSM.CONFIG.GetIntList("list_level_event").ToArray();
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_hint_normal")){
		//	default_num_hint_normal= (int)GSM.CONFIG.GetInt("default_num_hint_normal");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_hint_hard1")){
		//	default_num_hint_hard1= (int)GSM.CONFIG.GetInt("default_num_hint_hard1");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_hint_hard2")){
		//	default_num_hint_hard2= (int)GSM.CONFIG.GetInt("default_num_hint_hard2");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_hint_event")){
		//	default_num_hint_event = (int)GSM.CONFIG.GetInt("default_num_hint_event");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_random_normal")){
		//	default_num_random_normal= (int)GSM.CONFIG.GetInt("default_num_random_normal");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_random_hard1")){
		//	default_num_random_hard1= (int)GSM.CONFIG.GetInt("default_num_random_hard1");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_random_hard2")){
		//	default_num_random_hard2= (int)GSM.CONFIG.GetInt("default_num_random_hard2");
		//}
		//if(GSM.CONFIG.ContainsKey("default_num_random_event")){
		//	default_num_random_event = (int)GSM.CONFIG.GetInt("default_num_random_event");
		//}
		//if(GSM.CONFIG.ContainsKey("num_hint_bonus")){
		//	num_hint_bonus= (int)GSM.CONFIG.GetInt("num_hint_bonus");
		//}
		//if(GSM.CONFIG.ContainsKey("num_random_bonus")){
		//	num_random_bonus= (int)GSM.CONFIG.GetInt("num_random_bonus");
		//}
		//if(GSM.CONFIG.ContainsKey("num_coin_recover_life_base")){
		//	num_coin_recover_life_base= (int)GSM.CONFIG.GetInt("num_coin_recover_life_base");
		//}
		//if(GSM.CONFIG.ContainsKey("probability_ads_visible")){
		//	probability_ads_visible= (int)GSM.CONFIG.GetInt("probability_ads_visible");
		//}
		//if(GSM.CONFIG.ContainsKey("small_count")){
		//	small_count= (int)GSM.CONFIG.GetInt("small_count");
		//}
		//if(GSM.CONFIG.ContainsKey("medium_count")){
		//	medium_count= (int)GSM.CONFIG.GetInt("medium_count");
		//}
		//if(GSM.CONFIG.ContainsKey("big_count")){
		//	big_count= (int)GSM.CONFIG.GetInt("big_count");
		//}
		//if(GSM.CONFIG.ContainsKey("coin_per_10000d")){
		//	coin_per_10000d= (int)GSM.CONFIG.GetInt("coin_per_10000d");
		//}
		//if(GSM.CONFIG.ContainsKey("coin_revive_when_0")){
		//	coin_revive_when_0= (int)GSM.CONFIG.GetInt("coin_revive_when_0");
		//}
		//if (GSM.CONFIG.ContainsKey("number_level_pass_to_show_rate")) {
		//	number_level_pass_to_show_rate = (int) GSM.CONFIG.GetInt("number_level_pass_to_show_rate");
		//}
	//	try{
	//		GameStatic.storeController.updatePrice();
	//	}catch(System.Exception e){
	//	}
	//}

	//public static float getPercentShowFacebookAds(){
	//	return 0.5f;
	//}
}
