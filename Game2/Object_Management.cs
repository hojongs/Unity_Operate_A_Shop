﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_Management {
	static Desk_Slot[] desk_list;
	static int desk_list_length;

	public static bool Object_Management_Init()
	{
		desk_list_length = 3;
		desk_list = new Desk_Slot[desk_list_length];

		for(int i=0;i<desk_list_length;i++)
		{
			desk_list[i] = new Desk_Slot(new Vector3(45*(i-1), 5, 0));
		}

		return true;
	}

	public static Desk_Slot GetDesk(int index)
	{
		return Object_Management.desk_list[index];
	}

	public static int OrderDesk(string desk_name)
	{
		int num = -1;

		for (int i=0;i<desk_list_length;i++)
		{
			if(desk_list[i].GetInUse() == false)
			{
				num = i;
				break;
			}
		}

		if(num == -1)
			return 1;

		if(desk_list[num].UseDeskSlot(desk_name) == false)
			return -1;

		return 0;
	}

	public static int OrderItem(string item_name)
	{
		int result = 2; //there is not available desk
		for (int i=0;i<Object_Management.desk_list_length;i++)
		{
			if(desk_list[i].GetInUse() == true)
			{
				result = desk_list[i]._OrderItem(item_name);
				if(result == 0)
				{
					desk_list[i].IncreaseItemCount();
					//Debug.Log (desk_list[i].GetItemCount());
					break; //success
				}
			}
		}

		return result; //fail
	}

	public static int GetTotalItemCount()
	{
		int result=0;

		for (int i=0;i<desk_list_length;i++)
		{
			result+= desk_list[i].GetItemCount();
		}

		return result;
	}
	public static int[] Select_Item()
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

		
		Item_Slot[] item_list = desk_list[pick].GetItemList();
		List<int> random_list2 = new List<int>();
		for(int j=0;j<item_list.Length;j++)
		{
			if(item_list[j].GetInUse() == true)
				random_list2.Add(j);
		}
		int pick2 = random_list2[Random.Range(0,random_list2.Count)];
		//Debug.Log (random_list2.Count);
		//Debug.Break ();

		return new int[] {pick,pick2};
	}
}
