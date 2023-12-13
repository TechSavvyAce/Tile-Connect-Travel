using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
	Button myButton;
	void Start () {
		myButton = GetComponent<Button>(); // <-- you get access to the button component here
		myButton.onClick.AddListener( () => {myFunctionForOnClickEvent("stringValue", 4.5f);} );  // <-- you assign a method to the button OnClick event here

		if(gameObject.name.Equals("ExitButton")){
			#if UNITY_IOS || UNITY_EDITOR
			gameObject.SetActive(false);
			#endif
		}
		if(gameObject.name.Equals("FacebookButton")){
				gameObject.SetActive(false);
		}
		if(gameObject.name.Equals("InviteFacebookButton")){
				gameObject.SetActive(false);
		}
		if (name.Equals ("FacebookInfo")) {
			gameObject.SetActive (false);
		}
	}

	void myFunctionForOnClickEvent(string argument1, float argument2)
	{
		SoundSystem.ins.playButtonClick ();
	}

	public void loginFacebook(){
	}

	public void facebookPage(){
	}
}
