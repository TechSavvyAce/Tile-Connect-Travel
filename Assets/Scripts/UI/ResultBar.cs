using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultBar : MonoBehaviour
{
    public GameObject starBar;
    public Text txtTime;
    public Text txtLevel;
    public Text txtBest;
    //public Text txtReason;

    public Text txtRandom;
    public Text txtHint;
    //public Text txtEnergy;
    //public Text txtResult;

    //public Transform posEnergyBar;
    //public Transform energyBar;

    //public GameObject btnRank;
    public GameObject btnNext;

    public void showFail(string reason, int score)
    {
        transform.parent.gameObject.SetActive(true);

        GameStatic.timeBar.pause();
        GameStatic.map.changeState(Map.MapState.result);

        GetComponent<Animator>().Play("showpause");
        //GetComponent<Animator>().SetTrigger("Running");
        //transform.Find ("BackBlack").gameObject.SetActive (true);
        //txtReason.text = reason;
        txtLevel.text = "Level: " + GameStatic.currentLevel;
        txtTime.text = "Score: " + score;
        txtBest.text = "Best: " + Save.getHighScore();
        //		energyBar.SetParent (posEnergyBar);
        //		energyBar.localPosition = new Vector3 (0, 0, 0);
        //stop sound
        SoundSystem.ins.stop();
        //play sound game over
        SoundSystem.ins.playSoundGameOver();
        SoundSystem.ins.play_music_back();
    }

    public void showResult(int star, int score, int level, bool isVictory)
    {
        transform.parent.gameObject.SetActive(true);

        if (star > 0 && level + 2 > PlayerPrefs.GetInt(StringUtils.level_lock))
            PlayerPrefs.SetInt(StringUtils.level_lock, level + 2);

        GameStatic.map.changeState(Map.MapState.result);
        GameStatic.timeBar.pause();
        //transform.Find ("BackBlack").gameObject.SetActive (true);
        starBar.GetComponent<Animator>().SetBool("OneStar", false);
        starBar.GetComponent<Animator>().SetBool("TwoStar", false);
        starBar.GetComponent<Animator>().SetBool("ThreeStar", false);
        //GetComponent<Animator>().SetTrigger("Running");
        GetComponent<Animator>().Play("showpause");

        //if (star < 3)
        //{
        //    GameObject.Find("Star3").SetActive(false);
        //}
        //if (star < 2)
        //{
        //    GameObject.Find("Star2").SetActive(false);
        //}
        //if (star < 1)
        //{
        //    GameObject.Find("Star1").SetActive(false);
        //}

        if (star >= 1)
        {
            starBar.GetComponent<Animator>().SetBool("OneStar", true);
        }
        if (star >= 2)
        {
            starBar.GetComponent<Animator>().SetBool("TwoStar", true);
        }
        if (star >= 3)
        {
            starBar.GetComponent<Animator>().SetBool("ThreeStar", true);
        }
        Save.saveScore(score, star, level);
        if (star > 0)
        {
#if !UNITY_EDITOR
#endif
            //ParseController.saveLevelScore();
        }
        txtTime.text = "Score: " + score;
        txtLevel.text = "Level: " + level;
        txtBest.text = "Best: " + Save.getHighScore();
        //txtResult.text = isVictory ? "Victory" : "Level Cleared";
        //stop soundback
        SoundSystem.ins.stop();
        //play sound win
        SoundSystem.ins.playSoundGameWin();
        SoundSystem.ins.play_music_back();
        //
        if (GameStatic.currentLevel == GameStatic.maxLevel)
        {
            btnNext.SetActive(false);
            //btnRank.SetActive (true);
        }
        else
        {
            btnNext.SetActive(true);
            //btnRank.SetActive (false);
        }
    }

    public void hideResult()
    {
        Invoke("hideParent", 0.2f);

        starBar.GetComponent<Animator>().SetBool("OneStar", false);
        starBar.GetComponent<Animator>().SetBool("TwoStar", false);
        starBar.GetComponent<Animator>().SetBool("ThreeStar", false);
        //transform.Find ("BackBlack").gameObject.SetActive (false);
        //GetComponent<Animator>().SetTrigger("Disappear");
        GetComponent<Animator>().Play("hidepause");
        starBar.GetComponent<Animator>().SetTrigger("Reset");
        GameStatic.ShowAds();
    }

    void hideParent()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
