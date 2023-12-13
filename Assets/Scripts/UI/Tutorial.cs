using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	public int step = 1;
	public GameObject topBar;
	public GameObject mapObject;
	public GameObject btnCloseStore;
	public Map map;

	public int TYPE_UI = 1;
	public int TYPE_GAME = 2;

	public static int TUTORIAL_MODE = 0;
	// MODE = 0 : tutorial game play
	// MODE = 1 : tutorial store

	public GameObject tutorialGame;
	public GameObject tutorialStore;

	public StoreController storeController;

	void Start () {
//		if (TUTORIAL_MODE == 0) {
//			tutorialGame.SetActive(true);
//			mapObject.SetActive(true);
//			Save.setTutorial (1);
//		} else if (TUTORIAL_MODE == 1) {
//			tutorialStore.SetActive(true);
//			StartCoroutine (onloadStore());
//			Save.setTutorialStore (1);
//		}
	}

	IEnumerator onloadStore() {
		yield return new WaitForFixedUpdate ();
		//storeController.onLoad (true);
	}

	bool showStep1 = false;
	void Update(){
		if (!showStep1 && TUTORIAL_MODE == 0) {
			if (map != null && map.getPokemon (1, 1) != null) {
				Save.setTutorial(1);
				setTutorial (1);
				showStep1 = true;
			}
		}
		if (!showStep1 && TUTORIAL_MODE == 1) {
			showStep1 = true;
			// Save.setTutorialStore(101);
			setTutorial(101);
		}
	}

	public void setTutorial(int step) {
		this.step = step;
		switch (step) {
		case 1:
			//chon con 1 (7, 6)
			GameObject pokemon1 = map.getPokemon(7, 6);
			_setTutorialPosition (pokemon1, TYPE_GAME);
			break;
		case 2:
			//chon con 2 (7, 12)
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
		case 101:
			//chon tab Coin
			GameObject btnStore = GameObject.Find("TabCoin");
			_setTutorialPosition(btnStore, TYPE_UI);
			break;
		case 102:
			//chon mua coin free
			_setTutorialPositionForItemShop (999);
			break;
		case 103:
			//chon tab item
			GameObject btnItem = GameObject.Find("TabItem");
			_setTutorialPosition(btnItem, TYPE_UI);
			break;
		case 104:
			//chon mua item hint 1
			_setTutorialPositionForItemShop(0);
			break;
		case 105:
			//chon tab UI
			GameObject btnUI = GameObject.Find("TabUI");
			_setTutorialPosition(btnUI, TYPE_UI);
			break;
		case 106:
			//chon mua item UI default 101
			_setTutorialPositionForUIPack(101, 0);
			break;
		case 107:
			//chon mua item UI default 101
			_setTutorialPositionForUIPack(101, 1);
			break;
		case 108:
			//chon quay lai store
			GameObject settingObj1 = GameObject.Find("BackButton");
			_setTutorialPosition (settingObj1, TYPE_UI);
			break;
		case 109:
			//chon mua item UI default 100
			_setTutorialPositionForUIPack(100, 0);
			break;
		case 110:
			//chon mua item UI default 100
			_setTutorialPositionForUIPack(100, 1);
			break;
		case 111:
			//chon quay lai store
			GameObject settingObj2 = GameObject.Find("BackButton");
			_setTutorialPosition (settingObj2, TYPE_UI);
			break;
		case 112:
			//chon tab other
			GameObject btnOther = GameObject.Find("TabSale");
			_setTutorialPosition(btnOther, TYPE_UI);
			break;
		case 113:
			//chon xem video free
			GameObject playVideo = GameObject.Find ("PlayVideo");
			_setTutorialPosition (playVideo, TYPE_UI);
			break;	
		case 114:
			_setTutorialPosition (btnCloseStore, TYPE_UI);
			//thoat
			break;
		case 999:
			break;
		default:
			break;
		}
	}

	private void _setTutorialPositionForItemShop(int pack_id) {
		GameObject[] items = GameObject.FindGameObjectsWithTag ("item");
		for (int i = 0; i < items.Length; i++) {
			GameObject item = items [i];
			ItemShop itemShop = (ItemShop)item.GetComponent<ItemShop> ();
			if (itemShop != null && itemShop.itemShopBase.pack_id == pack_id) {
				GameObject btnBuy = item.transform.Find ("ButtonBuy").gameObject;
				_setTutorialPosition (btnBuy, TYPE_UI);
			}
		}
	}

	private void _setTutorialPositionForUIPack(int item_id, int mode) {
		GameObject[] items = GameObject.FindGameObjectsWithTag ("item");
		for (int i = 0; i < items.Length; i++) {
			GameObject item = items [i];
			ItemShop itemShop = (ItemShop)item.GetComponent<ItemShop> ();
			if (itemShop != null && itemShop.itemShopBase != null && itemShop.itemShopBase.item_id == item_id) {
				if (mode == 0) { // active, inactive
					GameObject btnBuy = item.transform.Find ("ButtonBuy").gameObject;
					_setTutorialPosition (btnBuy, TYPE_UI);
				} else if (mode == 1) { // try
					GameObject btnTryNow = item.transform.Find ("ButtonPreview").gameObject;
					_setTutorialPosition (btnTryNow, TYPE_UI);
				}
			}
		}
	}


	private void _setTutorialPosition (GameObject parent, int type) {

		if (type == TYPE_UI) {
            Debug.Log(parent.name);
			this.gameObject.transform.SetParent (parent.transform);
			this.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (10,-20);
		} else if (type == TYPE_GAME) {
			this.gameObject.transform.SetParent (mapObject.transform);
			this.gameObject.transform.position = new Vector3 (parent.transform.position.x + 10, parent.transform.position.y - 20, parent.transform.position.z);
		}
		gameObject.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void showFingerTutorial(GameObject parent, int type) {
		_setTutorialPosition (parent, type);
	}

	public void nextTutorial() {
		setTutorial (this.step + 1);
	}

	public void onClickToExit() {
		if (this.step == 114 || this.step < 100) {
			SceneManager.LoadSceneAsync ("Main");
		}
	}

	public void onClick() {
		switch (this.step) {
		case 9:
			map.Hint ();
			nextTutorial ();
			break;
		case 12:
			map.RandomMap ();
			nextTutorial ();
			break;
		case 13:
			//thoat
			onClickToExit ();
			break;
		case 100:
			break;
		case 101:
			//chon tab Coin
			//storeController.onClickTabCoin ();
			nextTutorial();
			break;
		case 102:
			//chon mua coin free
				ItemShop itemCinFree = (ItemShop) transform.GetComponentInParent<ItemShop> ();
				Save.addPlayerCoin(itemCinFree.itemShopBase.number);
				//itemCinFree.storeController.updateUserInfo ();
				ToastManager.showToast (StringUtils.success_store_purchase + " " + itemCinFree.itemShopBase.number + " coin");
				nextTutorial();
			break;
		case 103:
			//chon tab item
			nextTutorial();
			//storeController.onClickTabItem ();
			break;
		case 104:
			//chon mua item hint 1
			ItemShop itemHint1 = (ItemShop) transform.GetComponentInParent<ItemShop> ();
			ItemController.addHintItem (itemHint1.itemShopBase.number);
			Save.addPlayerCoin(-itemHint1.itemShopBase.cost);
			//itemHint1.storeController.updateUserInfo ();
			ToastManager.showToast (StringUtils.success_store_purchase + " " + itemHint1.itemShopBase.number + " item hint");
			nextTutorial();
			break;
		case 105:
			//chon tab UI
			//storeController.onClickTabUI ();
			nextTutorial ();
			break;
		case 106:
			//chon mua item UI default 101
			Save.savePackInfo(101);
			UIPackItemController itemPackController101 = (UIPackItemController) transform.GetComponentInParent<UIPackItemController> ();
			//itemPackController101.buy();
			nextTutorial();
			break;
		case 107:
			//chon dung thu item UI default 101
			tutorialGame.SetActive (true);
			tutorialStore.SetActive (false);
			mapObject.SetActive (true);
			map.isTutorial = false;
			// map.Replay ();
			nextTutorial();
			break;
		case 108:
			//chon quay lai store
			tutorialGame.SetActive (false);
			tutorialStore.SetActive (true);
			mapObject.SetActive (false);
			nextTutorial ();
			break;
		case 109:
			//chon mua item UI default 100
			Save.savePackInfo(100);
			UIPackItemController itemPackController100 = (UIPackItemController) transform.GetComponentInParent<UIPackItemController> ();
			Save.savePackInfo (100);
			//itemPackController100.buy();
			nextTutorial();
			break;
		case 110:
			//chon dung thu
			tutorialGame.SetActive (true);
			tutorialStore.SetActive (false);
			mapObject.SetActive (true);
			map.isTutorial = false;
			map.Replay ();
			nextTutorial();
			break;
		case 111:
			//chon quay lai store
			tutorialGame.SetActive (false);
			tutorialStore.SetActive (true);
			mapObject.SetActive (false);
			nextTutorial ();
			break;
		case 112:
			//chon tab other
			//storeController.onClickTabOther ();
			nextTutorial ();
			break;
		case 113:
			nextTutorial ();
			break;
		case 114:
			//thoat
			onClickToExit ();
			break;
		default:
			break;
		}
	}

	public void onClickToStore() {
		if (this.step == 100) {
			nextTutorial();
		}
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
