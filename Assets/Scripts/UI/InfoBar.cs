using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour {
	string textContent = "Level\n_level \n\nScore\n_tiles";
	Text text;
	int level = 1;
	int scores = 0;
	// Use this for initialization
	void Start () {

	}
	
	public void SetLevel(int level){
		this.level = level;
		updateInfo ();
	}

	public void SetScore(int score){
		this.scores = score;
		updateInfo ();
	}

	void updateInfo(){
		text = GetComponent<Text> ();
		text.text = textContent.Replace ("_level", "" + level).Replace ("_tiles", "" + scores);
	}
}
