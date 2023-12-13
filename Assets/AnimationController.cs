using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	public bool playOnEnable = false;
	public Animation anim;
	void OnEnable(){
		if (anim == null)
			anim = GetComponent<Animation> ();
		if (playOnEnable) {
			anim.Play ();
		}
	}
	public void onEnd(){
	}
}
