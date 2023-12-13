using UnityEngine;
using System.Collections;

public class ToastManager : MonoBehaviour {

	// Use this for initialization
	public static PoolGameObject pool = new PoolGameObject ();

	public static GameObject instance;
	public static void showToast(string info,bool useLastToast = false, float time = 2.5f){
		if (pool == null) {
			pool = new PoolGameObject ();
		}
		if (!useLastToast) {
			if (instance != null && instance.GetComponent<Toast> ().infoText.Equals (info) && instance.activeSelf) {
			} else {
				instance = pool.getInCache ();
			}
		}
		if (instance == null) {
			instance = Instantiate (Resources.Load ("Prefab/Toast", typeof(GameObject))) as GameObject;
			pool.cacheNew (instance);
		}
		instance.transform.SetParent (GameStatic.toastPool.transform);
		instance.transform.localScale = new Vector3 (1, 1, 1);
		Toast toast = instance.GetComponent<Toast> ();
		toast.showToast (info, time);
	}

	public static void hideLastToast(){
		if (instance != null) {
			instance.SetActive (false);
			pool.backToCache (instance);
		}
	}

	public static void logToastLength() {
		Debug.Log("list available : " + pool.listAvai.Count);
		Debug.Log("list in use : " + pool.listInUse.Count);
	}
}
