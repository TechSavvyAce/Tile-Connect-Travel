using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour {
	public static SoundSystem ins;
	public static bool isPlaySound = true;
	public AudioSource audioSouceSound;
	public AudioSource audioSouceMusic;
	public AudioClip eat;
	public AudioClip onlineEat;
	public AudioClip select;
	public AudioClip deSelect;
	public AudioClip sound_gameover;
	public AudioClip sound_gamewin;
	public AudioClip sound_online_win;
	public AudioClip sound_button_click;
	public AudioClip sound_shuffle;
	public AudioClip sound_cant_move;
	public AudioClip sound_rank_up;
	public AudioClip sound_online_start;
	public AudioClip sound_ice_break;
	public AudioClip sound_fall_down;
	public AudioClip sound_ice_add;

	public AudioClip music_back;
	public AudioClip music_back1;
	public AudioClip music_back2;
	public AudioClip music_war;
	public AudioClip music_off;
	public AudioClip time_tick;
	public AudioClip[] sound_eat_list;

	int indexSoundEat = 0;
	void Awake () {
		if(ins == null)
		ins = this;
		play_music_back ();
		audioSouceMusic.loop = true;
		turn(Save.getSound());
	}

	public void turn(bool isOn){
		isPlaySound = isOn;
		Save.turnSound (isOn);
		if (isPlaySound) {
			audioSouceMusic.volume = 0.75f;
			audioSouceSound.volume = 1;
		} else {
			audioSouceMusic.volume = 0;
			audioSouceSound.volume = 0;
		}
	}

	public void onClick(){
		isPlaySound = !isPlaySound;
	}

	public void playEat(){
		audioSouceSound.PlayOneShot (eat);
		audioSouceSound.PlayOneShot (sound_eat_list[(indexSoundEat++) % sound_eat_list.Length]);
	}

	public void playOnlineEat(){
		audioSouceSound.PlayOneShot (onlineEat);
	}
	public void playSelect(){
		audioSouceSound.PlayOneShot (select);
	}
	public void playDeSelect(){
		audioSouceSound.PlayOneShot (deSelect);
	}

	public void playButtonClick(){
		audioSouceSound.PlayOneShot (sound_button_click);
	}
	
	public void play_sound_online_win(){
		audioSouceSound.PlayOneShot (sound_online_win);
	}

	public void play_sound_shuffle(){
		audioSouceSound.PlayOneShot (sound_shuffle);
	}

	public void play_online_start(){
		audioSouceSound.PlayOneShot (sound_online_start);
	}

	public void play_sound_cant_move(){
		audioSouceSound.PlayOneShot (sound_cant_move);
	}

	public void play_sound_rank_up(){
		audioSouceSound.PlayOneShot (sound_rank_up);
	}

	public void play_sound_ice_break(){
		audioSouceSound.PlayOneShot (sound_ice_break);
	}

	public void play_sound_fall_down(){
		audioSouceSound.PlayOneShot (sound_fall_down);
	}

	public void play_sound_ice_add(){
		audioSouceSound.PlayOneShot (sound_ice_add);
	}

	public void play_music_back(){
		audioSouceMusic.Stop();
		int level = GameStatic.currentLevel;
		if(level%3 == 0)
			audioSouceMusic.clip = music_back;
		if(level%3 == 1)
			audioSouceMusic.clip = music_back1;
		if(level%3 == 2)
			audioSouceMusic.clip = music_back2;

		audioSouceMusic.Play();
	}

	public void play_music_war(){
		audioSouceMusic.Stop();
		audioSouceMusic.clip = music_war;
		audioSouceMusic.Play();
	}

	public void play_music_off(){
		audioSouceMusic.Stop();
		audioSouceMusic.clip = music_off;
		audioSouceMusic.Play();
	}

	public void play_time_over_in_1_min(){
		audioSouceMusic.Stop();
		audioSouceMusic.clip = time_tick;
		audioSouceMusic.Play();
	}

	public void playSoundGameOver() {
		audioSouceSound.PlayOneShot(sound_gameover);
	}
	public void playSoundGameWin() {
		audioSouceSound.PlayOneShot(sound_gamewin);
	}
	public void stop() {
		audioSouceSound.Stop();
		audioSouceMusic.Stop();
	}
	public void pause() {
		audioSouceSound.Pause();
		audioSouceMusic.Pause();
	}
	public void play() {
		audioSouceSound.UnPause();
		audioSouceMusic.UnPause();
	}
}
