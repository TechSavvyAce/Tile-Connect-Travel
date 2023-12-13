using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;


public class TimeBar : MonoBehaviour {
	float totalTime = 100;
	float currentTime = 0;
	bool isRun = false;
	public ResultBar levelFail;
	public Image fillImage;
	public RectTransform start;
	public RectTransform end;
	float timeStar1;
	float timeStar2;
	float timeStar3;
	int numStar = 3;
	bool _isTrigger1MinSound = false;
	// Update is called once per frame
	void Update () {
		if (isRun) {
            currentTime -= Time.smoothDeltaTime;
			if (!_isTrigger1MinSound && currentTime < 60) {
				_isTrigger1MinSound = true;
				SoundSystem.ins.play_time_over_in_1_min ();
			}
			fillImage.fillAmount = currentTime/totalTime;
			if(currentTime <= 0){
				isRun =false;
				timeOut();
			}
			if(totalTime - currentTime > timeStar1 && numStar == 3){
				numStar = 2;
				transform.Find("Star1").gameObject.SetActive(false);
			}
			if(totalTime - currentTime > timeStar2 && numStar == 2){
				numStar = 1;
				transform.Find("Star2").gameObject.SetActive(false);
			}
			if(totalTime - currentTime > timeStar3 && numStar == 1){
				numStar = 0;
				transform.Find("Star3").gameObject.SetActive(false);
			}
		}
	}

	public void startBar(float time){
		totalTime = time;
		currentTime = totalTime;
		isRun = true;
		numStar = 3;
		_isTrigger1MinSound = false;
	}

	public void startBar(float totalTime, float currentTime) {
//		totalTime = 110;
//		currentTime = 5;
		this.totalTime = totalTime;
		this.currentTime = currentTime;
		isRun = true;
		numStar = 3;
		_isTrigger1MinSound = false;
	}

	public void setTarget(float timeStar1, float timeStar2, float timeStar3){
		this.timeStar1 = timeStar1;
		this.timeStar2 = timeStar2;
		this.timeStar3 = timeStar3;
		float dis = start.anchoredPosition.x - end.anchoredPosition.x;
		transform.Find("Star1").gameObject.SetActive(true);
		transform.Find("Star2").gameObject.SetActive(true);
		transform.Find("Star3").gameObject.SetActive(true);
		transform.Find ("Star1").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (start.anchoredPosition.x - timeStar1 * dis / totalTime, start.anchoredPosition.y);
		transform.Find ("Star2").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (start.anchoredPosition.x - timeStar2 * dis / totalTime, start.anchoredPosition.y);
		transform.Find ("Star3").GetComponent<RectTransform> ().anchoredPosition = new Vector2 (start.anchoredPosition.x - timeStar3 * dis / totalTime, start.anchoredPosition.y);
	}

	public void pause(){
		isRun = false;
	}

	public void continueRun(){
		isRun = true;
	}
	public int getNumStar(){
		return numStar;
	}

	public int getTimeRun(){
		return (int) (totalTime - currentTime);
	}

	public int getTimeRemain() {
		return (int) currentTime;
	}

	void timeOut(){
		numStar = 0;
		GameStatic.map.gameOver (true);
	}
}
