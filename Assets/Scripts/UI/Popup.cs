using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Popup : MonoBehaviour {
    //public RectTransform hidePos;
    //public RectTransform showPos;
    //RectTransform rect;
    bool show = false;
    //   void Start(){
    //	init ();
    //}
    //public void init(){
    //	rect = GetComponent<RectTransform> ();
    //	show = false;
    //}
    public void showPopup(){
        transform.parent.gameObject.SetActive(true);
        GetComponent<Animator>().Play("showpause");
        //_moveUp ();
        show = true;
	}

	//void _moveUp(){
	//	if(rect == null) 
	//		rect = GetComponent<RectTransform> ();
	//	iTween.ValueTo(gameObject, iTween.Hash(
	//		"from",rect.anchoredPosition,
	//		"to", new Vector2(rect.anchoredPosition.x,showPos.anchoredPosition.y),
	//		"time", 0.2f,
	//		"onupdatetarget", this.gameObject, 
	//		"onupdate", "MoveGuiElement"));
	//}
	//public void MoveGuiElement(Vector2 position){
	//	rect.anchoredPosition3D = position;
	//}

	public void hidePopup(){
        Invoke("hideParent", 0.2f);
        //transform.Find ("BackBlack").gameObject.SetActive (false);
        GetComponent<Animator>().Play("hidepause");
        //iTween.ValueTo(gameObject, iTween.Hash(
        //	"from", rect.anchoredPosition,
        //	"to",new Vector2(rect.anchoredPosition.x,hidePos.anchoredPosition.y),
        //	"time", 0.2f,
        //	"onupdatetarget", this.gameObject, 
        //	"onupdate", "MoveGuiElement"));
        show = false;
    }

    void hideParent()
    {
        transform.parent.gameObject.SetActive(false);
    }

    //public bool isShow(){
    //	return show;
    //}
}
