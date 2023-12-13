using UnityEngine;
using System.Collections;

public class AutoGenData {

	public int type;
	public float timeWait;
	public float timeRun;

	public AutoGenData (int type, float timeWait, float timeRun) {
		this.type = type;
		this.timeWait = timeWait;
		this.timeRun = timeRun;
	}
}
