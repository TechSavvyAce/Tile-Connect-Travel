using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {

	public enum TAB{none, item, ui, coin, other};
	public TAB currentTab = TAB.none;

	public TabHeader tabItem;
	public TabHeader tabUI;
	public TabHeader tabCoin;
	public TabHeader tabOther;

	public GameObject prefabPackItem;
	public GameObject prefabPackUI;
	public GameObject prefabPackCoin;
	public GameObject prefabPackFB;
	public GameObject prefabPackVideo;

	public RectTransform DEFAULT_POSITON;
	public RectTransform content;

	public bool isLoaded = false;

	public float packItemHeight;
	public float packUIHeight;
	public float packCoinHeight;
	public float packFBHeight;
	public float packVideoHeight;

	public Sprite[] iconUIDefault;

	public Text userName;
	public Text hintTxt;
	public Text randomTxt;
	public Text energyTxt;
	public Text coinTxt;

	public Text loading;

	public ArrayList listPackItem = new ArrayList();
	public ArrayList listPackUI = new ArrayList();
	public ArrayList listPackCoin = new ArrayList ();
	public ArrayList listPackOther = new ArrayList ();

	private bool canFreeCoin = false;

	void Start () {
		packItemHeight  = prefabPackItem.GetComponent<RectTransform> ().rect.height;
		packUIHeight    = prefabPackUI.GetComponent<RectTransform> ().rect.height;
		packCoinHeight  = prefabPackCoin.GetComponent<RectTransform> ().rect.height;
		packFBHeight    = prefabPackFB.GetComponent<RectTransform> ().rect.height;
		packVideoHeight = prefabPackVideo.GetComponent<RectTransform> ().rect.height;
	}
	
	// Update is called once per frame
//	void Update () {

//	}

//	void createListPackItem() {
//		content.DetachChildren ();
//		if (listPackItem.Count > 0) {
//			int max = listPackItem.Count;
//			float height = 0;
//			for (int i = 0; i < max; i++) {
//				GameObject instance = (GameObject) listPackItem [i];
//				height += packItemHeight;
//				instance.transform.SetParent (content);
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		} else {
//			ArrayList listItemShop = GSM.getListItemShop();
//			int max = listItemShop.Count;
//			float height = 0;
//			for (int i = 0; i < max; i++) {
//				ItemShopBase itemShopBase = (ItemShopBase) listItemShop[i];
//				if (itemShopBase.item_type == Const.TYPE_ITEM_IN_GAME || itemShopBase.item_type == Const.TYPE_ITEM_REMOVE_ADS) {
//					GameObject instance = Instantiate (prefabPackItem) as GameObject;
//					ItemShop itemShop = instance.GetComponent<ItemShop> ();
//					itemShop.setInfo(itemShopBase);
//					height += packItemHeight;
//					instance.transform.SetParent (content);
//					instance.transform.localScale = new Vector3 (1, 1, 1);
//					instance.SetActive (true);
//					listPackItem.Add (instance);
//				}
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		}
//	}

//	void createListPackUI() {
//		content.DetachChildren ();
//		if (listPackUI.Count > 0) {
//			int max = listPackUI.Count;
//			float height = 0;
//			for (int i = 0; i < max; i++) {
//				GameObject instance = (GameObject) listPackUI [i];
//				height += packUIHeight;
//				instance.transform.SetParent (content);
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		} else {
//			ArrayList listItemShop = GSM.getListItemShop();
//			int max = listItemShop.Count;
//			float height = 0;
//			for (int i = 0; i < max; i++) {
//				ItemShopBase itemShopBase = (ItemShopBase) listItemShop[i];
//				if (itemShopBase.item_type == Const.TYPE_ITEM_UI) {
//					GameObject instance = Instantiate(prefabPackUI) as GameObject;
//					ItemShop itemShop = instance.GetComponent<ItemShop> ();
//					itemShop.setInfo(itemShopBase);
//					height += packUIHeight;
//					instance.transform.SetParent (content);
//					instance.transform.localScale = new Vector3 (1, 1, 1);
//					instance.SetActive (true);
//					listPackUI.Add (instance);
//					instance.GetComponent<UIPackItemController> ().data = itemShopBase;
//				}
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		}
//	}

//	void createListPackCoin() {
//		content.DetachChildren ();
//		if (listPackCoin.Count > 0) {
//			int max = listPackCoin.Count;
//			float height = 0;
//			for (int i = 0; i < max; i++) {
//				GameObject instance = (GameObject)listPackCoin [i];
//				height += packCoinHeight;
//				instance.transform.SetParent (content);
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		} else {
//			ArrayList listItemShop = GSM.getListItemShop ();
//			int max = listItemShop.Count;
//			float height = 0;
//			if (this.canFreeCoin) {
//				GameObject instanceCoin = Instantiate (prefabPackCoin) as GameObject;
//				ItemShop itemShop = instanceCoin.GetComponent<ItemShop> ();
//				ItemShopBase itembase = new ItemShopBase (999, 999, 2, 200, 0, 1, "Free coin", null);
//				itemShop.setInfo (itembase);
//				height += packCoinHeight;
//				instanceCoin.transform.SetParent (content);
//				instanceCoin.transform.localScale = new Vector3 (1, 1, 1);
//				instanceCoin.SetActive (true);
//				listPackCoin.Add (instanceCoin);
//			}
//			for (int i = 0; i < max; i++) {
//				ItemShopBase itemShopBase = (ItemShopBase)listItemShop [i];
//				if (itemShopBase.item_type == Const.TYPE_ITEM_COIN) {
//					GameObject instance = Instantiate (prefabPackCoin) as GameObject;
//					ItemShop itemShop = instance.GetComponent<ItemShop> ();
//					itemShop.setInfo (itemShopBase);
//					height += packCoinHeight;
//					instance.transform.SetParent (content);
//					instance.transform.localScale = new Vector3 (1, 1, 1);
//					instance.SetActive (true);
//					listPackCoin.Add (instance);
//				}
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		}
//	}

//	void createListPackOther() {
//		content.DetachChildren ();
//		if (listPackOther.Count > 0) {
//			int max = listPackOther.Count;
//			float height = 0;
//			for (int i = 0; i < max; i++) {
//				GameObject instance = (GameObject)listPackOther [i];
//				height += instance.GetComponent<RectTransform>().rect.height;
//				instance.transform.SetParent (content);
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		} else {
//			ArrayList listItemShop = GSM.getListItemShop ();
//			int max = listItemShop.Count;
//			float height = 0;
//			//create pack FB
//			GameObject instancePackFB = Instantiate (prefabPackFB) as GameObject;
//			height += packFBHeight + 50;
//			instancePackFB.transform.SetParent (content);
//			instancePackFB.transform.localScale = new Vector3 (1, 1, 1);
//			instancePackFB.SetActive (true);
//			listPackOther.Add (instancePackFB);
//			//create pack Video
//			GameObject instancePackVideo = Instantiate (prefabPackVideo) as GameObject;
//			height += packVideoHeight + 50;
//			instancePackVideo.transform.SetParent (content);
//			instancePackVideo.transform.localScale = new Vector3 (1, 1, 1);
//			instancePackVideo.SetActive (true);
//			listPackOther.Add (instancePackVideo);
//			//
//			for (int i = 0; i < max; i++) {
//				ItemShopBase itemShopBase = (ItemShopBase)listItemShop [i];
//				if (itemShopBase.item_type == 99) {
//					GameObject instance = Instantiate (prefabPackCoin) as GameObject;
//					ItemShop itemShop = instance.GetComponent<ItemShop> ();
//					itemShop.setInfo(itemShopBase);
//					height += packCoinHeight + 20;
//					instance.transform.SetParent (content);
//					instance.transform.localScale = new Vector3 (1, 1, 1);
//					instance.SetActive (true);
//					listPackOther.Add (instance);
//				}
//			}
//			content.sizeDelta = new Vector2 (content.sizeDelta.x, height);
//		}
//	}

//	public void setTab(TAB tab) {
//		currentTab = tab;
////		hideAllRank ();
//		if (tab == TAB.item) {
//			tabItem.setActive (true);
//			tabUI.setActive (false);
//			tabCoin.setActive (false);
//			tabOther.setActive (false);

//		} else if (tab == TAB.ui) {
//			tabItem.setActive (false);
//			tabUI.setActive (true);
//			tabCoin.setActive (false);
//			tabOther.setActive (false);
//		} else if (tab == TAB.coin) {
//			tabItem.setActive (false);
//			tabUI.setActive (false);
//			tabCoin.setActive (true);
//			tabOther.setActive (false);
//		} else if (tab == TAB.other) {
//			tabItem.setActive (false);
//			tabUI.setActive (false);
//			tabCoin.setActive (false);
//			tabOther.setActive (true);
//		}
//	}

//	public void onClickTabItem() {
//		if (currentTab != TAB.item) {
//			setTab (TAB.item);
//			createListPackItem ();
//		}
//	}

//	public void onClickTabUI() {
//		if (currentTab != TAB.ui) {
//			setTab (TAB.ui);
//			createListPackUI ();
//		}
//	}

//	public void onClickTabCoin() {
//		if (currentTab != TAB.coin) {
//			setTab (TAB.coin);
//			createListPackCoin ();
//		}
//	}

//	public void onClickTabOther() {
//		if (currentTab != TAB.other) {
//			setTab (TAB.other);
//			createListPackOther ();
//		}
//	}

//	public void updateUserInfo() {
//		coinTxt.text = StringUtils.getNumberDot (Save.getPlayerCoin ());
//		userName.text = GSM.user.GetString (StringUtils.user_name);
//		hintTxt.text = ItemController.getNumHintItem () + "";
//		randomTxt.text = ItemController.getNumRandomItem () + "";
//		energyTxt.text = ItemController.getNumEnergyItem () + "";
//	}

	//public void onLoad(bool canFreeCoin = false, TAB tab = TAB.item) {
	//	if (!isLoaded) {
	//		if (canFreeCoin) {
	//			this.canFreeCoin = canFreeCoin;
	//		}
	//		createListPackItem ();
	//		setTab (tab);
	//		updateUserInfo ();
	//		isLoaded = true;
	//	}
	//}
}
