using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;


public class UIPackItemController : MonoBehaviour
{
    public Image ava;
    public Text status;
    public Text coin;
    public Text btn_buy_text;
    public GameObject btn_preview;
    public Button btn_coin;
    public Button btn_use;
    public ItemShopBase data;
    public ItemShop itemShop;
    public Image back;

    public List<WWW> listWWW = new List<WWW>();

    public bool isDefault = false;

    public static Text lastText;
    public static Button lastButtonUse;
    public static Image lastBack;

    public bool isTutorial = false;
    // Use this for initialization
    void Start()
    {
        if (data.item_id <= Const.UI_DEFAULT_FAKE)
        {
            isDefault = true;
        }

        //if (!isDefault) {
        //	ImageDownloader.LoadImgFromURL (data.data.GetString (GSM.url), delegate(Texture pictureTexture) {
        //		if (pictureTexture != null && !ava.IsDestroyed ()) {
        //			Texture2D old = (Texture2D)pictureTexture;
        //			Texture2D left = new Texture2D ((int)(old.width), old.height, old.format, false);
        //			Color[] colors = old.GetPixels (0, 0, (int)(old.width), old.height);
        //			left.SetPixels (colors);
        //			left.Apply ();
        //			Sprite sprite = Sprite.Create (left,
        //				               new Rect (0, 0, left.width, left.height),
        //				               new Vector2 (0.5f, 0.5f),
        //				               40);
        //			ava.sprite = sprite;
        //		}
        //	});
        //}

    }

    bool isDownload()
    {
        // if (data.item_id == 103) return false;
        return false;

    }

    public bool isInUsed()
    {
        int pack = data.item_id;
        return Save.getUsedPack() == pack;
    }


    public void changeToActive()
    {
        if (lastText != null && lastText.text.Equals(StringUtils.actived))
        {
            lastText.text = StringUtils.active;
            lastButtonUse.interactable = true;
            // lastBack.color = new Color (100f / 255, 100f / 255, 100f / 255, 100f / 255);
            lastBack.color = Color.white;
        }
        lastText = coin;
        lastButtonUse = btn_coin;
        lastBack = back;
        btn_coin.interactable = false;
        coin.text = StringUtils.actived;
        back.color = Color.white;
    }


    public void review()
    {
        UIPackController.staticViewListItem.SetActive(true);
        //		UIPackController.staticViewListItem.GetComponentsInChildren<Text> ()[0].text = StringUtils.loading;
    }

}
