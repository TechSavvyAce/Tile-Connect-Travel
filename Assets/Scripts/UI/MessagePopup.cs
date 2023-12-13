using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessagePopup : MonoBehaviour {
	public Text text;
	public RectTransform hidePos;
	public RectTransform showPos;
	RectTransform rect;
	//public GameObject back;
	public GameObject yes;
	public GameObject no;
	public GameObject confirm;

	public Text numHint;
	public Text numRandom;
	public Text numCoin;
	//public GameObject rubyInfo;
	void Start(){
		rect = GetComponent<RectTransform> ();
	}
	public void showPopup(){
		_moveUp ();
		no.SetActive (true);
		yes.SetActive (true);
		numHint.text = "+" + GameConfig.num_hint_bonus;
		numRandom.text = "+" + GameConfig.num_random_bonus;
		numCoin.text = GameStatic.getCoinNeedToRecover() + "";
		GameStatic.coin_need_to_recover = GameStatic.getCoinNeedToRecover ();
		//back.SetActive (true);
		//rubyInfo.SetActive (true);
	}
	public void showPopup(string mes){
		_moveUp ();
		text.text = mes;
		//back.SetActive (true);
		no.SetActive (false);
		yes.SetActive (false);
		confirm.SetActive (true);
	}
	public void showYesNo(string mes){
		_moveUp ();
		text.text = mes;
		//back.SetActive (true);
		no.SetActive (true);
		yes.SetActive (true);
		confirm.SetActive (false);
	}
	void _moveUp(){
        transform.parent.gameObject.SetActive(true);
        GetComponent<Animator>().Play("showpause");
        //iTween.ValueTo(gameObject, iTween.Hash(
        //	"from", hidePos.anchoredPosition,
        //	"to", showPos.anchoredPosition,
        //	"time", 0.2f,
        //	"onupdatetarget", this.gameObject, 
        //	"onupdate", "MoveGuiElement"));
    }
	public void MoveGuiElement(Vector2 position){
		rect.anchoredPosition3D = position;
	}
	public void hidePopup(){
        Invoke("hideParent", 0.2f);
        GetComponent<Animator>().Play("hidepause");
        //iTween.ValueTo(gameObject, iTween.Hash(
        //	"from", showPos.anchoredPosition,
        //	"to", hidePos.anchoredPosition,
        //	"time", 0.2f,
        //	"onupdatetarget", this.gameObject, 
        //	"onupdate", "MoveGuiElement"));
        //if (back != null) {
        //	back.SetActive (false);	
        //}
  //      if (rubyInfo != null) {
		//	rubyInfo.SetActive (false);
		//}
	}

    void hideParent()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
