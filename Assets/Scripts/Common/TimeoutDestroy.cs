using UnityEngine;
using System.Collections;

public class Timeout : MonoBehaviour {
	public float TIME_OUT = 1.5f;
	void Start () {
		StartCoroutine (destroy ());
	}

	public IEnumerator destroy(){
		yield return new WaitForSeconds (TIME_OUT);
		Destroy (gameObject);
	}
}
