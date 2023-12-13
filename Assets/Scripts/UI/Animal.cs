using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {
	float time;
	public float TimeLive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > TimeLive) {
			Destroy (gameObject);
		}
	}
}
