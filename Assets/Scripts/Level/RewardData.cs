using UnityEngine;
using System.Collections;
using SimpleJSON;
public class RewardData {
	public int type;
	public int number;
	public int probability0;
	public int probability1;
	public int probability2;
	public int probability3;
	public RewardData(int type, int number, int probability0, int probability1, int probability2, int probability3) {
		this.type = type;
		this.number = number;
		this.probability0 = probability0;
		this.probability1 = probability1;
		this.probability2 = probability2;
		this.probability3 = probability3;
	}

	public RewardData (JSONClass data) {
		this.type = data [StringUtils.type].AsInt;
		this.number = data [StringUtils.number].AsInt;
		this.probability0 = data [StringUtils.probability + "0"].AsInt;
		this.probability1 = data [StringUtils.probability + "1"].AsInt;
		this.probability2 = data [StringUtils.probability + "2"].AsInt;
		this.probability3 = data [StringUtils.probability + "3"].AsInt;
	}

	public int getReward(int star) {
        //Debug.Log("+++++++++++++++++++++++");
        //Debug.Log("type = " + type);
		int reward = 0;
        int probability = Random.Range(0, 60); //Random.Range (0, 100);
        switch (star) {
		case 0:
			if (probability < probability0) {
				reward = number;
			}
			break;
		case 1:
			if (probability < probability1) {
				reward = number;
			}
			break;
		case 2:
			if (probability < probability2) {
				reward = number;
			}
			break;
		case 3:
			if (probability < probability3) {
				reward = number;
			}
			break;
		}
        //Debug.Log("reward = " + reward);
        return reward;
	}
}
