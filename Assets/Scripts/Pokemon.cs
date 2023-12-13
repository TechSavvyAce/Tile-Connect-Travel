using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pokemon : MonoBehaviour {
	
	public int id;
	public int onlineId;
	public Vec2 POS;
	public GameObject back;
	public GameObject backDefault;
	public GameObject backNew;
	public GameObject backKawai;
	public GameObject backBox;
	private Vector3 localScale;
	private Animator animator;
	private bool isHint = false;
	private int state;
	private bool isFrozen;
	public static List<Sprite> sprites = new List<Sprite>();
	public List<Sprite> _sprites = new List<Sprite>();

	Map map;

	void Start () {
		animator = GetComponent<Animator> ();
		map = transform.parent.gameObject.GetComponent<Map>();
		state = Const.STATE_NORMAL;
		if (_sprites.Count > 0) {
			sprites = _sprites;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isHint && animator.GetCurrentAnimatorStateInfo (0).IsName ("ItemDefault")) {
			Hint();
		}

	}

	public void setInfo(int id, int row, int col, Vector3 pos, int width, int height, Transform parent) {
		this.id = id;
		this.POS = new Vec2 (row, col);
		transform.position = pos;
		transform.parent = parent;
		Sprite sprite = reloadUI ();
		backBox.SetActive(false);
		name = "pokemon_clone_" + row + "_" + col;
	}

	public Sprite reloadUI(){
		if (isSpecial ())
			return null;
		Sprite sprite = null;
		backDefault.SetActive (false);
		backKawai.SetActive (false);
		backNew.SetActive (false);
		if (Save.isShowClassicUI ()) {
			/*if (Save.getUsedPack () % 3 == Const.UI_DEFAULT_POKEMON) {
				sprite = GetSprite ("a" + id);
				back = backDefault;
			} else if (Save.getUsedPack () % 3 == Const.UI_DEFAULT_KAWAI) {
				sprite = GetSprite ("b" + id);
				back = backKawai; 
			} else if (Save.getUsedPack () % 3 == Const.UI_DEFAULT_FAKE) {
				sprite = GetSprite ("c" + id);
				back = backNew;
			}else {
				//sprite = ImageDownloader.loadSprite("pack" + Save.getUsedPack() + "item" + (id +1) + ".png");
			}*/
			Debug.Log(Save.country_key);
			
			sprite = GetSprite(Save.country_key + id);
			Debug.Log(sprite);
			back.SetActive (true);
			GetComponent<SpriteRenderer>().sprite = sprite;
			localScale = new Vector3(Mathf.Abs(Map.CELL_WIDH * 1.0f / sprite.bounds.size.x), Mathf.Abs (- Map.CELL_HEIGHT * 1.0f / sprite.bounds.size.y), 1);
			if (Save.getUsedPack () % 3 == Const.UI_DEFAULT_FAKE) {
				localScale = localScale * 29.8f/40f;
			}
		} else {
			/*if (Save.getUsedPack () % 2 == Const.UI_DEFAULT_POKEMON) {
				sprite = GetSprite (id + "a");
				back = backDefault;
			} else if (Save.getUsedPack () % 2 == Const.UI_DEFAULT_KAWAI) {
				sprite = GetSprite (id + "b");
				back = backNew;
			}else {
				//sprite = ImageDownloader.loadSprite("pack" + Save.getUsedPack() + "item" + (id +1) + ".png");
			}*/
			sprite = GetSprite(Save.country_key + id);
			back.SetActive (true);
			GetComponent<SpriteRenderer>().sprite = sprite;
			localScale = new Vector3(Mathf.Abs(Map.CELL_WIDH * 1.0f / sprite.bounds.size.x), Mathf.Abs (- Map.CELL_HEIGHT * 1.0f / sprite.bounds.size.y), 1);
			if (Save.getUsedPack () % 2 == Const.UI_DEFAULT_KAWAI) {
				localScale = localScale * 29.8f/40f;
			}
		}

        transform.localScale = localScale;
        return sprite;
	}

	public void setOrder(){
		int order =  ((Map.ins.row - 2 - POS.R) * (Map.ins.row -2) + POS.C)*2;
		back.GetComponent<SpriteRenderer> ().sortingOrder = order;
		GetComponent<SpriteRenderer> ().sortingOrder = order + 1;
	}

	public void setSpecialOrder(){
		GetComponent<SpriteRenderer> ().sortingOrder = 1000;
	}

	public void setSpecialInfo(int id, int row, int col, Vector3 pos, int width, int height, Transform parent) {
		this.id = id;
		this.POS = new Vec2 (row, col);
		transform.position = pos;
		transform.parent = parent;
		string item_name = "";
		if (id == Const.STONE_FIXED_ID) {
			item_name = "stone3d";
			transform.position = new Vector3 (pos.x - width / 2, pos.y + height / 2, pos.z);
		} else if (id == Const.STONE_MOVING_ID) {
			item_name = "Crate";
			backKawai.SetActive (false);
			backDefault.SetActive (false);
			backNew.SetActive (false);
			backBox.SetActive (true);
			back = backBox;
		} else if (id == Const.FROZEN_FIXED_ID) {
			item_name = "IceBox";
			setSpecialOrder ();
		}
//		Sprite sprite = Resources.Load("Images/SpecialItem/" + item_name, typeof(Sprite)) as Sprite;
		Sprite sprite = GetSprite(item_name);
		GetComponent<SpriteRenderer>().sprite = sprite;
		localScale =new Vector3(Mathf.Abs(width * 1.0f / sprite.bounds.size.x), Mathf.Abs (- height * 1.0f / sprite.bounds.size.y), 1);
		if (id == Const.STONE_FIXED_ID) {
			localScale = localScale * 27f / 26f;
		}
		transform.localScale = localScale;
		name = "pokemon_clone_" + row + "_" + col;
	}

	public void setSpecialInfo(int id, string special_name, int row, int col, Vector3 pos, int width, int height, Transform parent){
		this.id = id;
		this.POS = new Vec2 (row, col);
		transform.position = pos;
		transform.parent = parent;
		string item_name = "";
		if (id == Const.STONE_FIXED_ID) {
			item_name = "stone3d";
			transform.position = new Vector3 (pos.x - width / 2, pos.y + height / 2, pos.z);
		} else if (id == Const.STONE_MOVING_ID) {
			item_name = "Crate";
			back = backBox;
			backKawai.SetActive (false);
			backDefault.SetActive (false);
			backNew.SetActive (false);
			backBox.SetActive (true);
		} else if (id == Const.FROZEN_FIXED_ID) {
			item_name = "IceBox";
			setSpecialOrder ();
		}
//		Sprite sprite = Resources.Load("Images/SpecialItem/" + item_name, typeof(Sprite)) as Sprite;
		Sprite sprite = GetSprite(item_name);
		GetComponent<SpriteRenderer>().sprite = sprite;
		localScale =new Vector3(Mathf.Abs(width * 1.0f / sprite.bounds.size.x), Mathf.Abs (- height * 1.0f / sprite.bounds.size.y), 1);
		if (id == Const.STONE_FIXED_ID) {
			localScale = localScale * 27f / 26f;
		}
		transform.localScale = localScale;
		name = special_name + row + "_" + col;
	}

	public bool isSpecial(){
		if (id == Const.STONE_FIXED_ID) {
			return true;
		} else if (id == Const.STONE_MOVING_ID) {
			return true;
		} else if (id == Const.FROZEN_FIXED_ID) {
			return true;
		}
		return false;
	}
	
	public void updateInfo(Vec2 POS, Vector3 pos, float time_move) {
		this.POS = POS;
		setOrder ();
		name = "pokemon_clone_" + POS.R + "_" + POS.C;
		iTween.MoveTo (gameObject, pos, time_move);
	}

	public void setFrozen(bool isFrozen) {
		this.isFrozen = isFrozen;
	}
	
	public bool checkIsFrozen()
	{
		return isFrozen;
	}

	public void setState(int state) {
		this.state = state;
	}

	public int getState() {
		return this.state;
	}

	public void setOnlineId (int onlineId) {
		this.onlineId = onlineId;
	}

	public void scaleToDefault(){
		transform.localScale = localScale;
	}

	GameObject selectPaticle = null;
	public void Select(){
		GetComponent<SpriteRenderer> ().color = new Color (188/255f,255/255f,196/255f);
		back.GetComponent<SpriteRenderer> ().color = new Color (71/255f,39/255f,39/255f,200/255f);
		if (selectPaticle == null) {
			selectPaticle = Instantiate (Resources.Load ("Prefab/Select")) as GameObject;
			selectPaticle.transform.SetParent (transform);
			selectPaticle.transform.localPosition = new Vector3 (0, 0, 0);
			selectPaticle.name = "Select";
		}
	}
	public void DeSelect(){
		GetComponent<SpriteRenderer> ().color = Color.white;
		back.GetComponent<SpriteRenderer> ().color = Color.white;
		if(selectPaticle != null)
			Destroy(selectPaticle);
	}
	public void Hint(){
		animator.SetTrigger("Hint");
		isHint = true;
	}
	public void RemoveHint(){
		try {
			if (!isActiveAndEnabled)
				return;
			if (animator != null)
				animator.SetTrigger ("RemoveHint");
			isHint = false;
		} catch (Exception e) {
			CustomDebug.Log (e.ToString ());
		}
	}
	public void RemoveAnimator(){
	}

	public void Eat(bool isOffline){
		map.changeState (Map.MapState.eating);
		GetComponent<Animator> ().SetTrigger ("Dissapear");
		StartCoroutine (Disappear (isOffline));
		setState(Const.STATE_DISAPPEARING);
		map.UpdateFrozenState (POS);
		if (!isOffline) {
			map.removeFrozen(POS.R, POS.C, false);
		}
	}

	public void thunderMe(){
		Vector3 pos = transform.position;
		GameObject instance = null;
		instance = Instantiate(Resources.Load("Prefab/FireBall", typeof(GameObject))) as GameObject;
		instance.transform.position = pos;
		instance.transform.SetParent (map.transform);
		instance.GetComponent<FireBall> ().target = transform;
		instance.GetComponent<FireBall> ().callback = b => {
			if(this != null)
				Eat(false);
		};
	}

	void explore(bool isEnemy){
		Vector3 pos = transform.position;
		GameObject instance = null;
		if(isEnemy)
			instance = Instantiate(Resources.Load("Prefab/ItemExploreEnemy", typeof(GameObject))) as GameObject;
		else
			instance = Instantiate(Resources.Load("Prefab/ItemExplore", typeof(GameObject))) as GameObject;
		instance.transform.position = pos;
		instance.transform.SetParent (map.transform);
		instance.GetComponent<PathItem> ().live (GameConfig.item_eat_time);
	}

	IEnumerator Disappear(bool isOffline){
		yield return new WaitForSeconds (0.4f);
		setState(Const.STATE_NORMAL);
		map.RemovePokemon(POS);
		map.updateMap (isOffline);
		if (map.mapState == Map.MapState.eating)
			map.changeState (Map.MapState.playing);
		if (!isOffline) {
			map._isCheckWrong = true;
			map.check_id.Add (id);
		}
	}

	public Sprite GetSprite(int index)
	{
		if (index >= 0 && index < sprites.Count)
			return sprites[index];

		return null;
	}
	public Sprite GetSprite(string spriteName)
	{
		foreach(Sprite sprite in sprites)
		{
			if (sprite!=null && sprite.name == spriteName)
				return sprite;
		}
		return null;
	}

	public void DeleteSprite(string spriteName1, string spriteName2)
	{
		List<Sprite> listDestroy = new List<Sprite> ();
		foreach(Sprite sprite in sprites)
		{
			if (sprite.name.IndexOf (spriteName1) > 0 ||sprite.name.IndexOf (spriteName2) > 0) {
				listDestroy.Add (sprite);
			}
		}
		foreach (Sprite sprite in listDestroy) {
			sprites.Remove (sprite);
			DestroyImmediate (sprite);
		}
		listDestroy = null;
	}
}
