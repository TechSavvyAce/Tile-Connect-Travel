using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmotionItem : MonoBehaviour {
	public Sprite[] listIcon;
	public Image image;
	public Text text;
	public enum EmotionType {happy,angry,cry,smile,text};
	public string[] text_emo= {"Good luck!","Well played!","Wow!","Thanks","Good game!","Facebook"};
	public int text_index;
	public EmotionType type;

	public void setType(EmotionType value){
		type = value;
		if (value == EmotionType.text)
			text.text = text_emo [text_index];
		else
			image.sprite = listIcon [(int)value];

	}

	public bool isClickAble = true;

	public void onClick(){
		if (!isClickAble)
			return;
	}
}
