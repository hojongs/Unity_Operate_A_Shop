using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_Management {
	static Desk_Slot[] desk_list;
	static int desk_list_length;

	static Dictionary<string, ItemPrice> obj_inform_list;
	//static int item_type_count;

	public static bool Object_Management_Init()
	{
		desk_list_length = 3;
		desk_list = new Desk_Slot[desk_list_length];

		for(int i=0;i<desk_list_length;i++)
		{
			desk_list[i] = new Desk_Slot(new Vector3(45*(i-1), 5, 0));
		}


		obj_inform_list = new Dictionary<string, ItemPrice>();
		//item
		obj_inform_list.Add ("Portion", new ItemPrice(-5,5+2));
		obj_inform_list.Add ("Sword", new ItemPrice(-10,10+4));
		obj_inform_list.Add ("Shield", new ItemPrice(-15,51+6));
		//desk
		obj_inform_list.Add ("Basic_Desk", new ItemPrice(-5,5));



		return true;
	}

	public static Vector3 GetDeskForward(int index)
	{
		Desk_Slot target = desk_list[index];
		Vector3 result = target.GetPosition() + target._GetDeskForward();
		result.y = 9;
		return result;
	}

	public static int OrderDesk(string desk_name)
	{
		int result;
		int num = -1;
		int order_price = Object_Management.obj_inform_list[desk_name].GetOrderPrice();
		
		if(Gold.GetGold() + order_price < 0)
		{
			result = 3;
			return result;
		}

		for (int i=0;i<desk_list_length;i++)
		{
			if(desk_list[i].GetInUse() == false)
			{
				num = i;
				break;
			}
		}

		if(num == -1)
			result = 1;

		if(desk_list[num].UseDeskSlot(desk_name) == false)
			result = -1;
		else //success
		{
			Gold.AddGold(order_price);
			result = 0;
		}

		return result;
	}

	public static int OrderItem(string item_name)
	{
		int result; 
		int order_price = Object_Management.obj_inform_list[item_name].GetOrderPrice();

		if(Gold.GetGold() + order_price < 0)
		{
			result = 3;
			return result;
		}

		result = 2; //there is not available desk

		for (int i=0;i<Object_Management.desk_list_length;i++)
		{
			if(desk_list[i].GetInUse() == true)
			{
				result = desk_list[i]._OrderItem(item_name);
				if(result == 0)
				{
					Gold.AddGold(order_price);
					//Debug.Log (desk_list[i].GetItemCount());
					break; //success
				}
			}
		}

		return result;
	}

	public static int GetTotalItemCount()
	{
		int result=0;

		for (int i=0;i<desk_list_length;i++)
		{
			if(desk_list[i].GetInUse() == true)
				result+= desk_list[i].GetItemCount();
			//Debug.Log (i + " : " + desk_list[i].GetItemCount());
		}

		return result;
	}
	public static Dictionary<string,int> Select_Item()
	{
		List<int> random_list = new List<int>();
		for(int i=0;i<desk_list_length;i++)
		{
			if(desk_list[i].GetItemCount() > 0)
			{
				random_list.Add(i);
			}
		}
		int pick = random_list[Random.Range(0,random_list.Count)];
		//Debug.Log (random_list.Count);

		Dictionary<string,int> result = new Dictionary<string, int>();
		result.Add("desk", pick);

		int pick2 = desk_list[pick]._Select_Item();

		result.Add ("item", pick2);

		return result;
	}

	public static int BuyItem(Dictionary<string,int> index)
	{
		int desk_index = index["desk"];
		int result;

		if(desk_list[desk_index].GetInUse() == true)
		{
			if(desk_list[desk_index]._BuyItem(index["item"]) == true)
			{
				string item_name = desk_list[desk_index].GetItemName(index["item"]);
				int buy_price = Object_Management.obj_inform_list[item_name].GetBuyPrice();
				Gold.AddGold(buy_price);
				result = 0;
			}
			else
				result = 1; //Sold out
		}
		else
			result = 2; //There is not the desk

		return result;
	}

	public static bool _Check_Item(Dictionary<string,int> index)
	{
		int desk_index = index["desk"];
		bool result;
		
		if(desk_list[desk_index].GetInUse() == true)
			result = desk_list[desk_index].__Check_Item(index["item"]);
		else
			result = false; //there is not the desk

		return result;
	}
}
