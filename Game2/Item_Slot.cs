using UnityEngine;
using System.Collections;

public class Item_Slot {
	Vector3 position;
	bool in_use;
	GameObject obj;
	string item_name;

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
		return this.in_use;
	}
	public GameObject GetItemObject()
	{
		return this.obj;
	}
	public string _GetItemName()
	{
		return this.item_name;
	}

	public bool UseItemSlot(string item_name)
	{
		bool result;

		this.in_use = true;
		
		GameObject obj = (GameObject) Resources.Load("Prefabs/Game2/"+item_name+"_Prefab", typeof(GameObject));

		if(obj)
		{
			this.obj = (GameObject)GameObject.Instantiate (obj, this.position, Quaternion.identity);
			this.item_name = item_name;
			result = true;
		}
		else //null
			result = false;

		return result;
	}
	public bool FreeItemSlot()
	{
		this.in_use = false;
		GameObject.Destroy(this.obj);
		//gold
		return true;
	}
}
