using UnityEngine;
using System.Collections;

public class CommonFunction {
	public static string getTimeFromSecond(int time){
		string timeTxt = "";
		int day = (int)(time / 86400);
		int hour = (int)(time / 3600 - day*24);
		int min = (int)((time - hour * 3600) / 60 - day * 1440);
		int second = time - day * 86400 - hour * 3600 - min * 60;

		if (day > 0) {
			timeTxt += day + "d ";
		}
		if (hour > 0) {
			timeTxt += hour + "h ";
		}
		if (min > 0) {
			timeTxt += min + "m ";
		}
		timeTxt += second + "sec";
		return timeTxt;
	}

	public static string getTimeMessageAgo(int time){
		string timeTxt = "";
		int day = (int)(time / 86400);
		int hour = (int)(time / 3600 - day*24);
		int min = (int)((time - hour * 3600) / 60 - day * 1440);
		int second = time - day * 86400 - hour * 3600 - min * 60;

		if (day > 0) {
			timeTxt += day + "d ";
		}
		if (hour > 0) {
			timeTxt += hour + "h ";
		}
		if (min > 0) {
			timeTxt += min + "m ";
		}
		timeTxt += second + "sec ago";
		return timeTxt;
	}
}
