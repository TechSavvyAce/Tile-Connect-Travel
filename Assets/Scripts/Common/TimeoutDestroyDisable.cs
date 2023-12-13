using UnityEngine;
using System.Collections;

public class TimeoutDisable : MonoBehaviour {
	public float TIME_OUT = 1.5f;
	void Start () {
		StartCoroutine (doSth ());
	}

	public IEnumerator doSth(){
		yield return new WaitForSeconds (TIME_OUT);
		gameObject.SetActive (false);
	}
}
