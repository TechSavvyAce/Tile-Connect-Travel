using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;


public class UIPackController : MonoBehaviour {
	public static bool hasData = false;
	public bool isUpdate = false;

	public RectTransform DEFAULT_POSITON;
	public RectTransform cell;
	public RectTransform panel;

	public  GameObject viewListItem;
	public static GameObject staticViewListItem;
	void Start(){
		staticViewListItem = viewListItem;
	}

	// Update is called once per frame
	//void Update () {
	//	if (hasData && !isUpdate) {
	//		isUpdate = true;
	//		for (int i =0; i < data.Count; i++) {
	//			GameObject instance = Instantiate(Resources.Load("Prefab/UIPackItem", typeof(GameObject))) as GameObject;
	//			UIPackItemController item = instance.GetComponent<UIPackItemController> ();
	//			item.data.data = data [i];
	//			instance.transform.SetParent(panel.transform);
	//			RectTransform rectTransform = instance.GetComponent<RectTransform> ();
	//			rectTransform.anchoredPosition = new Vector2(DEFAULT_POSITON.anchoredPosition.x,DEFAULT_POSITON.anchoredPosition.y - cell.rect.height * (i+1));
	//			rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
	//			rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
	//			instance.transform.localScale = new Vector3(1,1,1);
	//		}
	//		panel.sizeDelta = new Vector2 (panel.sizeDelta.x,panel.sizeDelta.y+ data.Count * cell.sizeDelta.y);
	//	}
	//}

	public void hideViewListItem(){
		viewListItem.SetActive (false);
	}
}
