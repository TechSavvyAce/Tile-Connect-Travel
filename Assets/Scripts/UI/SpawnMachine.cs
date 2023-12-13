using UnityEngine;
using System.Collections;

public class SpawnMachine : MonoBehaviour {
	public GameObject element;
	public float time_min = 0.2f;
	public float time_max = 1f;
	public float force_max = 15000f;
	public float force_min = 10000f;
	float last_time = -1;
	float curTime = 0;
	void Update () {
		if (last_time < 0)
			last_time = time_min + (time_max - time_min) * Random.value;
		curTime += Time.deltaTime;
		if (curTime > last_time) {
			curTime = 0;
			last_time = time_min + (time_max - time_min) * Random.value;
			spawn ();
		}
	}

	public void spawn(){
		GameObject instance = Instantiate (element) as GameObject;
		instance.SetActive (true);
		instance.transform.SetParent (element.transform.parent);
		instance.transform.localScale = new Vector3 (1, 1, 1);
		instance.transform.localPosition = element.transform.localPosition;
		float forceY = force_min + (force_max - force_min) * Random.value;
		float forceX = -force_min + (2*force_min) * Random.value;
		instance.GetComponent<Rigidbody2D> ().AddForce (new Vector2(forceX,forceY));
	}
}
