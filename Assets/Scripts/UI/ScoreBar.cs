using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {
	public Text scoreTxt;
	public Text levelTxt;
	int score = 0;
	int level = 0;
	// Use this for initialization
	void Start () {
		setInfo(1, 0);
	}
 
	public void SetScore(int score){
		this.score = score;
		updateInfo ();
	}

	public void setLevel(int level) {
		this.level = level;
		updateInfo();
	}

	public void setInfo(int level, int score) {
		this.level = level;
		this.score = score;
		updateInfo();
	}

	void updateInfo(){
		scoreTxt.text = score+"";
		levelTxt.text = level+"/"+GameStatic.maxLevel;
	}
}
