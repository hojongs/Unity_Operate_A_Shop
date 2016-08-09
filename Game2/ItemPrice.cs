using UnityEngine;
using System.Collections;

public class ItemPrice {
	int order_price;
	int buy_price;

	public ItemPrice(int order_price, int buy_price)
	{
		this.order_price = order_price;
		this.buy_price = buy_price;
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
