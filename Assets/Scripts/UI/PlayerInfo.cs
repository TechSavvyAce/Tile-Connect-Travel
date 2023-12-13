using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

	public Text numCoin;
	void Start () {
		// Save.setPlayerCoin(30);
		Debug.Log("PlayerCoin :: " + Save.getPlayerCoin());
		numCoin.text = Save.getPlayerCoin() + "";
	}

	void Update () {
		
	}

	public void updatePlayerCoin(int coin) {
		numCoin.text = coin + "";
	}

	public static void saveCoin(int coin) {
		Debug.Log("saveCoin :: " + coin);
		Save.setPlayerCoin(coin);
		PlayerPrefs.SetInt("client_coin_version", getClientCointVersion() + 1);
		PlayerPrefs.Save();
		GameStatic.playerInfo.updatePlayerCoin(Save.getPlayerCoin());
	}

    public static int getClientCointVersion()
    {
        return PlayerPrefs.GetInt("client_coin_version", 0);
    }
}
