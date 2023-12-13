using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine.Analytics;
using GoogleMobileAds.Api;

public class Map : MonoBehaviour 
{
	public static Map ins;
	public enum MapState{eating, playing, moving, none, result, pause, dialog_recover};
	public MapState mapState = MapState.none;
	private bool DEBUG = GameConfig.DEBUG_KEY;
	private GameObject[][] MAP_POKEMONS;

	//====camera controller===//
	public CameraController cameraController;
	public GameObject finger;
	public Object prefap_pikachu;
	public Object prefap_autoGen;
	public GameObject tutorialObj;
	public int[][] MAP;
	public int[][] MAP_FROZEN;
	public Vec2 POS1;
	public Vec2 POS2;
	public Vec2 HINT_POS1;
	public Vec2 HINT_POS2;
	public Vector3[][] POS;
	public static int MIN_X;
	public static int MIN_Y;
	public static int CELL_WIDH = 28;
	public static int CELL_HEIGHT = 28;
	public static int MAP_HEIGHT = 28;
	public static int MAP_WIDTH = 28;
	public static int order = 0;
	public int col = 16;
	public int row = 9;
	private bool isReseting;
	private bool isGameOver;

	public LogicLevel logicLevel;
	public PathSystem pathSystem;

	private ArrayList LPath;
	private ArrayList keyLPath;

	private ArrayList list_pokemon_can_eat1 = new ArrayList();
	private ArrayList list_pokemon_can_eat2 = new ArrayList();

	public bool checking_paire;
	public ResultBar resultBar;
	public ResultBar failBar;
	public TimeBar timeBar;
	public ItemController hintController;
	public ItemController randomController;
	public bool _isCheckWrong = false;
	public ArrayList check_id = new ArrayList();
	public Hashtable list_frozen_put = new Hashtable();
	public ArrayList listAutoGens = new ArrayList ();

	public bool isTutorial;
	public Tutorial tutorial;

	private ArrayList list_block_frozen = new ArrayList();
	private ArrayList list_block_frozen_normal = new ArrayList();
	private ArrayList list_block_frozen_special = new ArrayList();

	private float currentAutoGenTime = 0;
	private float time_zoom_to_frozen = 5;
	private float lastTime = 0;
	private float time_update_list_frozen = 5;
	private int _numberResetMap = 0;
	public float currentTime;
	public int nextScore = 10;
	public int extraPeriodScore = 0;
	public int numberPokemonRemain = 0;

	bool saveGameFirstTime = false;
	bool deadTypeTimeOver = false;
	float countSaveGameTime = 0;

    private bool isShowSetting = false;

	//===========Start game=========//
	void Start () 
	{
		ins = this;
		nextScore = 10;
		row = GameConfig.map_start_row;
		col = GameConfig.map_start_col;
		prefap_pikachu = Resources.Load("item");
		// logicLevel = GetComponentInParent<LogicLevel> ();
		pathSystem = GetComponent<PathSystem> ();
		LPath = new ArrayList ();
		keyLPath = new ArrayList ();
		LMap(row, col);
		if (isTutorial) {
			logicLevel.setLevelForTutorial();
			if (Save.getTutorialChangeUI() == 0) {
				tutorialObj.SetActive(true);
			}
		} else {
			logicLevel.setLevel (GameStatic.currentLevel);
		}
		_randomMap ();
		if (!isTutorial && GameStatic.mapData != null) {
			_loadOldMap (GameStatic.mapData);
		}
	}

	public void test() {
		Hint ();
		POS1 = HINT_POS1;
		POS2 = HINT_POS2;
		eat (HINT_POS1, HINT_POS2, false);
	}
	public void changeState(MapState state){
		if (mapState == state)
			return;
		mapState = state;
		if (mapState == MapState.playing) {
			checking_paire = false;
			for (int i = 0; i < listAutoGens.Count; i++) {
				AutoGenController autoGen = (AutoGenController)listAutoGens [i];
				autoGen.playClockCountDown ();
			}
		} else if (mapState == MapState.eating) {
			checking_paire = true;
		} else if (mapState == MapState.pause) {
			checking_paire = true;
			for (int i = 0; i < listAutoGens.Count; i++) {
				AutoGenController autoGen = (AutoGenController)listAutoGens [i];
				autoGen.pauseClockCountDown ();
			}
		} else if (mapState == MapState.result) {
			checking_paire = true;
			for (int i = 0; i < listAutoGens.Count; i++) {
				AutoGenController autoGen = (AutoGenController)listAutoGens [i];
				autoGen.pauseClockCountDown ();
			}
		} else {
			checking_paire = false;
		}
	}

	void Update () 
	{
		if (DEBUG) {
			if (Input.GetKeyDown (KeyCode.R)) {
				RandomMap();
			}
			if (Input.GetKeyDown (KeyCode.K)) {
//				DebugSendHighScore ();
			}
			if (Input.GetKeyDown (KeyCode.F)) {
				Hint();
				Debug.Log("Goi y 2 pokemon ::::: POS1 " + HINT_POS1.Print() + "---- POS2 " + HINT_POS2.Print());
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				Save.saveMapData("");
			}
			if (Input.GetKeyDown (KeyCode.N)) {
				nextLevel();
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				resultBar.hideResult();
				failBar.hideResult();
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				autoSelect();
			}
			if (Input.GetKeyDown (KeyCode.G)) {
				resetCamera ();
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				resultBar.showResult (2, 1000, 3, false);
			}
			// if (Input.GetKeyDown (KeyCode.C)) {
			// 	resultBar.showResult (2, 2000, ListLevel.CurrentLevel);
			// }
			// if (Input.GetKeyDown(KeyCode.S)) {
			// 	OnlineData.send_me_your_map ();
			// }
			// if (Input.GetKeyDown (KeyCode.M)) {
			// 	Save.saveScore (3211, 2, 4);
			// 	ParseController.saveLevelScore ();
			// }
			// if (Input.GetKeyDown (KeyCode.B)) {
			// 	Hint ();
			// 	getPokemonClass (HINT_POS1.R, HINT_POS1.C).thunderMe ();
			// }
			if (Input.GetKeyDown (KeyCode.O)) {
				GameStatic.saveGame ();
			}

			// if (Input.GetKeyDown (KeyCode.L)) {
			// 	ToastManager.showToast ("haha");
			// }
		}

		if (Input.GetMouseButtonDown(0)) {
			if (isGameOver || isShowSetting || checking_paire) {
				return;
			}

			float x = (Input.mousePosition.x - Screen.width / 2) / Screen.width * (Screen.width * 1.0f / Screen.height);
			float y = ((Input.mousePosition.y - Screen.height / 2) / Screen.height)  ;
			x *= 320;
			y *= 320;
			int mouse_col = (int)((x-MIN_X)/CELL_WIDH);
			int mouse_row = (int)((y - MIN_Y + CELL_HEIGHT/2) / CELL_HEIGHT);
			if (isTutorial) {
				if (!tutorial.onClick (mouse_row, mouse_col)) {
					return;
				}
			}
			Select(new Vec2(mouse_row, mouse_col));
		}

		if (check_id.Count> 0) {
			checkWrongOnlineEat ((int)(check_id[0]));
			check_id.RemoveAt(0);
			_isCheckWrong = false;
		}
		//======start=====//
		if (mapState != MapState.pause  && !isTutorial) {
			currentTime += Time.deltaTime;
			if (currentTime > 3 * countSaveGameTime) {
				GameStatic.saveGameTime ();
				countSaveGameTime++;
			}
			if (!saveGameFirstTime) {
				GameStatic.saveGame();
				saveGameFirstTime = true;
			}
		}
		//======autogen frozen=======//
		if (mapState == MapState.playing && !isGameOver) {
			updateLogicAutoGen();
		}
		//caculator random time
		//======wait zoom to frozen when game over and show result=====//
		if (isGameOver && mapState != MapState.result && mapState != MapState.dialog_recover) {
			if (currentTime - lastTime > time_zoom_to_frozen) {
				resetCamera ();
				showRecoverLifePopup();
			}
		}
		//======end=======//
	}

    public void showSetting()
    {
        isShowSetting = !isShowSetting;
    }

    public void hideSetting()
    {
        isShowSetting = false;
    }

	void showRecoverLifePopup() {
		if (mapState != MapState.dialog_recover) {
			GameStatic.recoverLifePopup.showPopup ();
			mapState = MapState.dialog_recover;
			//showBannerView();
		}
	}

	IEnumerator showRecoverLifePopupDelay(){
		yield return new WaitForFixedUpdate ();
        showRecoverLifePopup();
        //letMeDieOnClick();
	}

	//-------onclick let me die------//
	public void letMeDieOnClick() {
		GameStatic.recoverLifePopup.hidePopup ();

		failBar.showFail (deadTypeTimeOver ? StringUtils.time_over: StringUtils.can_not_eat_ice,logicLevel.getScore());
		changeState(MapState.result);
		GameStatic.endGame();
		deadTypeTimeOver = false;
		Analytics.CustomEvent ("gameOver", new Dictionary<string, object>{
			{"score",GameStatic.currentScore},
			{"level",GameStatic.currentLevel}
		});
	}
	//-------onclick recover life----//
	public void recoverLifeOnClick() {
		if (GameStatic.checkEnoughCoinToRecover()) {
			GameStatic.recoverLifePopup.hidePopup ();
			Replay ();
			int coinNeed = GameStatic.coin_need_to_recover;
			int cointRemain = Save.getPlayerCoin() - GameStatic.coin_need_to_recover;
			PlayerInfo.saveCoin(cointRemain);
			GameStatic.playerInfo.updatePlayerCoin (cointRemain);
			GameStatic.logUseCoin (GameStatic.currentMode, GameStatic.currentLevel, coinNeed, cointRemain, false);
			GameStatic.saveGameForReplay();
			//hideBannerView ();
		} else {
			ToastManager.showToast(StringUtils.message_not_enough_ruby);
			Popup popupScript = GameStatic.store.GetComponent<Popup> ();
            popupScript.showPopup ();
		}
	}
	/** When user eat three pokemon same time and one has no pokemon to eat with*/
	public void checkWrongOnlineEat(int id){
		int num = 0;
		Vec2 lastPos = null;
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j++) {
				if(MAP[i][j] != -1) {
					if (MAP [i] [j] == id) {
						num++;
						lastPos = new Vec2 (i, j);
					}
				}
			}
		}
		if(num% 2 == 1 && lastPos != null){
			getPokemon (lastPos.R, lastPos.C).GetComponent<Pokemon> ().Eat (false);
		}
	}

	// get list cell remain 1 pokemon
	private ArrayList getListPokemonRemainOne() {
		Hashtable hash = new Hashtable ();
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col -1; j++) {
				if (MAP [i] [j] != -1) {
					string key = "key_" + MAP [i] [j];
					if (hash.Contains (key)) {
						ArrayList list_pokemon = (ArrayList) hash [key];
						list_pokemon.Add (new Vec2 (i, j));
					} else {
						ArrayList list_pokemon = new ArrayList ();
						list_pokemon.Add (new Vec2 (i, j));
						hash.Add (key, list_pokemon);
					}
				}
			}
		}

		ArrayList list = new ArrayList ();
		foreach (string key in hash.Keys) {
			ArrayList list_pokemon = (ArrayList)hash [key];
			if (list_pokemon.Count == 1) {
				Vec2 pos = (Vec2) list_pokemon [0];
				list.Add (pos);
			}
		}
		return list;
	}

	public List<Pokemon> getListPokemon(){
		List<Pokemon> list = new List<Pokemon> ();
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col -1; j++) {
				if (MAP [i] [j] != -1 && getPokemon(i,j) != null) {
					list.Add(getPokemonClass(i,j));
				}
			}
		}
		return list;
	}

	//remove random one pokemon - test
	private void removeRandomOnePokemon() {
		for (int i = 1; i < row; i++) {
			for (int j = 1; j < col; j++) {
				if (MAP [i] [j] != -1) {
					RemovePokemon (new Vec2 (i, j));
					MAP [i] [j] = -1;
					return;
				}
			}
		}
	}

	public void reloadUI() {
		Map.order = 0;
		for (int i = row -2; i > 0; i--) {
			for (int j = 1; j < col -1; j++) {
				Pokemon poke = getPokemonClass (i, j);
				if (poke != null) {
					poke.reloadUI ();
					poke.setOrder ();
				}
			}
		}
	}

	public void autoSelect(){
		POS1 = null;
		POS2 = null;
		int count = list_pokemon_can_eat1.Count;
		if (count == 0) {
			return;
		}
		int r = Random.Range (0, count);
		if (r == count) {
			r = count - 1;
		}
		Vec2 pos1 = (Vec2)list_pokemon_can_eat1 [r];
		Vec2 pos2 = (Vec2)list_pokemon_can_eat2 [r];
		Select(pos1);
		Select(pos2);
		list_pokemon_can_eat1.RemoveAt (r);
		list_pokemon_can_eat2.RemoveAt (r);
		CheckPair(pos2);
	}

	public void nextLevel(){
		if (GameStatic.currentLevel <= GameStatic.maxLevel) {
			logicLevel.nextLevel ();
			_replay ();
			SoundSystem.ins.play_music_back ();
		}
	}

	// khoi tao lai map
	public void Replay() {
		SoundSystem.ins.play_music_back ();
		logicLevel.replay ();
		_replay ();
	}

	private void _replay(){
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j++) {
				RemovePokemon(new Vec2(i, j));
			}
		}
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j++) {
				removeFrozen (i, j, false);
			}
		}
		list_frozen_put.Clear();
		list_block_frozen.Clear();
		for (int i = 0; i < listAutoGens.Count; i++) {
			AutoGenController autoGenControler = (AutoGenController)listAutoGens [i];
			DestroyImmediate (autoGenControler.gameObject);
		}
		listAutoGens.Clear ();
		_randomMap ();
		isGameOver = false;
	}

	public int getNumberTitleCurrent() {
		int number = 0;
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j++) {
				if(MAP[i][j] != -1 && MAP[i][j] != Const.STONE_FIXED_ID
					&& MAP[i][j] != Const.STONE_MOVING_ID) {
					number ++;
				}
			}
		}
		return number;
	}

	public Vector2 getUIPosition(Vec2 pos){
		Vector3 worldPos = POS [pos.R] [pos.C];
		Vector3 viewPortPos = Camera.main.WorldToViewportPoint (worldPos);

		Vector2 WorldObject_ScreenPosition=new Vector2(
			((viewPortPos.x*GameStatic.canvas.sizeDelta.x)-(GameStatic.canvas.sizeDelta.x*0.5f)),
			((viewPortPos.y*GameStatic.canvas.sizeDelta.y)-(GameStatic.canvas.sizeDelta.y*0.5f)));
		return WorldObject_ScreenPosition;
	}

	public float getUISize(){
		Vector3 worldPos = new Vector3 (CELL_WIDH, 0, 0);
		Vector3 viewPortPos = Camera.main.WorldToViewportPoint (worldPos);

		Vector2 WorldObject_ScreenPosition=new Vector2(
			((viewPortPos.x*GameStatic.canvas.sizeDelta.x)),
			((viewPortPos.y*GameStatic.canvas.sizeDelta.y)));
		return WorldObject_ScreenPosition.x;
	}

	public void checkTitleCurrentRemainOne() {
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j++) {
				if(MAP[i][j] != -1 && MAP[i][j] != Const.STONE_FIXED_ID
					&& MAP[i][j] != Const.STONE_MOVING_ID) {
					check_id.Add (getPokemonClass (i, j).id);
				}
			}
		}
	}

	public void LMap(int row, int col)
	{
		float camHalfHeight = Camera.main.orthographicSize;
		float camHalfWidth = Camera.main.aspect * camHalfHeight;
		MAP_WIDTH = (int)camHalfWidth * 2;
		MAP_HEIGHT = (int)camHalfHeight * 2;
		CELL_WIDH = (int)(MAP_WIDTH / (col-2));
        CELL_HEIGHT = (int)( (MAP_HEIGHT - 48) / (row - 2));
        Debug.Log("Cell_height : " + CELL_HEIGHT);
        //		CELL_HEIGHT = (int)(Screen.height - 80) / row;
        //		CELL_WIDH = Mathf.Min (CELL_WIDH, CELL_HEIGHT);
        if (CELL_WIDH < CELL_HEIGHT)
        {
            CELL_HEIGHT = CELL_WIDH;
        }
        else
        {
            CELL_WIDH = CELL_HEIGHT;
        }
        
		Debug.Log ("Screen_width : " + camHalfWidth);
		Debug.Log ("Screen_height : " + camHalfHeight);
		Debug.Log ("Cell_width : " + CELL_WIDH);
		Debug.Log ("Cell_height : " + CELL_HEIGHT);

//		CELL_HEIGHT = (int)((320f  - top_height ) / (row - 2));
//		CELL_WIDH = (int)(CELL_HEIGHT * 0.9f);
		
		MAP = new int[row][];
		MAP_FROZEN = new int[row][];
		MAP_POKEMONS = new GameObject[row][];
		POS = new Vector3[row][];
		
		MIN_X = -col * CELL_WIDH / 2;
		MIN_Y = -(int)camHalfHeight -CELL_HEIGHT/2 + GameConfig.map_margin_bottom ;
		
		
		for (int i = 0; i < row; i++)
		{
			MAP[i] = new int[col];
			MAP_FROZEN[i] = new int[col];
			MAP_POKEMONS[i] = new GameObject[col];
			POS[i] = new Vector3[col];
			for (int j = 0; j < col; j++)
			{
				MAP[i][j] = -1;
				MAP_FROZEN[i][j]= -1;
				MAP_POKEMONS[i][j] = null;
				POS[i][j] = new Vector3(0, 0, 0);
				POS[i][j].x = MIN_X + j * CELL_WIDH + CELL_WIDH / 2;
				POS[i][j].y = MIN_Y + i * CELL_HEIGHT  ;
				POS[i][j].z = i / 10.0f;
			}
		}
		Debug.Log ("khoi tao map");
	}

	// khoi tao map
	private void _randomMap()
	{
		Debug.Log ("::::Random MAP::::");

		mapState = MapState.none;

		ArrayList list_pokemon_fixed = logicLevel.levelData.list_block_pokemon_fixed;
		ArrayList list_stone_fixed   = logicLevel.levelData.list_block_stone_fixed;
		ArrayList list_stone_moving  = logicLevel.levelData.list_block_stone_moving;
		list_block_frozen = logicLevel.levelData.list_block_frozen_fixed;
		
		int total_pokemon = (row - 2) * (col - 2) - list_stone_fixed.Count - list_stone_moving.Count - list_pokemon_fixed.Count;
		int total_pokemon_type = GameConfig.total_pokemon_kind;
		int number_pokemon_4 = (total_pokemon - 2 * total_pokemon_type) / 2;
		int number_pokemon_2 = total_pokemon_type - number_pokemon_4;
		
		ArrayList list_pokemon = new ArrayList ();
		for (int i = 0; i < number_pokemon_4; i++) {
			for(int j = 0; j < 4; j++){
				list_pokemon.Add(i);
			}
		}

		for (int i = number_pokemon_4; i < total_pokemon_type; i++) {
			for(int j = 0; j < 2; j++){
				list_pokemon.Add(i);
			}
		}

		int list_pk_count = list_pokemon.Count;
		int temp2 = (row - 2) * (col - 2) - list_stone_fixed.Count - list_stone_moving.Count - list_pk_count - list_pokemon_fixed.Count;

		if (temp2 % 2 != 0) {
			Debug.Log("Amount of pokemon must be even");
			return;
		}
		int max_id = (int)list_pokemon[list_pk_count - 1];
		
		for (int i = 0; i < temp2; i++) {
			list_pokemon.Add(max_id + 1);
		}
		list_pk_count = list_pokemon.Count;
		Debug.Log(list_stone_moving.Count);
		for (int i = 0; i < list_stone_fixed.Count; i++) {
			Vec2 pos = (Vec2) list_stone_fixed[i];
			AddSpecialItem(Const.STONE_FIXED_ID , pos.R, pos.C);
			MAP[pos.R][pos.C] = Const.STONE_FIXED_ID;
		}

		for (int i = 0; i < list_stone_moving.Count; i++) {
			Vec2 pos = (Vec2) list_stone_moving[i];
			AddSpecialItem(Const.STONE_MOVING_ID , pos.R, pos.C);
			MAP[pos.R][pos.C] = Const.STONE_MOVING_ID;
		}
		Debug.Log(list_pokemon_fixed.Count);
		for (int i = 0; i < list_pokemon_fixed.Count; i++) {
			Vector3 pokemon = (Vector3) list_pokemon_fixed[i];
			int p_row = (int) pokemon.x;
			int p_col = (int) pokemon.y;
			int p_id  = (int) pokemon.z;
			AddPokemon(p_id , p_row, p_col);
			MAP[p_row][p_col] = p_id;
		}

			for (int i = row -2; i > 0; i--)
//			for (int i = 1; i < row - 1; i++)
		{
			for (int j = 1; j < col - 1; j++)
			{
				if (MAP[i][j] == -1) {
					int r = (int)(UnityEngine.Random.value * list_pk_count);
					int type = (int)list_pokemon[r];
					AddPokemon(type , i, j);
					MAP[i][j] = type;
					list_pokemon.RemoveAt(r);
					list_pk_count--;
				}
				getPokemonClass (i, j).setOrder ();
			}
		}

		for (int i = 0; i < list_block_frozen.Count; i++) {
			Vec2 pos = (Vec2) list_block_frozen[i];
			AddSpecialItem(Const.FROZEN_FIXED_ID, "frozen_" , pos.R, pos.C);
		}

		for (int i = 0; i < logicLevel.levelData.list_auto_gen.Count; i++) {
			AutoGenData auto_gen_data = (AutoGenData)logicLevel.levelData.list_auto_gen [i];
			genAutoGen (auto_gen_data);
		}

		updateListFrozen();

		int numberPokemonCanEat = getNumberPokemonCanEat ();
		if (numberPokemonCanEat == 0) {
			_resetMap ();
		} else {
			changeState(MapState.playing);
		}
	}

	public void RandomMap(){
		_resetMap ();
	}

	// random lai map choi
	public void _resetMap(){
		if (checkGameOverByAllFrozen()) {
			isGameOver = true;
			StartCoroutine (showRecoverLifePopupDelay());
			sendHighScore();
			GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore, false);
			return;
		}
		_numberResetMap++;
		if (_numberResetMap > 500)
			return;
		RemoveHint ();
		Debug.Log(":::::Reset MAP::::");
		//get list pokemon and stone in map
		ArrayList list_pokemon = new ArrayList ();
		ArrayList list_stone_moving = new ArrayList();
		ArrayList list_slot_no_frozen = new ArrayList();
		ArrayList list_slot_has_frozen = new ArrayList();
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j ++) {
				if(MAP[i][j] != -1 && MAP[i][j] != Const.STONE_FIXED_ID) {
					if (MAP[i][j] == Const.STONE_MOVING_ID) {
						list_stone_moving.Add(getPokemon(i, j));
					} else {
						list_pokemon.Add(getPokemon(i, j));
					}
				}
			}
		}
		//get list slot avaiable
		for (int i = 1; i < row - 1; i++)
		{
			for (int j = 1; j < col - 1; j++)
			{
				if(MAP[i][j] != -1 && MAP[i][j] != Const.STONE_FIXED_ID){
					if (MAP_FROZEN[i][j] == -1) {
						list_slot_no_frozen.Add(new Vec2(i, j));
					} else {
						list_slot_has_frozen.Add(new Vec2(i, j));
					}
				}
			}
		}
		list_slot_no_frozen = getRandomList(list_slot_no_frozen);
		list_slot_has_frozen = getRandomList(list_slot_has_frozen);
		//random list stone moving
		for (int i = 0; i < list_stone_moving.Count; i++) {
			GameObject obj = (GameObject)list_stone_moving [i];
			Pokemon pokemon = obj.GetComponent<Pokemon>();
			int type = pokemon.id;
			Vec2 pos = (Vec2) list_slot_no_frozen[0];
			changePokemon(pokemon, type, pos);
			MAP_POKEMONS [pos.R] [pos.C] = obj;
			MAP[pos.R][pos.C] = type;
			list_slot_no_frozen.RemoveAt (0);
			pokemon.setFrozen (false);
		}

		ArrayList list_slot = new ArrayList ();
		list_slot.AddRange (list_slot_no_frozen);
		list_slot.AddRange (list_slot_has_frozen);
		for (int i = 0; i < list_pokemon.Count; i++) {
			GameObject obj = (GameObject)list_pokemon [i];
			Pokemon pokemon = obj.GetComponent<Pokemon>();
			int type = pokemon.id;
			Vec2 pos = (Vec2) list_slot[i];
			if (MAP_FROZEN [pos.R] [pos.C] == -1) {
				pokemon.setFrozen (false);
			} else {
				pokemon.setFrozen (true);
			}
			changePokemon(pokemon, type, pos);
			MAP_POKEMONS [pos.R] [pos.C] = obj;
			MAP[pos.R][pos.C] = type;
		}
		int numberPokemonCanEat = getNumberPokemonCanEat ();
		if (numberPokemonCanEat == 0) {
			_resetMap();
		} else {
			reloadUI ();
			_numberResetMap = 0;
			isReseting = false;
		}
	}

	public void gameOver(bool timeOver =false){
		isGameOver = true;
		deadTypeTimeOver = timeOver;
		StartCoroutine (showRecoverLifePopupDelay());
		sendHighScore();
		GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore, false);
	}

	//load old map from save game
	public void _loadOldMap(JSONNode data) {
		RemoveHint ();
		Debug.Log (":::::MAP DATA :: " + data.ToString ());
		JSONNode mapData = data ["map_data"].AsObject;
		JSONNode mapFrozenData = data ["map_frozen_data"].AsObject;
		JSONArray autoGensData = data ["auto_gen"].AsArray;
		if (autoGensData.Count > 0) {
			for (int i = 0; i < listAutoGens.Count; i++) {
				AutoGenController autoGenController = (AutoGenController) listAutoGens [i];
				DestroyImmediate (autoGenController.gameObject);
			}
			listAutoGens.Clear ();
		}
		list_block_frozen.Clear();
		logicLevel.levelData.loadListBlockStoneAndFrozen (true, false);
		for (int i = row - 2; i > 0; i--)
		{
			for (int j = 1; j < col - 1; j++)
			{
				int id = mapData [i + ""] [j + ""].AsInt;
				int frozen = mapFrozenData [i + ""] [j + ""].AsInt;
				if (id != -1) {
					GameObject pokemonObj = getPokemon (i, j);
					Pokemon pokemon = pokemonObj.GetComponent<Pokemon>();
					if (frozen > 0) {
						if (getFrozen(i, j) == null) {
							AddSpecialItem(Const.FROZEN_FIXED_ID, "frozen_" , i, j);
						} else {
							MAP_FROZEN[i][j] = Const.FROZEN_FIXED_ID;
						}
						pokemon.setFrozen (true);
						list_block_frozen.Add (new Vec2 (i, j));
						logicLevel.levelData.list_block_stone_and_frozen.Add (new Vec2 (i, j));
					} else {
						pokemon.setFrozen (false);
						removeFrozen (i, j, false);
					}
					changePokemon(pokemon, id, new Vec2(i, j));
					MAP_POKEMONS [i] [j] = pokemonObj;
					MAP[i][j] = id;
				} else {
					RemovePokemon(new Vec2(i, j));
					removeFrozen (i, j, false);
					MAP_FROZEN[i][j] = -1;
				}
			}
		}
		for (int i = 0; i < autoGensData.Count; i++) {
			JSONNode autoGenData = autoGensData [i].AsObject;
			float currentTime = autoGenData ["current_time"].AsFloat;
			float currentClockTime = autoGenData ["current_clock_time"].AsFloat;
			int type = autoGenData ["type"].AsInt;
			float timeWait = autoGenData ["time_wait"].AsFloat;
			float timeRun = autoGenData ["time_run"].AsFloat;
			int row = autoGenData ["row"].AsInt;
			int col = autoGenData ["col"].AsInt;
			AutoGenData autoGenDataBase = new AutoGenData(type, timeWait, timeRun);
			genAutoGenFix (autoGenDataBase, currentTime, currentClockTime, new Vec2(row, col));
		}
		updateListFrozen();
		int numberPokemonCanEat = getNumberPokemonCanEat ();
		if (numberPokemonCanEat == 0) {
			if (checkGameOver()) {
				isGameOver = true;
				GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore, false);
				sendHighScore();
				StartCoroutine (showRecoverLifePopupDelay());
			} else {
				_resetMap();	
			}
		} else {
			isReseting = false;
			reloadUI ();
		}
	}

	public void ItemRandomMap(){
		if (mapState == MapState.eating)
			return;
        if (randomController.useRandomItem())
        {
            SoundSystem.ins.play_sound_shuffle();
			_resetMap ();
		}
	}

	public int getNumberPokemonCanEat() {
		numberPokemonRemain = 0;
		int number = 0;
		list_pokemon_can_eat1.Clear();
		list_pokemon_can_eat2.Clear();
		for (int i1 = 1; i1 < row - 1; i1++) {
			for (int j1 = 1; j1 < col - 1; j1++) {
				if(MAP[i1][j1] != -1 && MAP[i1][j1] != Const.STONE_FIXED_ID
					&& MAP[i1][j1] != Const.STONE_MOVING_ID && MAP_FROZEN[i1][j1] == -1) {
					for (int i2 = 1; i2 < row - 1; i2 ++) {
						for (int j2 = 1; j2 < col - 1; j2 ++) {
							if (i2 <= i1 && j2 <= j1) continue;
							if (MAP_FROZEN[i2][j2] != -1) continue;
							if(MAP[i2][j2] == MAP[i1][j1]) {
								HINT_POS1 = new Vec2(i1, j1);
								HINT_POS2 = new Vec2(i2, j2);
								if(checkPaire(HINT_POS1, HINT_POS2, false)) {
									list_pokemon_can_eat1.Add(HINT_POS1);
									list_pokemon_can_eat2.Add(HINT_POS2);
									number++;
								}
							}
						}
					}
				}
				//get number pokemon remain
				if(MAP[i1][j1] != -1 && MAP[i1][j1] != Const.STONE_FIXED_ID
					&& MAP[i1][j1] != Const.STONE_MOVING_ID) {
					numberPokemonRemain ++;
				}
			}
		}
		return number;
	}

	bool clearMap () {
		for (int i1 = 1; i1 < row - 1; i1++) {
			for (int j1 = 1; j1 < col - 1; j1++) {
				if(MAP[i1][j1] != -1 && MAP[i1][j1] != Const.STONE_FIXED_ID && MAP[i1][j1] != Const.STONE_MOVING_ID) {
					return false;
				}
			}
		}
		return true;
	}

	void Select(Vec2 pos)
	{
		if (checking_paire) {
			return;
		}
		if (pos.R < 1 || pos.R > row - 1 || pos.C < 1 || pos.C > col - 1) {
			return;
		}
		if (MAP [pos.R] [pos.C] == -1 || MAP[pos.R][pos.C] == Const.STONE_FIXED_ID
		    || MAP[pos.R][pos.C] == Const.STONE_MOVING_ID) {
			return;
		}
		if (MAP_FROZEN [pos.R] [pos.C] != -1) {
			return;
		}
		GameObject obj = getPokemon (pos.R, pos.C);
		Pokemon pokemon = obj.GetComponent<Pokemon>();
		if (pokemon.getState() != Const.STATE_NORMAL) {
			return;
		}
		if(POS1 != null && POS1.C == pos.C && POS1.R == pos.R){
			DeSelect();
			SoundSystem.ins.playDeSelect ();	
		} else if (POS1 == null) {
			POS1 = new Vec2 (pos.R, pos.C);
			if (obj != null) {
				obj.GetComponent<Pokemon>().Select();
				RemoveHint();
			}
			SoundSystem.ins.playSelect ();	
		} else if (POS2 == null) {
			POS2 = new Vec2 (pos.R, pos.C);
			if (obj != null) {
				obj.GetComponent<Pokemon>().Select();
				RemoveHint();
				CheckPair(pos);
			}
		} else {
			return;
		}
	}
	void DeSelect()
	{
		CustomDebug.Log("DESELECT :::");
		GameObject pokemon = null;
		if (POS1 != null) {
			pokemon = getPokemon (POS1.R, POS1.C);
		}
		if (pokemon != null) {
			pokemon.GetComponent<Pokemon>().DeSelect();
		}
		if (POS2 != null) {
			pokemon = getPokemon(POS2.R, POS2.C);
		}
		if (pokemon != null) {
			pokemon.GetComponent<Pokemon>().DeSelect();
		}
		POS1 = null;
		POS2 = null;
	}

	public void Hint(){
		int count = list_pokemon_can_eat1.Count;
		if (count == 0) {
			return;
		}
		int r = Random.Range (0, count);
		if (r == count) {
			r = count - 1;
		}
		HINT_POS1 = (Vec2)list_pokemon_can_eat1 [r];
		HINT_POS2 = (Vec2)list_pokemon_can_eat2 [r];
        if (!isTutorial)
        {
            if (!hintController.useHintItem() && !GameConfig.DEBUG_KEY)
            {
                return;
            }
        }
        if (GameConfig.DEBUG_KEY && !isTutorial) {
			POS1 = HINT_POS1;
			Select (HINT_POS2);
			return;
		}
		GameObject pokemon = null;
		if (HINT_POS1 != null) {
			pokemon = getPokemon (HINT_POS1.R, HINT_POS1.C);
		}
		if (pokemon != null) {
			pokemon.GetComponent<Pokemon>().Hint();
		}
		if (HINT_POS2 != null) {
			pokemon = getPokemon(HINT_POS2.R, HINT_POS2.C);
		}
		if (pokemon != null) {
			pokemon.GetComponent<Pokemon>().Hint();
		}

	}

	void RemoveHint(){
		GameObject pokemon = null;
		if (HINT_POS1 != null) {
			pokemon = getPokemon (HINT_POS1.R, HINT_POS1.C);
		}
		if (pokemon != null) {
			pokemon.GetComponent<Pokemon>().RemoveHint();
		}
		if (HINT_POS2 != null) {
			pokemon = getPokemon(HINT_POS2.R, HINT_POS2.C);
		}
		if (pokemon != null) {
			pokemon.GetComponent<Pokemon>().RemoveHint();
		}
	}

	void CheckPair(Vec2 pos)
	{
		POS2 = new Vec2(pos.R, pos.C);
		if (MAP_FROZEN[pos.R][pos.C] != -1) {
			return;
		}
		if (POS1 != null && MAP [POS1.R] [POS1.C] != MAP [POS2.R] [POS2.C]) {
			SoundSystem.ins.play_sound_cant_move ();
			DeSelect();
			POS1 = null;
			POS2 = null;
			LPath.Clear ();
			keyLPath.Clear ();
		} else if (checkPaire (POS1, POS2, true)) {
			logicLevel.updateScore(nextScore);
			pathSystem.draw (LPath,false);
			eat(POS1,POS2,true);
		} else {
			SoundSystem.ins.play_sound_cant_move ();
			DeSelect();
			POS1 = null;
			POS2 = null;
			LPath.Clear ();
			keyLPath.Clear ();
		}
	}

	public void eat(Vec2 pos1, Vec2 pos2, bool isOffline){
		extraPeriodScore += 5;
		SoundSystem.ins.playEat();
		if (!isOffline) {
			CustomDebug.p.log ("eat pos: (" + pos1.R+"," + pos1.C+")");
		}
		changeState (MapState.eating);
		getPokemon(pos1.R, pos1.C).GetComponent<Animator> ().SetTrigger ("Dissapear");
		getPokemon(pos2.R, pos2.C).GetComponent<Animator> ().SetTrigger ("Dissapear");
		if (isOffline) {
			StartCoroutine (execute_check_paire (pos1, pos2));
		} else {
			StartCoroutine (execute_check_paire_online (pos1, pos2));
		}
	}

	IEnumerator execute_check_paire(Vec2 pos1, Vec2 pos2){
		yield return new WaitForSeconds (0.4f);
		RemovePokemon(pos1);
		RemovePokemon(pos2);
		UpdateFrozenState (pos1);
		UpdateFrozenState (pos2);
		updateClockState (pos1);
		updateClockState (pos2);
		updateMap (true);
	}

	IEnumerator execute_check_paire_online(Vec2 pos1, Vec2 pos2){
		yield return new WaitForSeconds (0.4f);
		RemovePokemon(pos1);
		RemovePokemon(pos2);
		updateMap (false);
	}

	public void updateMap(bool isOffline){
		if (clearMap ()) {
			if (mapState == MapState.result) {
				return;
			}
			changeState (MapState.result);
			Debug.Log ("+++++++++++++++++++++++++++++++++++++++++clear map+++++++++++++++++++++++++++++++++++++++++++++++++");
			DeSelect ();
			LPath.Clear ();
			keyLPath.Clear ();
			logicLevel.collectReward ();
			GameStatic.canShowRatePopup = true;
			//showBannerView();
			if (GameStatic.currentLevel == GameStatic.maxLevel) {
				// final win
				logicLevel.updateScore (logicLevel.getScoreBonus () + GameConfig.bonus_victory);
				resultBar.showResult (timeBar.getNumStar (), logicLevel.getScore (), GameStatic.currentLevel, true);
				GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 1, GameStatic.currentScore, false);
				GameStatic.endGame();
				sendHighScore ();
			} else {
				logicLevel.updateScore (logicLevel.getScoreBonus ());
				resultBar.showResult (timeBar.getNumStar (), logicLevel.getScore (), GameStatic.currentLevel, false);
				GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 1, GameStatic.currentScore, false);
				GameStatic.saveGameWithoutMap();
				//GameStatic.postScore (GameStatic.currentScore);
				sendHighScore ();
			}
			Save.countLevelPass();
		} else {
			StartCoroutine(updateLogicLevel(isOffline));
		}
	}

	IEnumerator updateLogicLevel(bool isOffline) {
		yield return new WaitForSeconds (Time.deltaTime);
		logicLevel.list_pos_need_update.Add (POS1);
		logicLevel.list_pos_need_update.Add (POS2);
		logicLevel.updateMap();
		StartCoroutine(end_update_map(isOffline));
	}

	IEnumerator end_update_map(bool isOffline) {
		yield return new WaitForSeconds (0.1f);
		if (!isTutorial) {
			GameStatic.saveGame();
		}
		if (isOffline) {
			DeSelect ();
			LPath.Clear ();
			keyLPath.Clear ();
		}
		if (checkGameOver()) {
			isGameOver = true;
			lastTime = currentTime;
			Analytics.CustomEvent ("gameOver", new Dictionary<string, object>{
				{"score",GameStatic.currentScore},
				{"level",GameStatic.currentLevel}
			});
			GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore,false);
			sendHighScore ();
		}
		int numberPokemonCanEat = getNumberPokemonCanEat ();
		Debug.Log("numberPokemonCanEat : " + numberPokemonCanEat);
		if(!isReseting && numberPokemonCanEat == 0 && numberPokemonRemain > 0) {
			StartCoroutine(StartResetMap());
		}
		if(mapState == MapState.eating)
			GameStatic.map.changeState (Map.MapState.playing);
	}

	public void UpdateFrozenState(Vec2 POS) {
		if (POS == null) return;
		bool isRemove = false;
		if (POS.R > 1) {
			if (MAP_FROZEN[POS.R - 1][POS.C] != -1) {
				MAP_FROZEN[POS.R - 1][POS.C] = -1;
				removeFrozen(POS.R - 1, POS.C, true);
				updateDataFrozen (POS.R - 1, POS.C);
				//logicLevel.updateMap (new Vec2 (POS.R - 1, POS.C));
				logicLevel.list_pos_need_update.Add (new Vec2(POS.R - 1, POS.C));
				isRemove = true;
			}
		}
		if (POS.R < row - 2) {
			if (MAP_FROZEN[POS.R + 1][POS.C] != -1) {
				MAP_FROZEN[POS.R + 1][POS.C] = -1;
				removeFrozen(POS.R + 1, POS.C, true);
				updateDataFrozen(POS.R + 1, POS.C);
				//logicLevel.updateMap (new Vec2 (POS.R + 1, POS.C));
				logicLevel.list_pos_need_update.Add (new Vec2(POS.R + 1, POS.C));
				isRemove = true;
			}
		}
		if (POS.C > 1) {
			if (MAP_FROZEN[POS.R][POS.C - 1] != -1) {
				MAP_FROZEN[POS.R][POS.C - 1] = -1;
				removeFrozen(POS.R, POS.C - 1, true);
				updateDataFrozen(POS.R, POS.C - 1);
				//logicLevel.updateMap (new Vec2 (POS.R, POS.C - 1));
				logicLevel.list_pos_need_update.Add (new Vec2(POS.R, POS.C - 1));
				isRemove = true;
			}
		}
		if (POS.C < col - 2) {
			if (MAP_FROZEN[POS.R][POS.C + 1] != -1) {
				MAP_FROZEN[POS.R][POS.C + 1] = -1;
				removeFrozen(POS.R, POS.C + 1, true);
				updateDataFrozen (POS.R, POS.C + 1);
				//logicLevel.updateMap (new Vec2 (POS.R, POS.C + 1));
				logicLevel.list_pos_need_update.Add (new Vec2(POS.R, POS.C + 1));
				isRemove = true;
			}
		}
		if (isRemove) {
			updateListFrozen();
		}
	}

	void updateDataFrozen(int row, int col) {
		for (int i = 0; i < logicLevel.levelData.list_block_stone_and_frozen.Count; i++) {
			Vec2 pos = (Vec2)logicLevel.levelData.list_block_stone_and_frozen [i];
			if (pos.R == row && pos.C == col) {
				logicLevel.levelData.list_block_stone_and_frozen.RemoveAt (i);
				return;
			}
		}
	}

	void resetCamera() {
		cameraController.resetCamera();
		finger.transform.position = new Vector3 (9999, 9999, 0);
	}

	void zoomToFrozenWhenGameOver(Vec2 pos) {
		Vector2 posZoom = getUIPosition (pos);
		Vector3 zoomPos = new Vector3 (posZoom.x, posZoom.y, 1);
		cameraController.ZoomOrthoCamera (zoomPos, 80, 1.3f);
		ToastManager.showToast (StringUtils.can_not_eat_ice, false, 5);
		GameObject objPk = getPokemon (pos.R, pos.C);
		if (objPk != null) {
			finger.transform.SetParent (transform);
			if (pos.R > 3) {
				finger.transform.position = new Vector3 (objPk.transform.position.x, objPk.transform.position.y - 35, 0);
				finger.transform.rotation = new Quaternion (0, 0, 0, 0);
			} else {
				finger.transform.position = new Vector3 (objPk.transform.position.x, objPk.transform.position.y + 25, 0);
				finger.transform.rotation = new Quaternion (180, 0, 0, 0);
			}
		}
	}

	bool checkGameOver() {
		if (checkGameOverByAllFrozen()) {
			return true;
		}
		for (int i = 0; i < list_block_frozen_normal.Count; i++) {
			Vec2 pos = (Vec2)list_block_frozen_normal [i];
			if (!canDestroyFrozen (pos)) {
				zoomToFrozenWhenGameOver (pos);
//				tutorialObj.SetActive (true);
//				tutorialObj.transform.SetParent(getPokemon(pos.R, pos.C).transform);
				return true;
			}
		}
		if (list_block_frozen_special.Count == 0) return false;
		for (int i = 0; i < list_block_frozen_special.Count; i++) {
			Hashtable list_frozen_special = (Hashtable) list_block_frozen_special[i];
			bool check = true;
			Vec2 posFrozen = new Vec2();
			foreach (string key in list_frozen_special.Keys) {
				Vec2 pos = (Vec2) list_frozen_special[key];
				if (canDestroyFrozen (pos)) {
					check = false;
					break;
				} else {
					posFrozen = pos;
				}
			}
			if (check) {
				zoomToFrozenWhenGameOver (posFrozen);
//				tutorialObj.SetActive (true);
//				tutorialObj.transform.SetParent(getPokemon(posFrozen.R, posFrozen.C).transform);
				return true;
			}
		}
		
		return false;
	}

	bool checkGameOverByAllFrozen() {
		int count = 0;
		for (int i = 1; i < row - 1; i++)
		{
			for (int j = 1; j < col - 1; j++)
			{
				if(MAP_FROZEN[i][j] == -1 && MAP[i][j] != Const.STONE_MOVING_ID
					&& MAP[i][j] != Const.STONE_FIXED_ID && MAP[i][j] != -1) {
					count ++;
					if (count > 1) {
						break;
					}
				}
			}
			if (count > 1) {
				break;
			}
		}
		if (count < 2) {
			return true;
		}
		return false;
	}

	bool isSingleFrozen (Vec2 POS) {
		if (POS.R > 1) {
			if (MAP_FROZEN[POS.R - 1] [POS.C] != -1) {
				return false;
			}
		}
		if (POS.R < row - 1) {
			if (MAP_FROZEN[POS.R + 1] [POS.C] != -1) {
				return false;
			}
		}
		if (POS.C > 1) {
			if (MAP_FROZEN[POS.R] [POS.C - 1] != -1) {
				return false;
			}
		}
		if (POS.C < col - 1) {
			if (MAP_FROZEN[POS.R] [POS.C + 1] != -1) {
				return false;
			}
		}
		return true;
	}

	ArrayList getFrozenAround(Vec2 POS) {
		ArrayList list_frozen = new ArrayList();
		if (POS.R > 1) {
			if (MAP_FROZEN[POS.R - 1] [POS.C] != -1) {
				list_frozen.Add(new Vec2(POS.R - 1, POS.C));
			}
		}
		if (POS.R < row - 1) {
			if (MAP_FROZEN[POS.R + 1] [POS.C] != -1) {
				list_frozen.Add(new Vec2(POS.R + 1, POS.C));
			}
		}
		if (POS.C > 1) {
			if (MAP_FROZEN[POS.R] [POS.C - 1] != -1) {
				list_frozen.Add(new Vec2(POS.R, POS.C - 1));
			}
		}
		if (POS.C < col - 1) {
			if (MAP_FROZEN[POS.R] [POS.C + 1] != -1) {
				list_frozen.Add(new Vec2(POS.R, POS.C + 1));
			}
		}
		return list_frozen;
	}

	bool canDestroyFrozen(Vec2 POS) {
		if (POS == null) return true;
		if (POS.R > 1) {
			if (MAP_FROZEN[POS.R - 1] [POS.C] == -1 && MAP[POS.R - 1] [POS.C] != -1 
				&& MAP[POS.R - 1][POS.C] != Const.STONE_FIXED_ID
				// && MAP[POS.R - 1][POS.C] != Const.STONE_MOVING_ID
				) {
				return true;
			}
		}
		if (POS.R < row - 1) {
			if (MAP_FROZEN[POS.R + 1] [POS.C] == -1 && MAP [POS.R + 1] [POS.C] != -1
				&& MAP[POS.R + 1][POS.C] != Const.STONE_FIXED_ID
				// && MAP[POS.R + 1][POS.C] != Const.STONE_MOVING_ID
				) {
				return true;
			}
		}
		if (POS.C > 1) {
			if (MAP_FROZEN[POS.R] [POS.C - 1] == -1 && MAP [POS.R][POS.C - 1] != -1
				&& MAP[POS.R][POS.C - 1] != Const.STONE_FIXED_ID
				// && MAP[POS.R][POS.C - 1] != Const.STONE_MOVING_ID
				) {
				return true;
			}
		}
		if (POS.C < col - 1) {
			if (MAP_FROZEN[POS.R] [POS.C + 1] == -1 && MAP [POS.R][POS.C + 1] != -1
				&& MAP[POS.R][POS.C + 1] != Const.STONE_FIXED_ID
				// && MAP[POS.R][POS.C + 1] != Const.STONE_MOVING_ID
				) {
				return true;
			}
		}
		return false;
	}

	private void updateListFrozen() {
		list_block_frozen_normal.Clear();
		list_block_frozen_special.Clear();
		if (list_block_frozen.Count == 0) return;
		Hashtable list_frozen_multi = new Hashtable();
		for (int i = 0; i < list_block_frozen.Count; i++) {
			Vec2 pos = (Vec2) list_block_frozen[i];
			if (isSingleFrozen(pos)) {
				list_block_frozen_normal.Add(pos);
			} else {
				list_frozen_multi.Add(pos.R + "_" + pos.C, pos);
			}
		}
		bool isCreated = false;
		bool isCalculator = list_frozen_multi.Count > 0 ? true : false;
		while(isCalculator) {
			Hashtable list_frozen_special;
			if (!isCreated) {
				isCreated = true;
				list_frozen_special = new Hashtable();
				list_block_frozen_special.Add(list_frozen_special);
			} else {
				list_frozen_special = (Hashtable) list_block_frozen_special[list_block_frozen_special.Count - 1];
			}
			bool check = false;
			foreach (string key in list_frozen_multi.Keys) {
				Vec2 pos = (Vec2) list_frozen_multi[key];
				string pos_key = pos.R + "_" + pos.C;
				if (list_frozen_special.Count == 0) {
					list_frozen_special.Add(pos_key, pos);
					check = true;
				} else {
					ArrayList list_frozen_around = getFrozenAround(pos);
					bool isAdded = false;
					for (int i = 0; i < list_frozen_around.Count; i++) {
						Vec2 pos_around = (Vec2) list_frozen_around[i];
						string pos_around_key = pos_around.R + "_" + pos_around.C;
						if (list_frozen_special.ContainsKey(pos_around_key) && !list_frozen_special.ContainsKey(pos_key)) {
							list_frozen_special.Add(pos_key, pos);
							check = true;
							isAdded = true;
							break;
						}
					}
					if (isAdded) {
						for (int i = 0; i < list_frozen_around.Count; i++) {
							Vec2 pos_around = (Vec2) list_frozen_around[i];
							string pos_around_key = pos_around.R + "_" + pos_around.C;
							if (!list_frozen_special.ContainsKey(pos_around_key)){
								list_frozen_special.Add(pos_around_key, pos_around);
							}
						}
					}
				}
			}
			foreach (string key in list_frozen_special.Keys) {
				Vec2 pos = (Vec2) list_frozen_special[key];
				list_frozen_multi.Remove(pos.R + "_" + pos.C);
			}
			if (!check) {
				isCreated = false;
			}
			if (list_frozen_multi.Count == 0) {
				isCalculator = false;
			}
		}
	}

	public GameObject getFrozen(int row, int col) {
		return GameObject.Find ("frozen_" + row + "_" + col);
	}

	public void removeFrozen(int row, int col, bool showParticle) {
		GameObject frozen = getFrozen (row, col);
		if (frozen != null) {
			DestroyImmediate (frozen);
			if (showParticle) {
				GameObject instance = null;
				instance = Instantiate(Resources.Load("Prefab/IceBreakParticle", typeof(GameObject))) as GameObject;
				instance.transform.position = POS[row][col];
				instance.transform.SetParent (transform);
				instance.GetComponent<PathItem> ().live (2);
				SoundSystem.ins.play_sound_ice_break ();
			}
		}
		GameObject pokemon = getPokemon (row, col);
		if (pokemon != null) {
			((Pokemon) pokemon.GetComponent<Pokemon>()).setFrozen(false);
		}
		MAP_FROZEN [row] [col] = -1;
		for (int i = 0; i < list_block_frozen.Count; i++) {
			Vec2 pos = (Vec2) list_block_frozen[i];
			if (pos.R == row && pos.C == col) {
				list_block_frozen.RemoveAt(i);
			}
		}
	}

	public GameObject getPokemon(int row, int col){
		return MAP_POKEMONS == null ? null : MAP_POKEMONS[row][col];
	}

	public Pokemon getPokemonClass(int row, int col){
		GameObject obj = getPokemon (row, col);
		if (obj) {
			return obj.GetComponent<Pokemon>();
		}
		return null;
	}

	public GameObject getPokemonById(int id){
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j ++) {
				GameObject gPokemon = getPokemon(i,j);
				Pokemon pokemon =null;
				if(gPokemon !=null){
					pokemon = gPokemon.GetComponent<Pokemon>();
					if(pokemon != null){
						if(pokemon.onlineId == id) return gPokemon;
					}
				}
			}
		}
		return null;
	}

	public void genAutoGen(AutoGenData auto_gen_data) {
		GameObject g = Instantiate(prefap_autoGen) as GameObject;
		AutoGenController autoGenController = g.GetComponent<AutoGenController> ();
		autoGenController.setAutoGenData (auto_gen_data);
		listAutoGens.Add (autoGenController);
	}

	public void genAutoGenFix(AutoGenData auto_gen_data, float currentTime, float currentClockTime, Vec2 pos) {
		GameObject g = Instantiate(prefap_autoGen) as GameObject;
		AutoGenController autoGenController = g.GetComponent<AutoGenController> ();
//		autoGenController.setAutoGenData (auto_gen_data);
//		autoGenController.setCurrentTime (currentTime);
//		autoGenController.pos = pos;
		autoGenController.setAutoGenControllerData(auto_gen_data, currentTime, currentClockTime, pos);
		listAutoGens.Add (autoGenController);
	}

	public void AddPokemon(int type, int row, int col,int online_id =0) {
		if (type == -1)
		return;
		MAP[row][col] = type;
		GameObject g = Instantiate(prefap_pikachu) as GameObject;
		Pokemon pokemon = g.GetComponent<Pokemon> ();
		pokemon.setOnlineId (online_id);
		pokemon.setInfo (type, row, col, POS[row][col], CELL_WIDH, CELL_HEIGHT, this.transform);
		MAP_POKEMONS [row] [col] = g;
	}

	public void AddSpecialItem(int type, int row, int col) {
		if (type == -1)
		return;
		GameObject g = Instantiate(prefap_pikachu) as GameObject;
		Pokemon pokemon = g.GetComponent<Pokemon> ();
		pokemon.setSpecialInfo(type, row, col, POS[row][col], CELL_WIDH, CELL_HEIGHT, this.transform);
		MAP_POKEMONS [row] [col] = g;
	}

	public Pokemon AddSpecialItem(int type, string name, int row, int col) {
		if (type == -1)
			return null;
		GameObject g = Instantiate(prefap_pikachu) as GameObject;
		Pokemon pokemon = g.GetComponent<Pokemon> ();
		pokemon.setSpecialInfo(type, name, row, col, POS[row][col], CELL_WIDH, CELL_HEIGHT, this.transform);
		pokemon.setFrozen(true);
		MAP_FROZEN[row][col] = Const.FROZEN_FIXED_ID;
		return pokemon;
	}

	public void changePokemon(GameObject obj, Pokemon pokemon, int type, Vec2 next_pos) {
		if (type == -1) {
			return;
		}
		if (type == Const.STONE_FIXED_ID || type == Const.STONE_MOVING_ID) {
			pokemon.setSpecialInfo(type, next_pos.R, next_pos.C, POS[next_pos.R][next_pos.C], CELL_WIDH, CELL_HEIGHT, this.transform);
		} else {
			pokemon.setInfo(type, next_pos.R, next_pos.C, POS[next_pos.R][next_pos.C], CELL_WIDH, CELL_HEIGHT, this.transform);
		}
		MAP [next_pos.R] [next_pos.C] = type;
		MAP_POKEMONS [next_pos.R][next_pos.C] = obj;
	}

	public void changePokemon(Pokemon pokemon, int type, Vec2 next_pos) {
		if (type == -1) {
			return;
		}
		if (type == Const.STONE_FIXED_ID || type == Const.STONE_MOVING_ID) {
			pokemon.setSpecialInfo(type, next_pos.R, next_pos.C, POS[next_pos.R][next_pos.C], CELL_WIDH, CELL_HEIGHT, this.transform);
			MAP [next_pos.R] [next_pos.C] = type;
		} else {
			pokemon.setInfo(type, next_pos.R, next_pos.C, POS[next_pos.R][next_pos.C], CELL_WIDH, CELL_HEIGHT, this.transform);
			MAP [next_pos.R] [next_pos.C] = type;
		}
	}

	public bool changePokemon(GameObject obj, Pokemon pokemon, Vec2 next_pos, float time_move) {
		if (pokemon.POS.R == next_pos.R && pokemon.POS.C == next_pos.C)
			return false;
		pokemon.updateInfo (next_pos, POS [next_pos.R] [next_pos.C], time_move);
		MAP [next_pos.R] [next_pos.C] = pokemon.id;
		MAP_POKEMONS[next_pos.R][next_pos.C] = obj;
		return true;
	}

	public void changePokemon(Pokemon pokemon, Vec2 next_pos, float time_move) {
		pokemon.updateInfo (next_pos, POS [next_pos.R] [next_pos.C], time_move);
		MAP [next_pos.R] [next_pos.C] = pokemon.id;
	}

	public void RemovePokemon(Vec2 POS){
		if (MAP [POS.R] [POS.C] == -1)
		return;
		GameObject obj = getPokemon(POS.R, POS.C);
		if (obj != null) {
			DestroyImmediate (obj);
		}
		MAP_POKEMONS [POS.R] [POS.C] = null;
		MAP[POS.R][POS.C] = -1;
	}

	//check can put frozen
	public bool canPutFrozen(Vec2 pos) {
		if (MAP [pos.R] [pos.C] != -1 && MAP [pos.R] [pos.C] != Const.STONE_FIXED_ID
			&& MAP [pos.R] [pos.C] != Const.STONE_MOVING_ID && MAP_FROZEN [pos.R] [pos.C] == -1) {
					return true;
		}
		return false;
	}
	//find next position can put frozen
	public Vec2 getNextAutoGenFrozenPos() {
		ArrayList list_pokemon = new ArrayList ();
		for (int i = 1; i < row - 1; i++) {
			for (int j = 1; j < col - 1; j++) {
				Vec2 pos = new Vec2(i, j);
				if (canPutFrozen(pos) && !list_frozen_put.ContainsKey(pos.R + "_" + pos.C)) {
					list_pokemon.Add (pos);
				}
			}
		}
		int count = list_pokemon.Count;
		if (count == 0) return null;
		int r = Random.Range(0, count);
		Vec2 result = (Vec2) list_pokemon [r];
		return result;
	}
	// set frozen and update some list constrain
	public void genFrozen(Vec2 pos) {
		AddSpecialItem(Const.FROZEN_FIXED_ID, "frozen_" , pos.R, pos.C);
		SoundSystem.ins.play_sound_ice_add ();
		list_block_frozen.Add(pos);
		logicLevel.levelData.list_block_stone_and_frozen.Add(pos);
		removeSelect(pos);
	}
	//remove select if select cell auto gen frozen
	public void removeSelect(Vec2 pos) {
		if(POS1 != null && POS1.C == pos.C && POS1.R == pos.R){
			GameObject pokemon = getPokemon (POS1.R, POS1.C);
			if (pokemon != null) {
				pokemon.GetComponent<Pokemon>().DeSelect();
			}
			POS1 = null;
		}
		if(POS2 != null && POS2.C == pos.C && POS2.R == pos.R){
			GameObject pokemon = getPokemon (POS2.R, POS2.C);
			if (pokemon != null) {
				pokemon.GetComponent<Pokemon>().DeSelect();
			}
			POS2 = null;
		}
		if(HINT_POS1 != null && HINT_POS1.C == pos.C && HINT_POS1.R == pos.R){
			RemoveHint();
		}
		if(HINT_POS2 != null && HINT_POS2.C == pos.C && HINT_POS2.R == pos.R){
			RemoveHint();
		}
	}

	public void autoGenFrozen() {
		if (list_frozen_put.Count == 0) return;
		foreach (string key in list_frozen_put.Keys) {
			Vec2 pos = (Vec2) list_frozen_put[key];
			if (canPutFrozen(pos)) {
				genFrozen(pos);
			}
		}
		list_frozen_put.Clear ();
		int numberPokemonCanEat = getNumberPokemonCanEat();
		if(!isReseting && numberPokemonCanEat == 0 && numberPokemonRemain > 3) {
			StartCoroutine(StartResetMap());
		}
	}

	public void updateLogicAutoGen() {
		if (mapState == MapState.playing && !isGameOver) {
			autoGenFrozen();
			currentAutoGenTime+=Time.deltaTime;
			if (currentAutoGenTime > time_update_list_frozen) {
				if (!checking_paire) {
					currentAutoGenTime = 0;
					updateListFrozen();
					if (checkGameOver ()) {
						isGameOver = true;
						lastTime = currentTime;
						for (int i = 0; i < listAutoGens.Count; i++) {
							AutoGenController autoGenController = (AutoGenController)listAutoGens [i];
							autoGenController.disableClockCountDown ();
						}
						sendHighScore ();
						GameStatic.logLevel(GameStatic.currentMode, ItemController.getNumHintItem(), ItemController.getNumRandomItem(), GameStatic.currentLevel, 0, GameStatic.currentScore,false);
					}
				}
			}
		}
	}

	//destroy Clock
	private void updateClockState(Vec2 pos) {
		if (pos == null) return;
		for (int i = 0; i < listAutoGens.Count; i++) {
			AutoGenController autoGenControl = (AutoGenController)listAutoGens [i];
			Vec2 pos_clock = autoGenControl.pos;
			if (pos_clock == null) {
				continue;
			}
			if (pos_clock.R == pos.R && pos_clock.C == pos.C
			    || pos_clock.R - 1 == pos.R && pos_clock.C == pos.C
			    || pos_clock.R + 1 == pos.R && pos_clock.C == pos.C
			    || pos_clock.R == pos.R && pos_clock.C - 1 == pos.C
			    || pos_clock.R == pos.R && pos_clock.C + 1 == pos.C) {
				autoGenControl.lockAutoGen ();
			}
		}
	}

	IEnumerator StartResetMap() {
		SoundSystem.ins.play_sound_shuffle();
		isReseting = true;
		yield return new WaitForSeconds(Time.deltaTime + 0.05f);
		RandomMap ();
	}

	IEnumerator botCallResetMap () {
		SoundSystem.ins.play_sound_shuffle();
		isReseting = true;
		yield return new WaitForSeconds(1f);
		RandomMap ();
	}

	public int getOffsetPokemonRemain() {
		int offset =  (int) Mathf.Floor(numberPokemonRemain / 8);
		if (offset > 15) {
			offset = 15;
		}
		return offset;
	}

	void printLPath(ArrayList LPath) {
		for (int i = 0; i < LPath.Count; i++) {
			Debug.Log("Value : " + ((Vec2)LPath[i]).Print());
		}
	}

	bool checkPaire(Vec2 POS1, Vec2 POS2, bool showPath){
		if (POS1 == null)
			return false;
		if (POS2 == null) {
			return false;
		}
		if (POS1.C == POS2.C) {
			if (check_vertical (POS1, POS2, showPath)) {
				return true;
			}
		} else if (POS1.R == POS2.R) {
			if (check_horizontal (POS1, POS2, showPath)) {
				return true;
			}
		}else if(checkL(POS1, POS2, showPath)){
			return true;
		} else if(checkZ (POS1, POS2, showPath)){
			return true;
		}

		if (checkU(POS1, POS2, showPath)){
			return true;
		}
		return false;
	}

	bool check_vertical(Vec2 POS1, Vec2 POS2, bool showPath){
		if (get_pos_between_vertical (POS1, POS2) == POS2.R) {
			if (showPath) {
				add_path_vertical (POS1, POS2);	
			}
			return true;
		} else {
			return false;
		}
	}

	bool check_horizontal(Vec2 POS1, Vec2 POS2, bool showPath){
		if (get_pos_between_horizontal (POS1, POS2) == POS2.C) {
			if (showPath) {
				add_path_horizontal(POS1, POS2);
			}
			return true;
		} else {
			return false;
		}
	}

	bool checkZ(Vec2 POS1, Vec2 POS2, bool showPath){
		int ph1 = get_pos_between_horizontal (POS1, POS2);
		int ph2 = get_pos_between_horizontal(POS2, POS1);
		int pv1 = get_pos_between_vertical (POS1, POS2);
		int pv2 = get_pos_between_vertical (POS2, POS1);

		if (ph1 > POS1.C) {
			if (ph1 > POS2.C) {
				ph1 = POS2.C;
			}
			if (ph2 < ph1 - 1) {
				for (int i = ph2 + 1; i < ph1; i++) {
					if (Mathf.Abs (POS1.R - POS2.R) < 2) {
						if (MAP [POS1.R] [i] == -1 && MAP [POS2.R] [i] == -1) {
							if (showPath) {
								add_path_horizontal(POS1, new Vec2(POS1.R, i));
								add_path_horizontal(new Vec2(POS2.R, i), POS2);	
							}
							return true;
						}
					} else {
						int pv = get_pos_between_vertical (new Vec2 (POS1.R, i), new Vec2 (POS2.R, i));
						if (pv == POS2.R) {
							if (showPath) {
								add_path_horizontal(POS1, new Vec2(POS1.R, i));
								add_path_vertical(new Vec2 (POS1.R, i), new Vec2 (POS2.R, i));
								add_path_horizontal(new Vec2(POS2.R, i), POS2);	
							}
							return true;
						}
					}
				}
			}
		} else if (ph1 < POS1.C) {
			if (ph1 < POS2.C) {
				ph1 = POS2.C;
			}
			if (ph2 > ph1 + 1) {
				for(int i = ph1 + 1 ; i < ph2; i++){
					if (Mathf.Abs(POS1.R - POS2.R) < 2) {
						if(MAP[POS1.R][i] == -1 && MAP[POS2.R][i] == -1){
							if (showPath) {
								add_path_horizontal(POS1, new Vec2(POS1.R, i));
								add_path_horizontal(new Vec2(POS2.R, i) ,POS2);	
							}
							return true;
						}
					}else {
						int pv = get_pos_between_vertical(new Vec2(POS1.R,i),new Vec2(POS2.R, i));
						if(pv == POS2.R){
							if (showPath) {
								add_path_horizontal(POS1, new Vec2(POS1.R, i));
								add_path_vertical(new Vec2 (POS1.R, i), new Vec2 (POS2.R, i));
								add_path_horizontal(new Vec2(POS2.R, i), POS2);	
							}
							return true;
						}
					}
				}
			}
		}
		
		if (pv1 > POS1.R) {
			if (pv1 > POS2.R) {
				pv1 = POS2.R;
			}
			if (pv2 < pv1 - 1) {
				for (int i = pv2 + 1; i < pv1; i++) {
					if (Mathf.Abs(POS1.C - POS2.C) < 2) {
						if(MAP[i][POS1.C] == -1 && MAP[i][POS2.C] == -1){
							if (showPath) {
								add_path_vertical(POS1, new Vec2(i, POS1.C));
								add_path_vertical(new Vec2(i, POS2.C), POS2);	
							}
							return true;
						}
					}else {
						int ph = get_pos_between_horizontal (new Vec2 (i, POS1.C), new Vec2 (i, POS2.C));
						if (ph == POS2.C) {
							if (showPath) {
								add_path_vertical(POS1, new Vec2(i, POS1.C));
								add_path_horizontal(new Vec2 (i, POS1.C), new Vec2 (i, POS2.C));
								add_path_vertical(new Vec2(i, POS2.C), POS2);	
							}
							return true;
						}
					}
				}
			}
		} else if (pv1 < POS1.R) {
			if (pv1 < POS2.R) {
				pv1 = POS2.R;
			}
			if (pv2 > pv1 + 1) {
				for (int i = pv1 + 1; i < pv2; i++) {
					if (pv1 == pv2 && Mathf.Abs(POS1.C - POS2.C) < 2) {
						if(MAP[i][POS1.C] == -1 && MAP[i][POS2.C] == -1){
							if (showPath) {
								add_path_vertical(POS1, new Vec2(i, POS1.C));
								add_path_vertical(new Vec2(i, POS2.C), POS2);	
							}
							return true;
						}
					} else {
						int ph = get_pos_between_horizontal (new Vec2 (i, POS1.C), new Vec2 (i, POS2.C));
						if (ph == POS2.C) {
							if (showPath) {
								add_path_vertical(POS1, new Vec2(i, POS1.C));
								add_path_horizontal(new Vec2 (i, POS1.C), new Vec2 (i, POS2.C));
								add_path_vertical(new Vec2(i, POS2.C), POS2);	
							}
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	bool checkL(Vec2 POS1, Vec2 POS2, bool showPath){
		int ph1 = get_pos_between_horizontal (POS1, POS2);
		int pv1 = get_pos_between_vertical (POS1, POS2);
		int ph2 = get_pos_between_horizontal(POS2, POS1);
		int pv2 = get_pos_between_vertical (POS2, POS1);
		
		if (ph1 == POS2.C && pv2 == POS1.R && MAP[pv2][ph1] == -1) {
			if (showPath) {
				add_path_horizontal(POS1, new Vec2(POS1.R, POS2.C));
				add_path_vertical(new Vec2(POS1.R, POS2.C), POS2);
			}
			return true;
		} else if (pv1 == POS2.R && ph2 == POS1.C && MAP[pv1][ph2] == -1) {
			if (showPath) {
				add_path_vertical(POS1, new Vec2(POS2.R, POS1.C));
				add_path_horizontal(new Vec2(POS2.R, POS1.C), POS2);	
			}
			return true;
		}
		return false;
	}

	bool checkU(Vec2 POS1, Vec2 POS2, bool showPath){
		if (checkU_Left(POS1, POS2, showPath)) {
			return true;
		}
		if (checkU_Right(POS1, POS2, showPath)){
			return true;
		}
		if (checkU_Up(POS1, POS2, showPath)){
			return true;
		}
		if (checkU_Down(POS1, POS2, showPath)){
			return true;
		}
		return false;
	}

	bool checkU_Left(Vec2 POS1, Vec2 POS2, bool showPath){
		if (POS1.R == POS2.R) {
			return false;
		}
		int ph1 = get_pos_between_horizontal (POS1, new Vec2 (POS1.R, 0));
		int ph2 = get_pos_between_horizontal (POS2, new Vec2 (POS2.R, 0));
		if (POS1.C < POS2.C && ph2 > POS1.C) {
			if (ph1 == 0 && ph2 == 0) {
				if (showPath) {
					add_path_horizontal (POS1, new Vec2 (POS1.R, 0));
					add_path_vertical(new Vec2 (POS1.R, 0), new Vec2 (POS2.R, 0));
					add_path_horizontal (new Vec2 (POS2.R, 0), POS2);	
				}
				return true;
			}
			return false;
		} else if (POS1.C > POS2.C && ph1 > POS2.C) {
			if (ph1 == 0 && ph2 == 0) {
				if(showPath) {
					add_path_horizontal (POS1, new Vec2 (POS1.R, 0));
					add_path_vertical(new Vec2 (POS1.R, 0), new Vec2 (POS2.R, 0));
					add_path_horizontal (new Vec2 (POS2.R, 0), POS2);
				}
				return true;
			}
			return false;
		}

		int start = POS1.C > POS2.C ? POS2.C : POS1.C;
		int end = ph1 > ph2 ? ph1 : ph2;
		for (int i = start - 1; i > end; i--) {
			int pv = get_pos_between_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
			if(pv == POS2.R){
				if (showPath) {
					add_path_horizontal(POS1, new Vec2(POS1.R, i));
					add_path_vertical (new Vec2(POS1.R, i), new Vec2(POS2.R, i));
					add_path_horizontal(new Vec2(POS2.R, i), POS2);	
				}
				return true;
			}
		}
		if (ph1 == 0 && ph2 == 0) {
			if (showPath) {
				add_path_horizontal (POS1, new Vec2 (POS1.R, 0));
				add_path_vertical(new Vec2 (POS1.R, 0), new Vec2 (POS2.R, 0));
				add_path_horizontal (new Vec2 (POS2.R, 0), POS2);	
			}
			return true;
		}
		return false;
	}

	bool checkU_Right(Vec2 POS1, Vec2 POS2, bool showPath){
		if (POS1.R == POS2.R) {
			return false;
		}
		int ph1 = get_pos_between_horizontal (POS1, new Vec2 (POS1.R, col - 1));
		int ph2 = get_pos_between_horizontal (POS2, new Vec2 (POS2.R, col - 1));
		if (POS1.C < POS2.C && ph1 < POS2.C) {
			if (ph1 == col - 1 && ph2 == col - 1) {
				if (showPath) {
					add_path_horizontal (POS1, new Vec2 (POS1.R, col - 1));
					add_path_vertical (new Vec2 (POS1.R, col - 1), new Vec2 (POS2.R, col -1));
					add_path_horizontal (new Vec2 (POS2.R, col -1), POS2);	
				}
				return true;
			}
			return false;
		} else if (POS1.C > POS2.C && ph2 < POS1.C) {
			if (ph1 == col - 1 && ph2 == col - 1) {
				if (showPath) {
					add_path_horizontal (POS1, new Vec2 (POS1.R, col - 1));
					add_path_vertical (new Vec2 (POS1.R, col - 1), new Vec2 (POS2.R, col -1));
					add_path_horizontal (new Vec2 (POS2.R, col -1), POS2);	
				}
				return true;
			}
			return false;
		}

		int start = POS1.C > POS2.C ? POS1.C : POS2.C;
		int end = ph1 > ph2 ? ph2 : ph1;
		for (int i = start + 1; i < end; i++) {
			int pv = get_pos_between_vertical(new Vec2(POS1.R, i), new Vec2(POS2.R, i));
			if(pv == POS2.R){
				if (showPath) {
					add_path_horizontal (POS1, new Vec2 (POS1.R, i));
					add_path_vertical (new Vec2 (POS1.R, i), new Vec2 (POS2.R, i));
					add_path_horizontal (new Vec2 (POS2.R, i), POS2);	
				}
				return true;
			}
		}
		if (ph1 == col - 1 && ph2 == col - 1) {
			if (showPath) {
				add_path_horizontal (POS1, new Vec2 (POS1.R, col - 1));
				add_path_vertical (new Vec2 (POS1.R, col - 1), new Vec2 (POS2.R, col -1));
				add_path_horizontal (new Vec2 (POS2.R, col -1), POS2);	
			}
			return true;
		}
		return false;
	}

	bool checkU_Up(Vec2 POS1, Vec2 POS2, bool showPath){
		if (POS1.C == POS2.C) {
			return false;
		}
		int pv1 = get_pos_between_vertical (POS1, new Vec2 (row - 1, POS1.C));
		int pv2 = get_pos_between_vertical (POS2, new Vec2 (row - 1, POS2.C));

		if (POS1.R < POS2.R && pv1 < POS2.R) {
			if (pv1 == row - 1 && pv2 == row - 1) {
				if (showPath) {
					add_path_vertical(POS1, new Vec2 (row - 1, POS1.C));
					add_path_horizontal(new Vec2 (row - 1, POS1.C), new Vec2 (row - 1, POS2.C));
					add_path_vertical(new Vec2 (row - 1, POS2.C), POS2);	
				}
				return true;
			}
			return false;
		} else if (POS1.R > POS2.R && pv2 < POS1.R) {
			if (pv1 == row - 1 && pv2 == row - 1) {
				if (showPath) {
					add_path_vertical(POS1, new Vec2 (row - 1, POS1.C));
					add_path_horizontal(new Vec2 (row - 1, POS1.C), new Vec2 (row - 1, POS2.C));
					add_path_vertical(new Vec2 (row - 1, POS2.C), POS2);	
				}
				return true;
			}
			return false;
		}

		int start = POS1.R > POS2.R ? POS1.R : POS2.R;
		int end = pv1 > pv2 ? pv2 : pv1;
		for (int i = start + 1; i < end; i++) {
			int ph = get_pos_between_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
			if(ph == POS2.C){
				if (showPath) {
					add_path_vertical(POS1, new Vec2 (i, POS1.C));
					add_path_horizontal(new Vec2 (i, POS1.C), new Vec2 (row - 1, POS2.C));
					add_path_vertical(new Vec2 (i, POS2.C), POS2);	
				}
				return true;
			}
		}
		if (pv1 == row - 1 && pv2 == row - 1) {
			if (showPath) {
				add_path_vertical(POS1, new Vec2 (row - 1, POS1.C));
				add_path_horizontal(new Vec2 (row - 1, POS1.C), new Vec2 (row - 1, POS2.C));
				add_path_vertical(new Vec2 (row - 1, POS2.C), POS2);	
			}
			return true;
		}
		return false;
	}

	bool checkU_Down(Vec2 POS1, Vec2 POS2, bool showPath){
		if (POS1.C == POS2.C) {
			return false;
		}
		int pv1 = get_pos_between_vertical (POS1, new Vec2 (0, POS1.C));
		int pv2 = get_pos_between_vertical (POS2, new Vec2 (0, POS2.C));

		if (POS1.R < POS2.R && pv2 > POS1.R) {
			if (pv1 == 0 && pv2 == 0) {
				if (showPath) {
					add_path_vertical(POS1, new Vec2 (0, POS1.C));
					add_path_horizontal(new Vec2 (0, POS1.C), new Vec2 (0, POS2.C));
					add_path_vertical(new Vec2 (0, POS2.C) ,POS2);	
				}
				return true;
			}
			return false;
		} else if (POS1.R > POS2.R && pv1 > POS2.R) {
			if (pv1 == 0 && pv2 == 0) {
				if (showPath) {
					add_path_vertical(POS1, new Vec2 (0, POS1.C));
					add_path_horizontal(new Vec2 (0, POS1.C), new Vec2 (0, POS2.C));
					add_path_vertical(new Vec2 (0, POS2.C) ,POS2);	
				}
				return true;
			}
			return false;
		}

		int start = POS1.R > POS2.R ? POS2.R : POS1.R;
		int end = pv1 > pv2 ? pv1 : pv2;
		for (int i = start - 1; i > end; i--) {
			int ph = get_pos_between_horizontal(new Vec2(i, POS1.C), new Vec2(i, POS2.C));
			if(ph == POS2.C){
				if (showPath) {
					add_path_vertical(POS1, new Vec2 (i, POS1.C));
					add_path_horizontal(new Vec2 (i, POS1.C), new Vec2 (row - 1, POS2.C));
					add_path_vertical(new Vec2 (i, POS2.C), POS2);	
				}
				return true;
			}
		}
		if (pv1 == 0 && pv2 == 0) {
			if (showPath) {
				add_path_vertical(POS1, new Vec2 (0, POS1.C));
				add_path_horizontal(new Vec2 (0, POS1.C), new Vec2 (0, POS2.C));
				add_path_vertical(new Vec2 (0, POS2.C) ,POS2);	
			}
			return true;
		}
		return false;
	}

	int get_pos_between_horizontal(Vec2 POS1, Vec2 POS2){
		if (POS1.C >= POS2.C) {
			for(int i = POS1.C - 1; i > POS2.C; i--){
				if(MAP[POS1.R][i] != -1){
					return i;
				}
			} 	
		} else {
			for(int i = POS1.C + 1; i < POS2.C; i++){
				if(MAP[POS1.R][i] != -1){
					return i;
				}
			}
		}
		return POS2.C;
	}

	int get_pos_between_vertical(Vec2 POS1, Vec2 POS2){
		if (POS1.R >= POS2.R) {
			for(int i = POS1.R - 1; i > POS2.R; i--){
				if(MAP[i][POS1.C] != -1){
					return i;
				}
			} 	
		} else {
			for(int i = POS1.R + 1; i < POS2.R; i++){
				if(MAP[i][POS1.C] != -1){
					return i;
				}
			}
		}
		return POS2.R;
	}

	void add_path_horizontal(Vec2 POS1, Vec2 POS2) {
		if (POS1.C >= POS2.C) {
			for (int i = POS1.C; i >= POS2.C; i--) {
				Vec2 pos = new Vec2(POS1.R, i);
				if(!keyLPath.Contains(POS1.R + "_" + i)) {
					LPath.Add(pos);
					keyLPath.Add(POS1.R + "_" + i);
				}
			}
		} else {
			for (int i = POS1.C; i <= POS2.C; i++) {
				Vec2 pos = new Vec2(POS1.R, i);
				if(!keyLPath.Contains(POS1.R + "_" + i)) {
					LPath.Add(pos);
					keyLPath.Add(POS1.R + "_" + i);
				}
			}
		}
	}

	void add_path_vertical(Vec2 POS1, Vec2 POS2) {
		if (POS1.R >= POS2.R) {
			for (int i = POS1.R; i >= POS2.R; i--) {
				Vec2 pos = new Vec2(i, POS1.C);
				if(!keyLPath.Contains(i + "_" + POS1.C)) {
					LPath.Add(pos);
					keyLPath.Add(i + "_" + POS1.C);
				}
			}
		}else {
			for (int i = POS1.R; i <= POS2.R; i++) {
				Vec2 pos = new Vec2(i, POS1.C);
				if(!keyLPath.Contains(i + "_" + POS1.C)) {
					LPath.Add(pos);
					keyLPath.Add(i + "_" + POS1.C);
				}
			}
		}
	}

	ArrayList getRandomList(ArrayList listBefore) {
		ArrayList listAfter = new ArrayList();
		int count = listBefore.Count;
		while(count > 0) {
			int r = Random.Range(0, count);
			listAfter.Add (listBefore [r]);
			listBefore.RemoveAt (r);
			count--;
		}
		return listAfter;
	}

	public void changeUI(){
		int usePack = Save.getUsedPack ();
		Debug.Log(usePack);
		if (Save.isShowClassicUI ()) {
			Save.usePack (++usePack % 3);
		} else {
			Save.usePack (++usePack % 2);
		}
		reloadUI ();
		Save.saveTutorialChangeUI(1);
		tutorialObj.SetActive(false);
	}

	//for ads banner
	//public void showBannerView() {
	//	AdsManager.ins.showBannerView();
	//}

	//public void hideBannerView() {
	//	AdsManager.ins.hideBannerView();
	//}

	public void sendHighScore() {
		//if (GameStatic.currentMode == Const.GAME_MODE_EVENT) {
		//	GSM.RQSendHighScore (GameStatic.currentScore, response => {
		//		Debug.Log ("response :: " + response.JSON);
		//		bool result = (bool) response.GetBoolean("result");
		//		if (result) {
		//			GSData rankUpData = response.GetGSData("rank_up_data");
		//			if (GameStatic.rankingResult != null) {
		//				GameStatic.rankingResult.showRankingResult(rankUpData);
		//			}
		//		}
		//	});
		//	int currentEventID = GSM.instance.eventData != null ? (int) GSM.instance.eventData.GetInt ("event_id") : Save.getCurrentEventID ();
		//	Save.saveHighScoreEvent (currentEventID, GameStatic.currentScore);
		//}
	}

}