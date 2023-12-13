using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PoolGameObject{
	public List<GameObject> listAvai;
	public List<GameObject> listInUse;
	public PoolGameObject(){
		listAvai = new List<GameObject> ();
		listInUse = new List<GameObject> ();
	}

	public void cacheNew(GameObject g){
		listInUse.Add(g);
	}

	public GameObject getInCache(){
		while(listAvai.Count > 0) {
			GameObject g = listAvai [0];
			listAvai.RemoveAt (0);
			if (g == null) {
				continue;
			}
			listInUse.Add (g);
			g.SetActive (true);
			return g;
		}
		return null;
	}

	public void backToCache(GameObject g){
		listInUse.Remove (g);
		listAvai.Add (g);
	}

	public void deleteAllCurrent(){
		foreach (GameObject g in listInUse) {
			g.SetActive (false);
		}

		listAvai.AddRange (listInUse);
		listInUse.Clear ();
	}
}
