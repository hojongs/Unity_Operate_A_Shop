using UnityEngine;
using System.Collections;

public class Desk_Slot {
	Vector3 position;
	bool in_use;
	GameObject obj;

	int item_max;
	Item_Slot[] item_list;

	public Desk_Slot(Vector3 pos)
	{
		this.position = pos;
		this.in_use = false;
	}

	//Get Methods
	public Vector3 GetPosition()
	{
		return this.position;
	}
	public bool GetInUse()
	{
		return this.in_use;
	}
	public GameObject GetDeskObject()
	{
		return this.obj;
	}
	public Item_Slot[] GetItemList()
	{
		return this.item_list;
	}

	public bool UseDeskSlot(string desk_name)
	{
		this.in_use = true;

		GameObject obj = (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/"+desk_name+"_Prefab.prefab", typeof(GameObject));

		switch(desk_name)
		{
		case "Basic_Desk":
		{
			this.obj = (GameObject)GameObject.Instantiate (obj, this.position, Quaternion.identity);

			this.item_max = 6;
			this.item_list = new Item_Slot[item_max];
			
			break;
		}
		default:
		{
			return false;
		}
		}


		return true;
	}
	public bool FreeDeskSlot()
	{
		return true;
	}

	public bool OrderItem()
	{
		int num = -1;

		for (int i=0;i<this.item_max;i++)
		{
			if(item_list[i].GetInUse() == false)
			{
				num = i;
				break;
			}
		}

		return true;
	}
	public bool BuyItem()
	{
		return true;
	}
}
