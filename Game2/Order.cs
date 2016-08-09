using UnityEngine;
using System.Collections;

public struct Desk_Space
{
	public Vector3 vector;
	public bool in_use; //need to buy a desk
	public GameObject desk_obj;
	public GameObject[] item_obj;
	public bool[] item_space_in_use;
};

public class BuyList
{
	GameObject item;
	int desk_index;
	int item_index;

	public BuyList() {}
	public BuyList(GameObject item, int desk_index, int item_index)
	{
		this.item = item;
		this.desk_index = desk_index;
		this.item_index = item_index;
	}

	public GameObject GetItem()
	{
		return this.item;
	}
	public int GetDeskIndex()
	{
		return this.desk_index;
	}
	public int GetItemIndex()
	{
		return this.item_index;
	}
}

public class Order{

	//static GameObject portion = ;

	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
	
	//}

	//static Vector3[] Desk_Space;
	static int Desk_MAX = 3;
	static Desk_Space[] desk_space;

	static int DeskItem_MAX = 6;

	static Vector3[] ItemList;
	static int[] instant_index = new int[2];

	static int item_count = 0;

	static int temp;

	static int[] price;

	public static bool Order_Init()
	{
		//desk
		Order.desk_space = new Desk_Space[Order.Desk_MAX];

		for(int i=0;i<Desk_MAX;i++)
		{
			Order.desk_space[i].vector = new Vector3 (5*(i-1), 0.5f, 0);
			Order.desk_space[i].in_use = false;
			Order.desk_space[i].item_obj = new GameObject[Order.DeskItem_MAX];
			Order.desk_space[i].item_space_in_use = new bool[Order.DeskItem_MAX];
			for(int j=0;j<Order.DeskItem_MAX;j++)
			{
				//Order.desk_space[i].item_obj[j]
				Order.desk_space[i].item_space_in_use[j] = false;
			}
		}

		Order.price = new int[1];
		price[0] = 5;

		return true;
	}

	public static bool meth_Item_Order(string item_name)
	{
		Vector3 pos = Order.GetAvailableItemSpace();
		
		if (pos == Vector3.zero)
		{
			Debug.Log ("Not Enough Space (Item)");
			return false;
		}

		//print (portion);
		switch(item_name)
		{
		case "Portion":
		{
			//Debug.Log("Ordered Portion");
			Order.desk_space[instant_index[0]].item_obj[instant_index[1]] = (GameObject) GameObject.Instantiate (GetPrefab(item_name), pos, Quaternion.identity);
			//Money_Management.SetGold(-5);
			break;
		}
		}

		//for(int i=0;i<3;i++)
			//for(int j=0;j<6;j++)
				//Debug.Log(Order.desk_space[i].item_obj[j]);

		Order.IncreaseItemCount();
		return true;
	}
	public static Vector3 GetAvailableItemSpace()
	{
		for(int i=0;i<Order.Desk_MAX;i++)
		{
			if(GetDeskInUse(i) == true)
			{
				Vector3 pos = GetAvailableItemSpace_OnDesk(i);
				if(pos != Vector3.zero)
				{
					return pos;
				}
			}
		}
		return Vector3.zero;
	}
	static Vector3 GetAvailableItemSpace_OnDesk(int desk_index)
	{
		for(int item_index=0;item_index<Order.DeskItem_MAX;item_index++)
		{
			if(Order.desk_space[desk_index].item_space_in_use[item_index] == false)
			{
				instant_index[0] = desk_index;
				instant_index[1] = item_index;
				Order.desk_space[desk_index].item_space_in_use[item_index] = true;
				return Order.desk_space[desk_index].vector + new Vector3(-1+item_index*0.4f,0.6f,0);
			}
		}
		return Vector3.zero;
	}



	public static bool meth_Desk_Order(string desk_name)
	{
		Vector3 pos = Order.GetAvailableDeskSpace();

		if (pos == Vector3.zero)
		{
			Debug.Log ("Not Enough Space (Desk)");
			return false;
		}

		switch(desk_name)
		{
		case "Basic_Desk":
		{
			desk_space[temp].desk_obj = (GameObject)GameObject.Instantiate (GetPrefab(desk_name), pos, Quaternion.identity);
			break;
		}
		}

		return true;
	}
	static Vector3 GetAvailableDeskSpace()
	{
		for(int i=0;i<Order.Desk_MAX;i++)
		{
			if(GetDeskInUse(i) == false) //not use space
			{
				temp = i;
				Order.desk_space[i].in_use = true;
				return Order.desk_space[i].vector;
			}
		}



		//Debug.Log("GetAvailableDeskSpace() Error occured");
		return Vector3.zero;
	}



	static GameObject GetPrefab(string prefab_name)
	{
		return (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/"+prefab_name+"_Prefab.prefab", typeof(GameObject));
	}
	static bool GetDeskInUse(int index)
	{
		return Order.desk_space[index].in_use;
	}

	//item_management
	public static int GetItemCount()
	{
		return Order.item_count;
	}
	public static bool IncreaseItemCount()
	{
		Order.item_count++;
		return true;
	}
	public static bool DecreaseItemCount()
	{
		Order.item_count--;
		return true;
	}
	public static Desk_Space GetDeskspace(int index)
	{
		return desk_space[index];
	}
	public static BuyList Getbuyitem()
	{
		ArrayList buylist = new ArrayList();
		for (int i=0;i<Order.Desk_MAX;i++)
		{
			for (int j=0;j<Order.DeskItem_MAX;j++)
			{
				if(Order.desk_space[i].item_space_in_use[j] == true)
				{
					buylist.Add(new BuyList(Order.desk_space[i].item_obj[j], i, j));
				}
			}
		}

		int rand = Random.Range(0,buylist.Count);
		
		//Debug.Log ("Item["+rand+"] is Selected");
		return (BuyList)buylist[rand];//GameObject.FindGameObjectsWithTag("Item");
	}
	public static bool SetItemInUse(int desk_index, int item_index, bool value)
	{
		Order.desk_space[desk_index].item_space_in_use[item_index] = value;
		return true;
	}
	public static int GetPrice(int index)
	{
		return Order.price[index];
	}
}
