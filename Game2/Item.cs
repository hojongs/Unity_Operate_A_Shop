using UnityEngine;
using System.Collections;

public class Item {
	Type type;
	Vector3 desk_pos;
	Vector3 item_pos; //on_desk
	GameObject obj;

	public Type GetType()
	{
		return type;
	}
	public bool SetType(Type type)
	{
		this.type = type;
		return true;
	}
	public Vector3 GetDeskPos()
	{
		return desk_pos;
	}
	public bool SetDeskPos(Vector3 desk_pos)
	{
		this.desk_pos = desk_pos;
		return true;
	}
	public Vector3 GetItemPos()
	{
		return item_pos;
	}
	public bool SetItemPos(Vector3 item_pos)
	{
		this.item_pos = item_pos;
		return true;
	}
	public bool GetObject()
	{
		return obj;
	}
	public bool SetObject(GameObject obj)
	{
		this.obj = obj;
		return true;
	}
}

public class Type
{
	int order_price;
	int sell_price;

	public int GetOrderPrice()
	{
		return order_price;
	}
	public bool SetOrderPrice(int order_price)
	{
		this.order_price = order_price;
		return true;
	}
	public int GetSellPrice()
	{
		return this.sell_price;
	}
	public bool SetSellPrice(int sell_price)
	{
		this.sell_price = sell_price;
		return true;
	}
}
