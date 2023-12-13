using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseBar : MonoBehaviour {
	public Transform posEnergyBar;
	public Transform energyBar;
	public Text levelText;
	public Text scoreText;
	void Start () {
	}

	public void showResult(){
        transform.parent.gameObject.SetActive(true);
		SoundSystem.ins.pause();
		GameStatic.map.changeState (Map.MapState.pause);
		GameStatic.timeBar.pause ();
		// energyBar.SetParent (posEnergyBar);
		// energyBar.localPosition = new Vector3 (0, 0, 0);
		//transform.Find ("BackBlack").gameObject.SetActive (true);
        GetComponent<Animator>().Play("showpause");//SetTrigger("");
		levelText.text = "Level: " + GameStatic.currentLevel;
		scoreText.text = "Score: " + GameStatic.currentScore;

        //StartCoroutine(showFullAds());
	}

    //IEnumerator showFullAds()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    GameStatic.ShowAds();
    //}

    public void hideResult(){
        Invoke("hideParent", 0.2f);

        SoundSystem.ins.play();
		//transform.Find ("BackBlack").gameObject.SetActive (false);
        GetComponent<Animator>().Play("hidepause");//SetTrigger("hidepause");
		GameStatic.timeBar.continueRun ();
		GameStatic.map.changeState (Map.MapState.playing);
	}

    void hideParent()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
