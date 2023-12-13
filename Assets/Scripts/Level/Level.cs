using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	int level;
	int star;
	public Text txtLevel;
	public Sprite gray;
	public MessagePopup messagePopup;
	public RawImage friend1;
	public RawImage friend2;
	public Image friendBack;
	public GameObject friendImage;
	bool _isUpdate = false;
	public static bool isUpdate = false;
	public GameObject button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!_isUpdate && isUpdate ) {
			_isUpdate = true;
			reset ();
		}
	}

	public void SetLevel(int level){
		txtLevel.text = (level + "");
		this.level = level;
		star = PlayerPrefs.GetInt(StringUtils.number_star_level + level);
		if (star <= 2)
		gameObject.transform.Find ("Star3").gameObject.GetComponent<Star> ().SetActive (false);
		if (star <= 1)
			gameObject.transform.Find ("Star2").gameObject.GetComponent<Star> ().SetActive (false);
		if (star <= 0)
			gameObject.transform.Find ("Star1").gameObject.GetComponent<Star> ().SetActive (false);
	}

	public void reset(){
		List<string> listImages = ParseController.getListUser (level);
		if (listImages == null)
			return;
		friendBack.fillAmount = 1;
		updateImage (0, friend1, listImages);
		updateImage (1, friend2, listImages);
	}

	public void updateImage(int i, RawImage image,List<string> listImages){
		if (i >= listImages.Count) {
			image.gameObject.SetActive (false);
			if (i == 1) {
				friendBack.fillAmount = 0.5f;
			}
			return;
		}
		image.gameObject.SetActive (true);
		friendImage.gameObject.SetActive (true);
		string id = listImages[i];
//		if (FacebookController.FriendImages.ContainsKey (id)) {
//			image.texture = FacebookController.FriendImages [id];
//		} else {
//			// We don't have this players image yet, request it now
//			FBGraph.LoadFriendImgFromID (id, pictureTexture => {
//				if (pictureTexture != null) {
//					if(!FacebookController.FriendImages.ContainsKey(id)){
//						FacebookController.FriendImages.Add (id, pictureTexture);
//					}
//					if(!image.IsDestroyed())
//						image.texture = pictureTexture;
//				}
//			});
//		}
	}

	public void choose(){
		ListLevel.CurrentLevel = this.level;
//		if (ParseUser.CurrentUser != null && ParseController.friends !=null)
			LevelRank.ins.showPopup ();
//		else {
//			LevelRank.ins.play ();
//		}
	}

	public void grayScale(){
		transform.Find("Back").GetComponent<Image> ().sprite = gray;
		transform.Find ("Lock").gameObject.SetActive (true);
	}
}