using UnityEngine;
using System.Collections;

public class Item_Slot {
	Vector3 position;
	bool in_use;
	GameObject obj;

	int order_price;
	int buy_price;

	public Item_Slot(Vector3 item_pos)
	{
		this.position = item_pos;
		this.in_use = false;
	}

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

	public bool UseItemSlot(string item_name)
	{
		this.in_use = true;
		
		GameObject obj = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/"+item_name+"_Prefab.prefab", typeof(GameObject));

		switch(item_name)
		{
		case "Portion":
		{
			this.obj = (GameObject)GameObject.Instantiate (obj, this.position, Quaternion.identity);

			this.order_price = 5;
			this.buy_price = 7;

			break;
		}
		default:
		{
			return false;
		}
		}

		return true;
	}
	public bool FreeItemSlot()
	{
		this.in_use = false;
		GameObject.Destroy(this.obj);
		//gold
		return true;
	}
}
