﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupFade : MonoBehaviour {
	public float fadeDelay = 0.0f; 
	public float fadeTime = 0.5f; 
	public bool fadeInOnStart = false; 
	public bool fadeOutOnStart = false;
	private bool logInitialFadeSequence = false; 

	// store colours
	private Color[] colors; 

	// allow automatic fading on the start of the scene
	IEnumerator Start ()
	{
		//yield return null; 
		yield return new WaitForSeconds (fadeDelay); 

		if (fadeInOnStart)
		{
			logInitialFadeSequence = true; 
			FadeIn (); 
		}

		if (fadeOutOnStart)
		{
			FadeOut (fadeTime); 
		}
	}




	// check the alpha value of most opaque object
	float MaxAlpha()
	{
		float maxAlpha = 0.0f; 
		Image[] rendererObjects = GetComponentsInChildren<Image>(); 
		foreach (Image item in rendererObjects)
		{
			maxAlpha = Mathf.Max (maxAlpha, item.color.a); 
		}
		return maxAlpha; 
	}

	// fade sequence
	IEnumerator FadeSequence (float fadingOutTime)
	{
		// log fading direction, then precalculate fading speed as a multiplier
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f / fadingOutTime; 

		// grab all child objects
		Image[] rendererObjects = GetComponentsInChildren<Image>(); 
		if (colors == null)
		{
			//create a cache of colors if necessary
			colors = new Color[rendererObjects.Length]; 

			// store the original colours for all child objects
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				colors[i] = rendererObjects[i].color; 
			}
		}

		// make all objects visible
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].enabled = true;
		}


		// get current max alpha
		float alphaValue = MaxAlpha();  


		// This is a special case for objects that are set to fade in on start. 
		// it will treat them as alpha 0, despite them not being so. 
		if (logInitialFadeSequence && !fadingOut)
		{
			alphaValue = 0.0f; 
			logInitialFadeSequence = false; 
		}

		// iterate to change alpha value 
		while ( (alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) 
		{
			alphaValue += Time.deltaTime * fadingOutSpeed; 

			for (int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = (colors != null ? colors[i] : rendererObjects[i].color);
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f); 				
				rendererObjects[i].color=newColor ; 
			}

			yield return null; 
		}

		// turn objects off after fading out
		if (fadingOut)
		{
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				rendererObjects[i].enabled = false; 
			}
		}
	}


	public void FadeIn ()
	{
		FadeIn (fadeTime); 
	}

	public void FadeOut ()
	{
		FadeOut (fadeTime); 		
	}

	public void FadeIn (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", newFadeTime); 
	}

	public void FadeOut (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", -newFadeTime); 
	}

}
