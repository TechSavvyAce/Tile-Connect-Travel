using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIPackListItemController : MonoBehaviour {
	int NUMBER_COL = 4;

	public static bool hasData = false;

	public RectTransform DEFAULT_POSITON;
	public RectTransform cell;
	public RectTransform panel;

	public Text loading;
	List<GameObject> list = new List<GameObject>();

	public void clearObject(){
		foreach (GameObject element in list) {
			DestroyObject (element);
		}
		list.Clear ();
	}

	// Update is called once per frame
	//void Update () {
	//	if (hasData) {
	//		loading.text = "";
	//		hasData = false;
	//		for (int i =0; i < data.Count; i++) {
	//			GameObject instance = Instantiate(Resources.Load("Prefab/Image", typeof(GameObject))) as GameObject;
	//			instance.transform.SetParent(panel.transform);
	//			instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(DEFAULT_POSITON.anchoredPosition.x + (i%NUMBER_COL) * cell.rect.width, DEFAULT_POSITON.anchoredPosition.y - cell.rect.height * (int)(i/NUMBER_COL));
	//			instance.transform.localScale = new Vector3(1,1,1);
	//			ImageDownloader.LoadImgFromURL (data [i].GetString (GSM.url), texture => {
	//				if(instance!= null && instance.activeSelf && !instance.GetComponent<RawImage>().IsDestroyed() && texture != null){
	//					RawImage image = instance.GetComponent<RawImage>();
	//					image.texture = texture;
	//				}
	//			});
	//			list.Add (instance);
	//		}
	//		panel.sizeDelta = new Vector2 (panel.sizeDelta.x,DEFAULT_POSITON.anchoredPosition.y+ (data.Count + 4) * cell.sizeDelta.y/4);
	//	}
	//}
}
