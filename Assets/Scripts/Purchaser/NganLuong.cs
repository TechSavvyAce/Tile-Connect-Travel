using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NganLuong : MonoBehaviour {
	public static NganLuong ins;

	string id = "";
	string pass = "";
	string url  = "https://www.nganluong.vn/mobile_card.api.post.v2.php";
	// Use this for initialization
	void Start () {
		ins = this;
//		sendCard ("0123456789125", "01234567891","VIETTEL");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sendCard(string cardid,string cardSerial,string type_card="VIETTEL",Action<int> success_cal =null, Action<string> error_cal = null){
		StartCoroutine (_sendCard (cardid,cardSerial,type_card,success_cal,error_cal));
	}

	IEnumerator _sendCard(string cardid,string cardSerial,string type_card="VIETTEL",Action<int> success_cal =null, Action<string> error_cal = null){
		WWWForm form = new WWWForm();
		form.AddField("func", "CardCharge");
		form.AddField("version", "2.0");
		form.AddField("pin_card", cardid);
		form.AddField("card_serial", cardSerial);
		form.AddField("ref_code", SystemInfo.deviceUniqueIdentifier);
		form.AddField("client_fullname", "");
		form.AddField("client_email", "");
		form.AddField("client_mobile", "");
		form.AddField("type_card", type_card);
		form.AddField("merchant_id", id);	
		form.AddField("merchant_account", "nhatnxbk@gmail.com");
		form.AddField("merchant_password", Md5Sum(id +"|" + pass));
		Debug.Log ("meachant id " + id);
		// Upload to a cgi script
		WWW w = new WWW(url, form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error);
			error_cal ("Có lỗi xảy ra. Vui lòng thử lại");
		}
		else {
			print("Res : " + w.text);
			string[] split = w.text.Split ('|');
			string code = split [0];
			string message = "Có lỗi xảy ra. Vui lòng thử lại";
			if(code == "07")
			message = "Thẻ đã được sử dụng.";
			if(code == "08")
			message = "Thẻ bị khoá.";
			if(code == "09")
			message = "Thẻ hết hạn sử dụng.";
			if(code == "10")
			message = "Thẻ chưa được kích hoạt hoặc không tồn tại.";
			if(code == "11")
			message = "Thẻ sai định dạng.";
			if(code == "12")
			message = "Sai số serial của thẻ";
			if(code == "16")
			message = "Số lần thử (nhập sai liên tiếp) của thẻ vượt quá giới hạn cho phép";
			int money = 0;
			if (code == "00") {
				int.TryParse (split [10], out money);
				Debug.Log ("SUCESSS money " + money);
				if(success_cal != null)
				success_cal (money);
			} else {
				Debug.Log ("Error " + message);
				if(error_cal != null)
				error_cal (message);
			}
		}
	}

	public  string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}
