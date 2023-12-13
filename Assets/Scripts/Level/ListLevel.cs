using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListLevel : MonoBehaviour {
	public static int CurrentLevel= 1;
	public RectTransform DEFAULT_POSITON;
	public RectTransform panel;
	public GameObject levelPrefab;
	public ScrollRect scrollRect;
	public static int level_lock = 2;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey(StringUtils.level_lock)) {
			level_lock = PlayerPrefs.GetInt (StringUtils.level_lock);
		} else {
			PlayerPrefs.SetInt(StringUtils.level_lock,2);
		}
//		levelPrefab.GetComponent<Level> ().SetLevel (1);
		panel.sizeDelta = new Vector2 ((DEFAULT_POSITON.rect.width + 20) * Mathf.FloorToInt((GameConfig.num_level + 1) / 2) + 10, panel.sizeDelta.y);
		scrollRect.horizontalNormalizedPosition = (float) (level_lock-2) / GameConfig.num_level;

		for (int i = 1; i <= GameConfig.num_level; i++) {
			GameObject instance = Instantiate(levelPrefab) as GameObject;
			instance.GetComponent<Level>().SetLevel(i);
			instance.transform.SetParent(transform);
			instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(DEFAULT_POSITON.anchoredPosition.x + Mathf.FloorToInt((i-1)/2 * (DEFAULT_POSITON.rect.width + 20)),
				DEFAULT_POSITON.anchoredPosition.y + (DEFAULT_POSITON.rect.height + 50) * (i % 2 - 1));
			instance.transform.localScale = new Vector3(1,1,1);
			if(i>= level_lock){
				if(!GameConfig.DEBUG_KEY)
				instance.GetComponent<Level>().button.GetComponent<Button>().interactable = false;
				instance.GetComponent<Level>().grayScale();
			}
		}
		levelPrefab.SetActive (false);

		if (GameStatic.canShowRatePopup && Save.getRateStatus() == Const.STATUS_RATE_REMIND) {
			GameStatic.canShowRatePopup = false;
			GameStatic.ratePopup.showPopup();
		}
	}
}
