using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialStore : MonoBehaviour {

	public int step = 1;
	public GameObject topBar;
	public GameObject mapObject;
	public Map map;
	private RectTransform rectTransform;

	private int TYPE_UI = 1;
	private int TYPE_GAME = 2;

	void Start () {
		rectTransform = gameObject.GetComponent<RectTransform> ();
	}

	bool showStep1 = false;
	void Update(){
		if (!showStep1 && Save.getTutorialStatus() == 0) {
			if (map.getPokemon (1, 1) != null) {
				setTutorial (1);	
				showStep1 = true;
			}
		}
	}

	public void setTutorial(int step) {
		this.step = step;
		switch (step) {
		case 1: 
			//chon con 1 (8, 6)
			GameObject pokemon1 = map.getPokemon(7, 6);
			_setTutorialPosition (pokemon1, TYPE_GAME);
			break;
		case 2:
			//chon con 2 (8, 12)
			GameObject pokemon2 = map.getPokemon(7, 12);
			_setTutorialPosition (pokemon2, TYPE_GAME);
			break;
		case 3: 
			//chon con 1 tren hop dung yen (1, 2)
			GameObject pokemon3 = map.getPokemon(1, 2);
			_setTutorialPosition (pokemon3, TYPE_GAME);
			break;
		case 4:
			//chon con 2 tren hop dung yen (1, 15)
			GameObject pokemon4 = map.getPokemon(1, 13);
			_setTutorialPosition (pokemon4, TYPE_GAME);
			break;
		case 5: 
			//chon con 1 duoi hop roi (1, 3)
			GameObject pokemon5 = map.getPokemon(1, 3);
			_setTutorialPosition (pokemon5, TYPE_GAME);
			break;
		case 6: 
			//chon con 2 duoi hop roi (1, 14)
			GameObject pokemon6 = map.getPokemon(1, 14);
			_setTutorialPosition (pokemon6, TYPE_GAME);
			break;
		case 7:
			//chon con 1 canh bang (1, 5)
			GameObject pokemon7 = map.getPokemon(1, 5);
			_setTutorialPosition (pokemon7, TYPE_GAME);
			break;
		case 8:
			//chon con 2 canh bang (1, 12)
			GameObject pokemon8 = map.getPokemon(1, 12);
			_setTutorialPosition (pokemon8, TYPE_GAME);
			break;
		case 9: 
			//chon item goi y 
			GameObject hintObj = GameObject.Find ("HintButton");
			_setTutorialPosition (hintObj, TYPE_UI);
			break;
		case 10: 
			//chon con 1 sau khi goi y (1, 1)
			GameObject pokemon9 = map.getPokemon(map.HINT_POS1.R, map.HINT_POS1.C);
			_setTutorialPosition (pokemon9, TYPE_GAME);
			break;
		case 11:
			//chon con 2 sau khi goi y (1, 4)
			GameObject pokemon10 = map.getPokemon(map.HINT_POS2.R, map.HINT_POS2.C);
			_setTutorialPosition (pokemon10, TYPE_GAME);
			break;
		case 12:
			//chon item random map
			GameObject randomObj = GameObject.Find ("RandomButton");
			_setTutorialPosition (randomObj, TYPE_UI);
			break;
		case 13:
			//chon exit
			GameObject settingObj = GameObject.Find("BackButton");
			_setTutorialPosition (settingObj, TYPE_UI);
			break;
		case 14:
			//chon thoat
			break;
		case 999:
			break;
		default:
			break;
		}
	}

	private void _setTutorialPosition (GameObject parent, int type) {
		if (type == TYPE_UI) {
			this.gameObject.transform.SetParent (topBar.transform);
		} else if (type == TYPE_GAME) {
			this.gameObject.transform.SetParent (mapObject.transform);
		}
		this.gameObject.transform.position = new Vector3 (parent.transform.position.x + 10, parent.transform.position.y - 20, parent.transform.position.z);
		gameObject.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void nextTutorial() {
		setTutorial (this.step + 1);
	}

	public void onClickToStore() {
		if (this.step == 1) {
			nextTutorial();
		}
	}

	public void onClickToTabCoin() {
		if (this.step == 2) {
			nextTutorial();
		}
	}

	public void onClickToBuyCoinFree() {
		if (this.step == 3) {
			nextTutorial();
		}
	}

	public void onClickToTabItem() {
		if (this.step == 4) {
			nextTutorial();
		}
	}

	public void onClickToBuyItemHint1() {
		if (this.step == 5) {
			nextTutorial ();
		}
	}

	public void onClickToExit() {
		SceneManager.LoadSceneAsync ("Main");
	}

	public bool onClick(int row, int col) {
		switch (this.step) {
		case 1:
			if (row == 7 && col == 6) {
				nextTutorial();
				return true;
			}
			break;
		case 2:
			if (row == 7 && col == 12) {
				nextTutorial();
				return true;
			}
			break;
		case 3:
			if (row == 1 && col == 2) {
				nextTutorial();
				return true;
			}
			break;
		case 4:
			if (row == 1 && col == 13) {
				nextTutorial();
				return true;
			}
			break;
		case 5:
			if (row == 1 && col == 3) {
				nextTutorial();
				return true;
			}
			break;
		case 6:
			if (row == 1 && col == 14) {
				nextTutorial();
				return true;
			}
			break;
		case 7:
			if (row == 1 && col == 5) {
				nextTutorial();
				return true;
			}
			break;
		case 8:
			if (row == 1 && col == 12) {
				nextTutorial();
				return true;
			}
			break;
		case 10:
			if (row == map.HINT_POS1.R && col == map.HINT_POS1.C) {
				nextTutorial();
				return true;
			}
			break;
		case 11:
			if (row == map.HINT_POS2.R && col == map.HINT_POS2.C) {
				nextTutorial();
				return true;
			}
			break;
		case 13:
			break;
		case 14:
			break;
		case 15:
			break;

		}
		return false;
	}
}
