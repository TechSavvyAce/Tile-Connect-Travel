using UnityEngine;
using System.Collections;

public class Const {
	//level mode
	public static int GAME_MODE_NORMAL = 0; // normal mode (29 level)
	public static int GAME_MODE_HARD1  = 1; // hard mode 1 (10 level - new level)
	public static int GAME_MODE_HARD2  = 2; // hard mode 2 (40 level)
	public static int GAME_MODE_EVENT  = 3;

	//logic direction of map
	public static int DIRECTION_NONE = 0;
	public static int DIRECTION_LEFT = 1;
	public static int DIRECTION_RIGHT = 2;
	public static int DIRECTION_UP = 3;
	public static int DIRECTION_DOWN = 4;

	//stone, frozen
	public static int STONE_FIXED_ID = 99;
	public static int STONE_MOVING_ID = 98;
	public static int FROZEN_FIXED_ID = 97;
	public static int FROZEN_MOVING_ID = 96;

	//state of pokemon
	public static int STATE_NORMAL = 0;
	public static int STATE_FROZEN = 1;
	public static int STATE_DISAPPEARING = 2;

	//item in game
	public static int REWARD_HINT = 0;
	public static int REWARD_RANDOM = 1;
	public static int REWARD_ENERGY = 2;

	//auto gen type
	public static int NOT_AUTO_GEN = 0;
	public static int AUTO_GEN_FROZEN = 1;
	public static int AUTO_GEN_POKEMON = 2;
	public static int AUTO_GEN_FROZEN_AND_POKEMON = 3;

	public static int GENERATE_STATE_WAIT = 0;
	public static int GENERATE_STATE_PLAY_CLOCK = 1;
	public static int GENERATE_STATE_DONE = 2;

	//rate status
	public static int STATUS_RATE_REMIND = 0;
	public static int STATUS_RATED = 1;
	public static int STATUS_NOT_RATE = 2;

	//item shop type
	public static int TYPE_ITEM_IN_GAME = 0;
	public static int TYPE_ITEM_UI = 1;
	public static int TYPE_ITEM_COIN = 2;
	public static int TYPE_ITEM_REMOVE_ADS = 3;

	//item UI
	public static int UI_DEFAULT_POKEMON = 0;
	public static int UI_DEFAULT_KAWAI   = 1;
	public static int UI_DEFAULT_FAKE = 2;

	//iteam UI by country
	public static string UI_DEFAULT_ARGENTINA = "a";
	public static string UI_DEFAULT_AUSTRALIA = "b";
	public static string UI_DEFAULT_BELGIUM = "c";
	public static string UI_DEFAULT_BRAZIL = "d";
	public static string UI_DEFAULT_CANADA = "e";
	public static string UI_DEFAULT_COLOMBIA = "f";
	public static string UI_DEFAULT_FRANCE = "g";
	public static string UI_DEFAULT_GERMANY = "h";
	public static string UI_DEFAULT_GREECE = "i";
	public static string UI_DEFAULT_INDIA = "j";
	public static string UI_DEFAULT_INDONESIA = "k";
	public static string UI_DEFAULT_IRELAND = "l";
	public static string UI_DEFAULT_ITALY = "m";
	public static string UI_DEFAULT_JAPAN = "n";
	public static string UI_DEFAULT_KOREA = "o";
	public static string UI_DEFAULT_MEXICO = "p";
	public static string UI_DEFAULT_MOROCO = "q";
	public static string UI_DEFAULT_NETHERLANDS = "r";
	public static string UI_DEFAULT_PERU = "s";
	public static string UI_DEFAULT_PORTUGAL = "t";
	public static string UI_DEFAULT_RUSSIA = "u";
	public static string UI_DEFAULT_SOUTHAFRICA = "v";
	public static string UI_DEFAULT_SPAIN = "w";
	public static string UI_DEFAULT_THAILAND = "x";
	public static string UI_DEFAULT_UK = "y";
	public static string UI_DEFAULT_USA = "z";
	public static string UI_DEFAULT_VIETNAM = "aa";

	//item coin
	public static int COIN_PACK_FREE   = 999;
	public static int COIN_PACK_SMALL  = 900;
	public static int COIN_PACK_LARGER = 901;
	public static int COIN_PACK_BIG    = 902;

	//item remove ads
	public static int ITEM_REMOVE_ADS = 800;

	//redirect by event
	public static string REDIRECT_NONE = "none";
	public static string REDIRECT_OPEN_STORE = "open_store";
	public static string REDIRECT_OPEN_MESSAGE = "open_message";
	public static string REDIRECT_OPEN_EVENT = "open_event";
	public static string REDIRECT_OPEN_EVENT_REWARD = "open_reward";
	public static string REDIRECT_OPEN_STORE_ITEM = "open_store_item";
	public static string REDIRECT_OPEN_STORE_UI = "open_store_ui";
	public static string REDIRECT_OPEN_STORE_COIN = "open_store_coin";
	public static string REDIRECT_OPEN_STORE_OTHER = "open_store_other";
}
