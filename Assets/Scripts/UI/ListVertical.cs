using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ListVertical : MonoBehaviour {
	public int NUM_COL =1;
	public int padding = 10;
	public GameObject element;
	public GameObject loading;
	public Transform element_parent;
	public RectTransform cell;
	public RectTransform panel;
	public ScrollRect scrollRect;
	int DataVersion =0;
	int data_version = 0;
	public int my_rank=-1;

	public PoolGameObject pool = new PoolGameObject();
	void Update(){
		if (data_version < DataVersion) {
			data_version = DataVersion;
			updateData ();
		}
	}

	public virtual void updateData(){
		int i = 0;
		pool.deleteAllCurrent();
		panel.sizeDelta = new Vector2 (panel.sizeDelta.x,(float) (Math.Ceiling((float)i/NUM_COL) * cell.sizeDelta.y) + Math.Abs(cell.anchoredPosition.y));
	}

	public void scrollToTop(){
		scrollRect.verticalNormalizedPosition = 1;
	}

	public void showLoading(){
		if (loading != null)
			loading.SetActive (true);
	}

	public void hideLoading(){
		if (loading != null)
			loading.SetActive (false);
	}
}