using UnityEngine;
using System.Collections;

public class AutoGenController : MonoBehaviour {

	public AutoGenData autoGenData;
	public ClockCountDown clockCountDown;
	public Map map;
	public Vec2 pos = new Vec2(0, 0);

	public enum AUTOGEN_STATE{STATE_WAITING, STATE_RUNNING, STATE_END};
	AUTOGEN_STATE state = AUTOGEN_STATE.STATE_WAITING;
	private float currentTime;
	private bool isLock, isPlay = true;

	// Use this for initialization
	void Start () {
		map = GameObject.Find ("Map").GetComponent<Map> ();
		this.transform.SetParent (GameObject.Find ("First").transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlay && autoGenData != null) {
			currentTime += Time.deltaTime;
			if (currentTime > autoGenData.timeWait && state == AUTOGEN_STATE.STATE_WAITING) {
				state = AUTOGEN_STATE.STATE_RUNNING;
				Vec2 pos = (Vec2)map.getNextAutoGenFrozenPos ();
				if (pos == null) {
					lockAutoGen ();
				}
				this.pos = pos;
				clockCountDown.start (autoGenData.timeRun, pos, done => {
					if (!isLock) {
						handleAutoGen(pos);
					}
					currentTime = 0;
					isLock = false;
					state = AUTOGEN_STATE.STATE_WAITING;
				});
			}
		}
	}

	public void setAutoGenData (AutoGenData autoGenData) {
		this.autoGenData = autoGenData;
	}

	public void lockAutoGen() {
		this.isLock = true;
		clockCountDown.gameObject.transform.position = new Vector3(-999, -999, 0);
	}

	public void pauseClockCountDown() {
		this.isPlay = false;
		clockCountDown.pauseClockCountDown();
	}

	public void playClockCountDown() {
		this.isPlay = true;
		clockCountDown.playClockCountDown();
	}

	public void disableClockCountDown() {
		this.isPlay = false;
		clockCountDown.pauseClockCountDown();
		clockCountDown.gameObject.transform.position = new Vector3(-999, -999, 0);
	}

	public void handleAutoGen(Vec2 pos) {
		if (autoGenData.type == Const.AUTO_GEN_FROZEN) {
			map.list_frozen_put.Add (pos.R + "_" + pos.C, pos);
		} else if (autoGenData.type == Const.AUTO_GEN_POKEMON) {
		
		} else if (autoGenData.type == Const.AUTO_GEN_FROZEN_AND_POKEMON) {
		
		}
	}

	public float getCurrentTime() {
		return currentTime;
	}

	public void setCurrentTime(float currentTime) {
		this.currentTime = currentTime;
	}

	public void setAutoGenControllerData(AutoGenData autoGenData, float currentTime, float currentClockTime, Vec2 pos) {
		this.autoGenData = autoGenData;
		this.currentTime = currentTime;
		this.pos = pos;
		state = AUTOGEN_STATE.STATE_RUNNING;
		clockCountDown.startAt (currentClockTime, autoGenData.timeRun, pos, done => {
			if (!isLock) {
				handleAutoGen(pos);
			}
			this.currentTime = 0;
			isLock = false;
			state = AUTOGEN_STATE.STATE_WAITING;
		});
	}
}
