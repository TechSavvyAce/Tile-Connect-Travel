using UnityEngine;
using System.Collections;

public class PathSystem : MonoBehaviour {
	public float scaleByItem = 2.1f;
	public float timeLive = GameConfig.item_eat_time;
	enum PathType{short_v, short_h, long_v, long_h, left_down, left_up, right_down, right_up};
	public GameObject map;
	public Map mapCS;
	public GameObject[] listObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			ArrayList list = new ArrayList();
			// list.AddRange(GameObject.FindGameObjectsWithTag("item"));
			// list = list.GetRange(1,20);
			foreach(GameObject obj in listObj){
				list.Add(obj.GetComponent<Pokemon>().POS);
			}
			draw(list,false);
		}
	}

	private void drawPath(Vector3 pos,PathType type){
		GameObject instance = null;
		switch (type) {
			case PathType.long_h:
			instance = Instantiate(Resources.Load("Prefab/long_line", typeof(GameObject))) as GameObject;
			break;
			case PathType.long_v:
			instance = Instantiate(Resources.Load("Prefab/long_line", typeof(GameObject))) as GameObject;
			instance.transform.localRotation =  Quaternion.Euler(0, 0, 90);
			break;
			case PathType.left_down:
			instance = Instantiate(Resources.Load("Prefab/line_angle_left_down", typeof(GameObject))) as GameObject;
			break;
			case PathType.left_up:
			instance = Instantiate(Resources.Load("Prefab/line_angle_left_up", typeof(GameObject))) as GameObject;
			break;
			case PathType.right_up:
			instance = Instantiate(Resources.Load("Prefab/line_angle_right_up", typeof(GameObject))) as GameObject;
			break;
			case PathType.right_down:
			instance = Instantiate(Resources.Load("Prefab/line_angle_right_down", typeof(GameObject))) as GameObject;
			break;
			case PathType.short_h:
			instance = Instantiate(Resources.Load("Prefab/short_line", typeof(GameObject))) as GameObject;
			break;
			case PathType.short_v:
			instance = Instantiate(Resources.Load("Prefab/short_line", typeof(GameObject))) as GameObject;
			instance.transform.localRotation =  Quaternion.Euler(0, 0, 90);
			break;
		}
		instance.GetComponent<PathItem> ().live (timeLive);
		instance.transform.position = pos;
		instance.transform.localScale *= scaleByItem;
		instance.transform.SetParent (map.transform);
	}

	public void draw(ArrayList list, bool isEnemy){
		if(list.Count > 2){
			for (int i = 1; i < list.Count - 1; i++) {
				Vec2 preObj = (Vec2) list[i - 1];
				Vec2 curObj = (Vec2) list[i];
				Vec2 nextObj = (Vec2) list[i + 1];
				if(curObj.R == preObj.R 
					&& curObj.R == nextObj.R ){
					drawPath(mapCS.POS[curObj.R][curObj.C], PathType.long_h);
				}
				if(curObj.C == preObj.C 
					&& curObj.C == nextObj.C ){
					drawPath(mapCS.POS[curObj.R][curObj.C], PathType.long_v);
				}
				if(curObj.C == getSameRow(curObj,nextObj,preObj).C +1
					&& curObj.R == getSameCol(curObj,nextObj,preObj).R + 1){
					drawPath(mapCS.POS[curObj.R][curObj.C], PathType.left_down);
				}
				if(curObj.C == getSameRow(curObj,nextObj,preObj).C +1
					&& curObj.R == getSameCol(curObj,nextObj,preObj).R - 1){
					drawPath(mapCS.POS[curObj.R][curObj.C], PathType.left_up);
				}
				if(curObj.C == getSameRow(curObj,nextObj,preObj).C -1
					&& curObj.R == getSameCol(curObj,nextObj,preObj).R - 1){
					drawPath(mapCS.POS[curObj.R][curObj.C], PathType.right_up);
				}
				if(curObj.C == getSameRow(curObj,nextObj,preObj).C -1
					&& curObj.R == getSameCol(curObj,nextObj,preObj).R + 1){
					drawPath(mapCS.POS[curObj.R][curObj.C], PathType.right_down);
				}
			}
		}else if(list.Count == 2){
			Vec2 preObj = (Vec2) list[0];
			Vec2 curObj = (Vec2) list[1];
			if(curObj.R == preObj.R){
				drawPath(mapCS.POS[curObj.R][curObj.C] / 2  + mapCS.POS[preObj.R][preObj.C] / 2, PathType.short_h);
			}
			if(curObj.C == preObj.C){
				drawPath(mapCS.POS[curObj.R][curObj.C] / 2  + mapCS.POS[preObj.R][preObj.C] / 2, PathType.short_v);
			}
		}
		Vec2 first = (Vec2) list[0];
		Vec2 last = (Vec2) list[list.Count - 1];
		drawExplore (mapCS.POS [first.R] [first.C],isEnemy);
		drawExplore (mapCS.POS [last.R] [last.C],isEnemy);
	}

	public Vec2 getSameRow(Vec2 cur, Vec2 v1, Vec2 v2){
		if (cur.R == v1.R)
		return v1;
		if (cur.R == v2.R)
		return v2;
		return new Vec2(-1000,-1000);
	}

	public Vec2 getSameCol(Vec2 cur, Vec2 v1, Vec2 v2){
		if (cur.C == v1.C)
		return v1;
		if (cur.C == v2.C)
		return v2;
		return new Vec2(-1000,-1000);
	}

	void drawExplore(Vector3 pos,bool isEnemy){
		GameObject instance = null;
		if(isEnemy)
			instance = Instantiate(Resources.Load("Prefab/ItemExploreEnemy", typeof(GameObject))) as GameObject;
		else
			instance = Instantiate(Resources.Load("Prefab/ItemExplore", typeof(GameObject))) as GameObject;
		instance.transform.position = pos;
		instance.transform.SetParent (map.transform);
		instance.GetComponent<PathItem> ().live (timeLive);
	}
}
