using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ClockCountDown : MonoBehaviour {
	float currentTime, totalTime;
	Action<bool> callback;
	public RectTransform position;
	public Image time;
	public RectTransform clock;
	public Transform direction;
	public Vec2 pos = new Vec2(0, 0);
	public bool isRunning = true;

	void Start(){

	}

	void Update () {
		if (callback != null && isRunning) {
			currentTime += Time.deltaTime;
			time.fillAmount = currentTime / totalTime;
			if (currentTime > totalTime) {
				end ();
			}
			direction.localRotation = Quaternion.Euler (0, 0, -360 * time.fillAmount);
		}
	}

	public void start(float time, Vec2 target,Action<bool> callback){
		this.callback = callback;
		totalTime = time;
		currentTime = 0;
		if (target != null) {
			position.anchoredPosition = GameStatic.map.getUIPosition (target);
			pos = new Vec2 (target.R, target.C);
		}
		position.sizeDelta = new Vector2 (GameStatic.map.getUISize (), GameStatic.map.getUISize ());
		float scale = clock.sizeDelta.x / GameStatic.map.getUISize ();
		position.localScale = new Vector3 (scale,scale,scale);
	}

	public void startAt(float currentTime, float time, Vec2 target,Action<bool> callback){
		StartCoroutine(_startAt (currentTime, time, target, callback));
	}

	IEnumerator _startAt(float currentTime, float time, Vec2 target,Action<bool> callback) {
		yield return new WaitForEndOfFrame ();
		this.callback = callback;
		totalTime = time;
		this.currentTime = currentTime;
		if (target != null) {
			position.anchoredPosition = GameStatic.map.getUIPosition (target);
			pos = new Vec2 (target.R, target.C);
		}
		position.sizeDelta = new Vector2 (GameStatic.map.getUISize (), GameStatic.map.getUISize ());
		float scale = clock.sizeDelta.x / GameStatic.map.getUISize ();
		position.localScale = new Vector3 (scale,scale,scale);
	}

	public void pauseClockCountDown() {
		this.isRunning = false;
	}

	public void playClockCountDown() {
		this.isRunning = true;
	}

	public void end(){
		if (callback != null) {
			callback (false);
		}

		callback = null;
//		Destroy (gameObject);
		gameObject.transform.position = new Vector3(-999, -999, 0);
	}

	public float getCurrentTime() {
		return currentTime;
	}
}
