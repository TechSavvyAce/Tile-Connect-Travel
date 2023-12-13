using UnityEngine;
using System.Collections;

public class ItemShopBase {

	public int item_id;
	public int pack_id;
	public int item_type;
	public int number;
	public int cost;
	public int cost_type;
	public string description;

	public ItemShopBase(int pack_id, int item_id, int item_type, int number, int cost, int cost_type, string description) {
		this.item_id     = item_id;
		this.pack_id     = pack_id;
		this.item_type   = item_type;
		this.number      = number;
		this.cost        = cost;
		this.cost_type   = cost_type;
		this.description = description;
	}

	public string getCurrency() {
		if (item_type == Const.TYPE_ITEM_COIN) {
			return "VND";
		}
		return "";
	}
}
