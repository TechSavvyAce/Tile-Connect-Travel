using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PackController : MonoBehaviour {
	public enum PackType {big,normal,small};
	Text txtValue;
	Text txtInfo;
	Text txtName;

	Text txtHint;
	Text txtRandom;
	Text txtEnergy;


	void Start () {
		txtInfo = transform.Find ("Detail").GetComponent<Text>();
		txtName = transform.Find ("Name").GetComponent<Text>();
		txtValue = transform.Find ("Buy").Find ("TextValue").GetComponent<Text>();
		txtHint = transform.Find("Rewards").Find ("Hint").Find ("Text").GetComponent<Text>();
		txtEnergy = transform.Find("Rewards").Find ("Energy").Find ("Text").GetComponent<Text>();
		txtRandom = transform.Find("Rewards").Find ("Random ").Find ("Text").GetComponent<Text>();
	}
	public void updateInfo(string name, string detail,string value,PackType type){
		if (txtInfo == null)
		Start ();
		txtValue.text = value;
		txtInfo.text = "";
		txtName.text = name;
		if (type == PackType.big) {
			txtHint.text = "+" + GameConfig.num_hint_big;
			txtRandom.text = "+" + GameConfig.num_random_big;
			txtEnergy.text = "+" + GameConfig.num_energy_big;
		}
		if (type == PackType.normal) {
			txtHint.text = "+" + GameConfig.num_hint_normal;
			txtRandom.text = "+" + GameConfig.num_random_normal;
			txtEnergy.text = "+" + GameConfig.num_energy_normal;
		}
		if (type == PackType.small) {
			txtHint.text = "+" + GameConfig.num_hint_lite;
			txtRandom.text = "+" + GameConfig.num_random_lite;
			txtEnergy.text = "+" + GameConfig.num_energy_lite;
		}
	}
}
