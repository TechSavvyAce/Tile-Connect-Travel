using UnityEngine;
using System.Collections;
using System;

public class FireBall : MonoBehaviour {
	public Transform target;
	public Action<bool> callback;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (0, Camera.main.orthographicSize, 0);
		iTween.ValueTo(gameObject, iTween.Hash(
			"from",transform.position,
			"to", target.transform.position,
			"time", GameConfig.item_eat_time,
			"easeType",iTween.EaseType.easeInQuart,
			"onupdatetarget", this.gameObject, 
			"oncomplete","hide",
			"onupdate", "MoveGuiElement"));
	}
	public void hide(){
		Vector3 pos = transform.position;
		GameObject instance = null;
		instance = Instantiate(Resources.Load("Prefab/ItemExploreEnemy", typeof(GameObject))) as GameObject;
		instance.transform.position = pos;
		instance.transform.SetParent (GameStatic.map.transform);
		instance.GetComponent<PathItem> ().live (GameConfig.item_eat_time);
		SoundSystem.ins.playOnlineEat ();
		if (callback != null) callback (false);
		Destroy (gameObject);
	}
	public void MoveGuiElement(Vector3 position){
		transform.position =  position;
	}
}
