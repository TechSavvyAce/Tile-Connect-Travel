using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateVersionController : MonoBehaviour {
	public static UpdateVersionController ins;
	public Text content;
	public GameObject objContent;
	void Start(){
		ins = this;
		objContent.SetActive (true);
		gameObject.SetActive (false);
	}

	public void show(string text){
		content.text = text;
		gameObject.SetActive (true);
	}

	public void hide(){
		gameObject.SetActive (false);
	}

	public void openUpdate(){
		GameStatic.openStore ();
	}
}
