using UnityEngine;
using System.Collections;

public class SettingBar : Popup {
    public RectTransform hidePos;
    public RectTransform showPos;

    private bool isOpen =false;
    private RectTransform rect;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        //init ();
    }

	public void toogle(){
		isOpen = !isOpen;
		if (isOpen) {
            showPopup();
        } else {
            hidePopup();
        }
	}
    //public void hide(){
    //	hidePopup ();
    //	isOpen = false;
    //}

    void showPopup()
    {
        if (rect == null)
            rect = GetComponent<RectTransform>();
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", rect.anchoredPosition,
            "to", new Vector2(rect.anchoredPosition.x, showPos.anchoredPosition.y),
            "time", 0.2f,
            "onupdatetarget", this.gameObject,
            "onupdate", "MoveGuiElement"));
    }

    public void MoveGuiElement(Vector2 position)
    {
        rect.anchoredPosition3D = position;
    }

    public void hidePopup()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", rect.anchoredPosition,
            "to", new Vector2(rect.anchoredPosition.x, hidePos.anchoredPosition.y),
            "time", 0.2f,
            "onupdatetarget", this.gameObject,
            "onupdate", "MoveGuiElement"));
    }
}
