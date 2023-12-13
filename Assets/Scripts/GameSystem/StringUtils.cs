using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StringUtils : MonoBehaviour{
	public static string level_lock 	   	= "level_lock";
	public static string number_star_level 	= "number_star_level";
	public static string time_dead         	= "time_dead";
	public static string time_star_1 		= "time_star_1";
	public static string time_star_2 		= "time_star_2";
	public static string time_star_3 		= "time_star_3";
	public static string auto_gen_type		= "auto_gen_type";
	public static string time_gen			= "time_gen";
	public static string time_gen_wait		= "time_gen_wait";
	public static string auto_gen 			= "auto_gen";

	public static string level 				= "level";
	public static string direction 			= "direction";
	public static string constraint 		= "constraint";
	public static string cell1 				= "cell1";
	public static string cell2 				= "cell2";
	public static string row 				= "row";
	public static string col 				= "col";
	public static string stones_fixed 		= "stones_fixed";
	public static string stones_moving 		= "stones_moving";
	public static string frozens_fixed 		= "frozens_fixed";
	public static string pokemon_fixed      = "pokemon_fixed";
	public static string probability 		= "probability";
	public static string number 			= "number";
	public static string hint 				= "hint";
	public static string random 			= "random";
	public static string energy 			= "energy";
	public static string reward 			= "reward"; 
	public static string type				= "type";
	public static string id 				= "id";
	public static string player_coin        = "player_coin";
	public static string user_item_version  = "user_item_version";
	public static string item_shop_data     = "item_shop_data";
	public static string item_type          = "item_type";
	public static string pack_id 			= "pack_id";
	public static string item_id 			= "item_id";
	public static string description 		= "description";
	public static string cost 				= "cost";
	public static string cost_type 			= "cost_type";
	public static string shop_version		= "shop_version";
	public static string config_version		= "config_version";
	public static string coin_bonus         = "coin_bonus";
	public static string score				= "score";

	//=====Player data==========//
	public static string random_time_1 = "rt_1";
	public static string random_time_2 = "rt_2";
	public static string random_time_3 = "rt_3";
	public static string random_time_4 = "rt_4";
	public static string random_time_5 = "rt_5";
	public static string count_random_time_1 = "crt_1";
	public static string count_random_time_2 = "crt_2";
	public static string count_random_time_3 = "crt_3";
	public static string count_random_time_4 = "crt_4";
	public static string count_random_time_5 = "crt_5";
	public static string random_time_offset_1 = "rto_1";
	public static string random_time_offset_2 = "rto_2";
	public static string random_time_offset_3 = "rto_3";
	public static string random_time_offset_4 = "rto_4";
	public static string random_time_offset_5 = "rto_5";
	public static string has_random_time = "has_random_time";
	public static string rank_global  = "rank_global";
	public static string rank_local   = "rank_local";
	public static string rank_friends = "rank_friends";
	public static string rank_before  = "rank_before";
	public static string rank_after   = "rank_after";
	public static string rate_status  = "rate_status";
	public static string facebook_name = "facebook_name";
	public static string one_signal_player_id = "one_signal_player_id";
	public static string time_buy_remove_ads = "time_buy_remove_ads";
	public static string time_remove_ads = "time_remove_ads";
	public static string remove_ads_pack_id = "remove_ads_pack_id";

	//=====Scene name===========//
	public static string scene_main				= "Main";
	public static string scene_level				= "LevelSence";
	public static string scene_play_off				= "GamePlay";
	public static string scene_online_menu				= "OnlineMenu";
	public static string scene_play_on				= "OnlinePlay";
	public static string scene_play_tutorial				= "Help";

	//=====Leader board==========//
	public static string leader_board_global 	 = "leader_board_global";
	public static string leader_board_by_country = "leader_board_by_country";
	public static string leader_board_by_friends = "leader_board_by_friends";
	public static string list_rank 		= "listRank";
	public static string my_player_rank = "myPlayerRank";
	public static string user_name 		= "userName";
	public static string trophies       = "trophies";
	public static string rank    		= "rank";
	public static string user_id 		= "userId";

	//=====Network Ping Config========//
	public static string network_ping_url   = "network_ping_url";
	public static string network_ping_time  = "network_ping_time";
	public static string network_ping_count = "network_ping_count";

	//=====Push Notification=========//
	public static string time_notify_recover_energy = "time_notify_recover_energy";

	//================= Text trong game================//
	public static string loading = "Loading...";
	public static string loading_old_data = "Load save data on server to local...";
	public static string no_facebook_level_data = "No friend play this level...";
	public static string loading_online_random_map = "Random map...";
	public static string loading_online_prepare_map = "Init map...";
	public static string not_enough_energy = "You don't have enough energy! Please wait or go to store.";
	public static string not_enough_item = "You don't have enough item!!!";
	public static string time_over = "Time over!";
	public static string can_not_eat_ice = "Ice can not be eaten any more!\n(Không thể phá huỷ hoàn toàn băng trong màn chơi!)";
	public static string out_of_game_pause_reason = "Exiting the game causes you to lose! Sorry.";
	public static string out_of_game_enermy_out_reason = "All players have left the game. You've won.";
	public static string win_game = "You win!";
	public static string lose_game = "You lose!";

	public static string error = "Something went wrong! Please try again later!";
	public static string error_text_empty = "Error! Text is empty";
	public static string success_store_purchase = "Success! You received: ";
	public static string success = "Success!";
	public static string received_item_from_server = "You received: ";
	public static string received_coin_from_watching_video = "Received coin from watching video: +";
	public static string received_energy_from_watching_video_click = "You received: 3 energy.";
	public static string received_energy_from_facebook_invite = "Facebook invite success! You received: ";
	public static string received_energy_from_facebook_login = "Facebook login success! You received: ";
	public static string video_skip = "Do not skip video.";
	public static string buy = "Buy";
	public static string buyed = "Sold";
	public static string active = "Activate";
	public static string actived = "Used";
	public static string update = "Update";
	public static string download_done = "Download complete!";
	public static string downloading = "Downloading...";
	public static string not_enough_money = "You don't have enough item to purchase! Please buy more in store.";
	public static string server_connect = "Connect to server...";
	public static string lose_trophies = "You lose and lost ";
	public static string wait_friend = "Wait for friend...";
	public static string player_out = "Opponent has left the game";
	public static string disconnect = "No internet connection.";
	public static string notify_recover_energy = "notify_recover_energy";
	public static string messgae_recover_energy = "Your energy is recovered, you can play continue...";
	public static string message_not_enough_coin = "You don't have enough coin to buy this item!";
	public static string message_not_enough_ruby = "Not enough ruby to play again !\n(Bạn không còn đủ ngọc để chơi lại!)";
	public static string[] message_text_hint = {
		"Eat the only pair of pokemon that can be eaten on map and get 80 points.",
		"Eat one pair of pokemon while there are only 2 pairs that can be eaten and get 40 points",
		"Eat one pair of pokemon while there are only 3 pairs that can be eaten and get 20 points",
		"Eat one pair of pokemon while there are more than three pairs that can be eaten and get 10 points",
		"Eat continuously two or more pair, you will have 5 more bonus points.",
		"Win online battle, you will get some free coins.",
		"You can use coin to buy items in store",
		"You can buy coin in store",
		"During event time, join and play to receive more coins and trophies."
	};

	public Text[] tranlateText;
	void Start(){
		foreach(Text text in tranlateText){
			text.text = text.name;
		}
	}

	public static string getNumberDot(double number) {
		return string.Format("{0:#,###0}", number);
	}

	public static string getMessageHint() {
		int length = message_text_hint.Length;
		int count = (int) Random.Range (0, length - 1);
		return message_text_hint [count];
	}
}
