using UnityEngine;
using System.Collections;

public class Item_Slot {
	Vector3 position;
	bool in_use;
	GameObject obj;

	int order_price;
	int buy_price;

	public Vector3 GetPosition()
	{
		return this.position;
	}
	public bool GetInUse()
	{
		return in_use;
	}
	public GameObject GetItemObject()
	{
		return this.obj;
	}
	public int GetOrderPrice()
	{
		return this.order_price;
	}
	public int GetBuyPrice()
	{
		return this.buy_price;
	}


}
