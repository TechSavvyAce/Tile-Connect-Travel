using UnityEngine;
using System.Collections;

public class SoundUI : MonoBehaviour
{
	public GameObject off;
	void Start()
	{
		turn(Save.getSound());
	}

	public void onClick()
	{
		SoundSystem.ins.onClick();
		turn(SoundSystem.isPlaySound);
	}


	void turn(bool isOn)
	{
		SoundSystem.ins.turn(isOn);
		bool isPlaySound = SoundSystem.isPlaySound;
		if (isPlaySound)
		{
			if (off != null)
			{
				off.SetActive(false);
			}
		}
		else
		{
			if (off != null)
			{
				off.SetActive(true);
			}
		}
	}

}
