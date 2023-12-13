using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	public static Hashtable collection = new Hashtable();
	// Use this for initialization
	void Awake () {

		if (collection[gameObject.name] == null) // check to see if the instance has a reference
		{
			collection[gameObject.name] = 1; // if not, give it a reference to this class...
			DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes
			Debug.Log ("Dont destroy awake");
		} else // if we already have a reference then remove the extra manager from the scene
		{
			Destroy(this.gameObject);
			Debug.Log ("Destroy awake because repeat object " + gameObject.name );
		}
	}
}
