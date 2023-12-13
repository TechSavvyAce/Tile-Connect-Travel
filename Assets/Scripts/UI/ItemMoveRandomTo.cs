using UnityEngine;
using System.Collections;

public class ItemMoveRandomTo : MonoBehaviour {
	public GameObject targetObj;
	public float timeDelay = 2;
	// Use this for initialization
	void Start () {
		StartCoroutine (StartAnimate ());
	}

	IEnumerator StartAnimate ()
	{
		yield return new WaitForSeconds (timeDelay);
		GameObject item = gameObject;
		AnimationCurve curveX = new AnimationCurve (new Keyframe (0, item.transform.position.x), new Keyframe (0.4f, targetObj.transform.position.x));
		AnimationCurve curveY = new AnimationCurve (new Keyframe (0, item.transform.position.y), new Keyframe (0.5f, targetObj.transform.position.y));
		curveY.AddKey (0.2f, item.transform.position.y + UnityEngine.Random.Range (-2, 0.5f));
		float startTime = Time.time;
		Vector3 startPos = item.transform.position;
		float speed = UnityEngine.Random.Range (0.4f, 0.6f);
		float distCovered = 0;
		while (distCovered < 0.5f) {
			distCovered = (Time.time - startTime) * speed;
			item.transform.position = new Vector3 (curveX.Evaluate (distCovered), curveY.Evaluate (distCovered), 0);
			item.transform.Rotate (Vector3.back, Time.deltaTime * 1000);
			yield return new WaitForFixedUpdate ();
		}
		Destroy (item);
	}
}
