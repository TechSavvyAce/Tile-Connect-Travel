using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public bool isGame = true;
	// Use this for initialization
	void Start () {
		float camHalfHeight = Camera.main.orthographicSize*2;
		float camHalfWidth = Camera.main.aspect * camHalfHeight;
//		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Sprite sprite;
		if (isGame) {
			// sprite = Resources.Load ("Bg/" + ((ListLevel.CurrentLevel / 5) + 1), typeof(Sprite)) as Sprite;
			sprite = Resources.Load ("Bg/" + Random.Range (1, 15), typeof(Sprite)) as Sprite;
		} else {
			sprite = GetComponent<SpriteRenderer> ().sprite;
		}
			
		float width = sprite.bounds.size.x;
		float height = sprite.bounds.size.y;
		float perX = camHalfWidth / width;
		float perY = camHalfHeight / height;
		float per =1;
		if (perX > perY) {
			per = perX;
		} else
			per = perY;
		
		Vector3 localScale = new Vector3(per,per, 1);
		transform.localScale = localScale;
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	public void download(string url, string cache =""){
		Sprite spriteOld = GetComponent<SpriteRenderer> ().sprite;
		//ImageDownloader.LoadImgFromURL (url, delegate(Texture pictureTexture) {
		//	if (pictureTexture != null && spriteOld!= null) {
		//		Texture2D old = (Texture2D)pictureTexture;
		//		Texture2D left = new Texture2D ((int)(old.width), old.height, old.format, false);
		//		Color[] colors = old.GetPixels (0, 0, (int)(old.width), old.height);
		//		left.SetPixels (colors);
		//		left.Apply ();
		//		Sprite sprite = Sprite.Create (left,
		//			new Rect (0, 0, left.width, left.height),
		//			new Vector2 (0.5f, 0.5f),
		//			40);

		//		float camHalfHeight = Camera.main.orthographicSize*2;
		//		float camHalfWidth = Camera.main.aspect * camHalfHeight;
		//		float width = sprite.bounds.size.x;
		//		float height = sprite.bounds.size.y;
		//		float perX = camHalfWidth / width;
		//		float perY = camHalfHeight / height;
		//		float per =1;
		//		if (perX > perY) {
		//			per = perX;
		//		} else
		//			per = perY;

		//		Vector3 localScale = new Vector3(per,per, 1);
		//		transform.localScale = localScale;
		//		GetComponent<SpriteRenderer> ().sprite = sprite;
		//	}
		//});
	}
}
