using UnityEngine;
using System.Collections;

public class PathItem : MonoBehaviour {
	float timeToLive = 0;
	bool isLive = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLive) {
			timeToLive -= Time.deltaTime;
			if(timeToLive <= 0){
				Destroy(gameObject);
			}
		}
	}

	public void live(float time){
		timeToLive = time;
		isLive = true;
	}
}
