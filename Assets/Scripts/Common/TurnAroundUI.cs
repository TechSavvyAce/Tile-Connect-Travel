using UnityEngine;
using System.Collections;

public class TurnAroundUI : MonoBehaviour {
	float timeCounter = 0;
	public float speed = 3;
	public float width = 30;
	public float height = 30;
	RectTransform trans;
	Vector2 def;
	void Start(){
		trans = GetComponent<RectTransform> ();
		def = trans.anchoredPosition;
	}
	void Update () {
		timeCounter += Time.deltaTime * speed;

		float x = Mathf.Cos(timeCounter) * width;
		float y = Mathf.Sin(timeCounter) * height;
		trans.anchoredPosition = new Vector2(x,y) + def;
	}
}
