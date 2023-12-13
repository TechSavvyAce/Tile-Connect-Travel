using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Toast : MonoBehaviour {
	Text text;
	Outline outline;
	RectTransform rect;
	public string infoText;
	// Use this for initialization
	void Start () {

	}

	public void MoveGuiElement(Vector2 position){
		rect.anchoredPosition3D = position;
	}

	void ChangeAlpha(Color color){
		text.color = color;
	}

	void ChangeAlpha2(Color color){
		outline.effectColor = color;
	}

	void OnComplete(){
		gameObject.SetActive (false);
		ToastManager.pool.backToCache (gameObject);
	}

	public void showToast(string info, float time = 2.5f) {
		infoText = info;
		iTween.Stop (gameObject);
		text = GetComponent<Text> ();
		outline = GetComponent<Outline> ();
		rect = GetComponent<RectTransform> ();
		rect.anchoredPosition = new Vector2 (0,-165);
		text.text = info;
//		text.color = new Color (255, 255, 255, 255);
//		outline.effectColor = new Color (0, 0, 0, 255);
		iTween.ValueTo(gameObject, iTween.Hash(
			"from",rect.anchoredPosition,
			"to", new Vector2(rect.anchoredPosition.x,rect.anchoredPosition.y + 50),
			"time", time,
			"easetype", iTween.EaseType.easeOutQuart,
			"onupdatetarget", this.gameObject, 
			"oncomplete","OnComplete", 
			"onupdate", "MoveGuiElement"));
//		iTween.ValueTo(gameObject, iTween.Hash(
//			"from",text.color,
//			"to", new Color(255,255,255,0),
//			"time", 0.5f,
//			"delay", 2.3f,
//			"onupdatetarget", this.gameObject, 
//			"onupdate", "ChangeAlpha"));
//		iTween.ValueTo(gameObject, iTween.Hash(
//			"from",outline.effectColor,
//			"to", new Color(255,255,255,0),
//			"time", 0.5f,
//			"delay", 2.3f,
//			"onupdatetarget", this.gameObject, 
//			"onupdate", "ChangeAlpha2"));
	}
}
