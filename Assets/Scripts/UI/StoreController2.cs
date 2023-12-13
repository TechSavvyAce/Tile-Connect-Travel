using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController2 : MonoBehaviour {
	public GameObject cardGroup;
	public Text numItem1;
	public Text numItem2;
	public Text numItem3;
	public Text costItem1;
	public Text costItem2;
	public Text costItem3;

	public Text rateExchange;

	public GameObject paymentDetail;
	public GameObject iconVinaphone;
	public GameObject iconViettel;
	public GameObject iconMobifone;
	public Text numCode;
	public Text numSeri;
	public bool isShowPaymentDetail = false;
	public enum CARD_TYPE {NONE, VNP, VIETTEL, VMS};
	public CARD_TYPE cardType = CARD_TYPE.NONE;

    public static string cost1 = "0.99$";
	public static string cost2 = "1.99$";
	public static string cost3 = "5.49$";
	void Start () {
		//cardGroup.SetActive (false);
		if (cost1 != "") {
			costItem1.text = cost1;
			costItem2.text = cost2;
			costItem3.text = cost3;
		}
		updatePrice ();
	}

	public void updatePrice(){
		numItem1.text = GameConfig.small_count + "";
		numItem2.text = GameConfig.medium_count + "";
		numItem3.text = GameConfig.big_count + "";
		//rateExchange.text = "10.000d = " + GameConfig.coin_per_10000d;
		//if (GSM.CONFIG != null && GSM.CONFIG.ContainsKey ("card_group_enable") && (bool)GSM.CONFIG.GetBoolean ("card_group_enable")) {
		//	cardGroup.SetActive (true);
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickItem1() {
		Purchaser.ins.BuyProductID (Purchaser.kProductIDSmallPack);
	}

	public void onClickItem2() {
		Purchaser.ins.BuyProductID (Purchaser.kProductIDNormalPack);

	}

	public void onClickItem3() {
		Purchaser.ins.BuyProductID (Purchaser.kProductIDBigPack);

	}

	//public void onClickItemVinaphone() {
	//	if (!isShowPaymentDetail) {
	//		isShowPaymentDetail = true;
	//		cardType = CARD_TYPE.VNP;
	//		paymentDetail.SetActive (true);
	//		iconVinaphone.SetActive (true);
	//		iconViettel.SetActive (false);
	//		iconMobifone.SetActive (false);
	//	}
	//}

	//public void onClickItemViettel() {
	//	if (!isShowPaymentDetail) {
	//		isShowPaymentDetail = true;
	//		cardType = CARD_TYPE.VIETTEL;
	//		paymentDetail.SetActive (true);
	//		iconVinaphone.SetActive (false);
	//		iconViettel.SetActive (true);
	//		iconMobifone.SetActive (false);
	//	}
	//}

	//public void onClickItemMobiPhone() {
	//	if (!isShowPaymentDetail) {
	//		isShowPaymentDetail = true;
	//		cardType = CARD_TYPE.VMS;
	//		paymentDetail.SetActive (true);
	//		iconVinaphone.SetActive (false);
	//		iconViettel.SetActive (false);
	//		iconMobifone.SetActive (true);
	//	}
	//}

	//public void hidePaymentDetail() {
	//	paymentDetail.SetActive (false);
	//	isShowPaymentDetail = false;
	//}

	//public void payment() {
	//	string code = numCode.text;
	//	string seri = numSeri.text;
	//	if (code.Replace (" ", "").Equals ("")) {
	//		ToastManager.showToast ("Mã số thẻ không được trống!");
	//	} else if (seri.Replace (" ", "").Equals ("")) {
	//		ToastManager.showToast ("Seri thẻ không được trống!");
	//	} else {
	//		//call request payment
	//		Loading.showLoading("");
	//		Debug.Log ("Call payment with code :: " + code + " - seri :: " + seri);
	//		NganLuong.ins.sendCard (code, seri, cardType.ToString (), c => {
	//			Debug.Log("Coin " + c);
	//			Save.addPlayerCoin((int)(c * GameConfig.coin_per_10000d/10000));
	//			GameStatic.playerInfo.updatePlayerCoin(Save.getPlayerCoin());
	//			Loading.hideLoading();
	//			ToastManager.showToast("Success");
	//			GameStatic.logBuyCoin ("code " + code + " seri " + seri + " " + cardType.ToString(), (int)(c/1000), Save.getPlayerCoin (),"card_payment");
	//		},mess=>{
	//			Loading.hideLoading();
	//			ToastManager.showToast(mess);
	//		});
	//	}
	//}
}
