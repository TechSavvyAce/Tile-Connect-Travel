using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayOffUI : MonoBehaviour {
	public void BackToLevelSelect(){
		LoadingScene.loadingScreen.loadLevelMode ();
		SoundSystem.ins.play_music_back ();
	}
	public void BackToMainMenu(){
		LoadingScene.loadingScreen.loadMain ();
		SoundSystem.ins.play_music_back ();
	}

	public void saveGame() {
		GameStatic.saveGame();
	}

	public void saveGameWithoutMap() {
		GameStatic.saveGameWithoutMap();
	}
}
