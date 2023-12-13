using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour {
	public Object prefap_pikachu;
	public static float timeLive = 30;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 30; i++) {
			GameObject g = Instantiate (prefap_pikachu) as GameObject;
			Sprite sprite = Resources.Load ("Images/item/item" + (int)(Random.value*30), typeof(Sprite)) as Sprite;
			g.GetComponent<Image> ().sprite = sprite;
			g.GetComponent<Animal> ().TimeLive = timeLive + i * 2;
			iTween.MoveTo (g, iTween.Hash ("path", iTweenPath.GetPath ("MainPath"), "time", MainSceneUI.timeLive, "delay", i*2,"easetype",iTween.EaseType.easeInOutSine));
			g.transform.SetParent (transform);
		}
	}
}
