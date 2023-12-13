using UnityEngine;
using System.Collections;

public class CustomDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public class p {
		public static string pDocument ;
		public static void log (string text) {
			pDocument+="\n"+text;
			Debug.Log (text+ " PikaDebug ");
		}
	}
	void OnGUI () {
//		GUI.TextArea (new Rect (0, 0, 100, 200), p.pDocument);
	}

	public static void Log(string text){
		p.log (text);
	}
}
