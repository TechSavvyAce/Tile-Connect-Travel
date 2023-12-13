using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour {

	public ItemShopBase itemShopBase;
	public SpriteResource spriteResource;
	public StoreController storeController;

	public Image icon;
	public Image coinImage;
	public Text itemNumber;
	public Text itemDescription;
	public Text itemCost;

	public TimeCountDown timeCountDown;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setInfo(ItemShopBase itemShopBase) {
		this.itemShopBase = itemShopBase;
		if (icon != null) icon.sprite = getSprite (itemShopBase.item_id);
		if (itemNumber != null) itemNumber.text = "x" + itemShopBase.number;
		if (itemCost != null) itemCost.text = StringUtils.getNumberDot(itemShopBase.cost) + " " + itemShopBase.getCurrency();
		if (itemDescription != null) itemDescription.text = itemShopBase.description;
		if (itemShopBase.item_id == Const.COIN_PACK_SMALL) {
		}
		if (itemShopBase.item_id == Const.COIN_PACK_LARGER) {
		}
		if (itemShopBase.item_id == Const.COIN_PACK_BIG) {
		}
		if (itemShopBase.item_type == Const.TYPE_ITEM_REMOVE_ADS) {
			if (!Save.canShowAds ()) {
				if (Save.getRemoveAdsPackId () == itemShopBase.pack_id) {
					timeCountDown.StartRun (Save.getTimeRemoveAdsRemain (), null);
					coinImage.enabled = false;
				}
			}
		}
	}

	public void onClick() {
		if (itemShopBase.item_type == Const.TYPE_ITEM_IN_GAME) {
			onClickItemInGame ();
		} else if (itemShopBase.item_type == Const.TYPE_ITEM_UI) {
			onClickItemUI ();
		} else if (itemShopBase.item_type == Const.TYPE_ITEM_REMOVE_ADS) {
			onClickToItemRemoveAds ();
		} else if (itemShopBase.item_type == Const.TYPE_ITEM_COIN) {
				ToastManager.showToast (StringUtils.loading);
			} else {
				if (itemShopBase.item_id == Const.COIN_PACK_SMALL) {
				}
				if (itemShopBase.item_id == Const.COIN_PACK_LARGER) {
				}
				if (itemShopBase.item_id == Const.COIN_PACK_BIG) {
				}
			}
		}

	private void onClickItemInGame() {
		int playerCoin = Save.getPlayerCoin ();
		if (playerCoin < itemShopBase.cost) {
			ToastManager.showToast (StringUtils.message_not_enough_coin);
		} else {
			Save.setPlayerCoin (playerCoin - itemShopBase.cost);
			if (itemShopBase.item_id == Const.REWARD_HINT) {
				ItemController.addHintItem (itemShopBase.number);
			} else if (itemShopBase.item_id == Const.REWARD_RANDOM) {
				ItemController.addRandomItem (itemShopBase.number);
			} else if (itemShopBase.item_id == Const.REWARD_ENERGY) {
				ItemController.addEnergyItem (itemShopBase.number);
			}
			//GSM.logPurchaser(itemShopBase.pack_id);
			//storeController.updateUserInfo ();
			ToastManager.showToast (StringUtils.success);
		}
	}

	private void onClickItemUI() {
			int playerCoin = Save.getPlayerCoin ();
			if (playerCoin < itemShopBase.cost) {
				ToastManager.showToast (StringUtils.message_not_enough_coin);
			} else {
				Save.setPlayerCoin (playerCoin - itemShopBase.cost);
				Save.savePackInfo (itemShopBase.item_id);
				UIPackItemController uiItemPackController = GetComponent<UIPackItemController> ();

				ToastManager.showToast (StringUtils.success);
			}
	}

	private void onClickToItemRemoveAds() {
		if (!Save.canShowAds ()) {
			ToastManager.showToast ("Item remove ads has been bought");
			return;
		}
		int playerCoin = Save.getPlayerCoin ();
		if (playerCoin < itemShopBase.cost) {
			ToastManager.showToast (StringUtils.message_not_enough_coin);
		} else {
			Save.setPlayerCoin (playerCoin - itemShopBase.cost);
			Save.buyRemoveAdsPack (itemShopBase.pack_id, itemShopBase.number);

			ToastManager.showToast (StringUtils.success);
			timeCountDown.StartRun (Save.getTimeRemoveAdsRemain (), null);
			coinImage.enabled = false;
		}
	}

	private void onClickItemCoin() {
	
	}

	private Sprite getSprite(int item_id) {
		if (item_id == Const.REWARD_HINT) {
			return spriteResource.item_hint;
		} else if (item_id == Const.REWARD_RANDOM) {
			return spriteResource.item_random;
		} else if (item_id == Const.REWARD_ENERGY) {
			return spriteResource.item_energy;
		} else if (item_id == Const.UI_DEFAULT_POKEMON) {
			return spriteResource.ui_default_pokemon;
		} else if (item_id == Const.UI_DEFAULT_KAWAI) {
			return spriteResource.ui_default_fruit;
		} else if (item_id == Const.UI_DEFAULT_FAKE) {
			return spriteResource.ui_default_monster;
		} else if (item_id == Const.COIN_PACK_SMALL) {
			return spriteResource.coin_icon_small;
		} else if (item_id == Const.COIN_PACK_LARGER) {
			return spriteResource.coin_icon_normal;
		} else if (item_id == Const.COIN_PACK_BIG) {
			return spriteResource.coin_icon_big;
		} else if (item_id == Const.COIN_PACK_FREE) {
			return spriteResource.coin_icon;
		} else if (item_id == Const.ITEM_REMOVE_ADS) {
			return spriteResource.icon_remove_ads;
		}
		return spriteResource.item_hint;
	}
}
