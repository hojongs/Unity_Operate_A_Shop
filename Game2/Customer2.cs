using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer2 : MonoBehaviour {

	//private GameObject Object_Item;
	Dictionary<string, int> Object_Index;

	private Vector3 src_pos;
	private Vector3 dst_pos;

	private int state;

	private bool move_init;
	private float start_time;
	public  float distance_time = 8;
	private float t;

	int desk_pos;
	int item_pos;

	// Use this for initialization
	void Start () {
		//if(Order.GetItemCount()<=0)
		//	Destroy(this.gameObject);
		state = 0;
		move_init = false;
		//Object_Item = Select_Item();
		Object_Index = Object_Management.Select_Item();

		//Debug.Log (Object_Index[0]);
		//Debug.Log (Object_Index[1]);
		animation.Play ("Walk"); //Check Anime (1/3)
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
			case 0: //go to the desk
			{
				Move();
				Check_Item();
				break;
			}
			case 1: //arrived to the desk
			{
				Buy();
				break;
			}
			case 2: //go to the exit
			{
				Move();
				ShowText();
				break;
			}
			case 3:
			{
				Exit();
				break;
			}
		}
	}

	GameObject Select_Item()
	{
		BuyList item_value = Order.Getbuyitem();

		GameObject Object_Item = item_value.GetItem();
		desk_pos = item_value.GetDeskIndex();
		item_pos = item_value.GetItemIndex();
		//print (Object_Item);
		//print (desk_pos);
		//print (item_pos);

		//int Item_Index = Random.Range(0,Object_Item.Length); //exception point (out of range)
		//print (Object_Item[Item_Index]);
		//if (Object_Item == null)//if (Object_Item[Item_Index] == null)
		//{
		//	print("Destroy");
		//	Destroy(this.gameObject);
		//	return null;
		//}
		//else
		return Object_Item;
			//return Object_Item[Item_Index];
	}
	
	void Move()
	{
		if(move_init == false)
		{
			src_pos = this.transform.position;
			start_time = Time.time;
			switch(state)
			{
			case 0: //go to the desk
			{
				//dst_pos = Order.GetDeskspace(desk_pos).desk_obj.transform.position + new Vector3 (0,0.5f,2);//desk;
				dst_pos = Object_Management.GetDeskForward(Object_Index["desk"]);
				//Debug.Log (dst_pos);
				break;
			}
			case 2: //go to the exit
			{
				//dst_pos = new Vector3 (50,9,5);//exit;
				dst_pos = new Vector3 (100,9,100);
				break;
			}
			}
			move_init = true;
		}
		else
		{
			t = ( (Time.time - start_time)*distance_time ) / Vector3.Distance(src_pos, dst_pos);
			//print(t);
			//print (src_pos);
			//print (dst_pos);
			//Debug.Break ();
			this.transform.position = Vector3.Lerp(src_pos, dst_pos, t);
			this.transform.rotation = Quaternion.LookRotation(dst_pos - src_pos);
			this.transform.Rotate (new Vector3(0,90,0));
			if(t >= 1)
			{
				move_init = false;
				state++;
				//print(state);
				//Debug.Break();
			}
		}
	}

	void Check_Item()
	{
		bool result = Object_Management._Check_Item(Object_Index);

		if (result == false)
		{
			SetText("Oops! There is not the item.");
			animation.Play("Oops");
			move_init = false;
			state = 2;
		}
	}
	
	void Buy()
	{
		int result = Object_Management.BuyItem(Object_Index);
		switch(result)
		{
		case 0:
			this.audio.Play();
			SetText("Thank you!");
			break;
		case 1:
			Debug.Log ("Sold Out");
			animation.Play("Oops");
			SetText("Oops! There is not the item.");
			break;
		case 2:
			Debug.Log ("There is not the desk");
			SetText("Oops! There is not the item.");
			animation.Play("Oops");
			break;
		}
		state++;

		//desk.DecreaseItemCount();
		//item.BuyItem();

		/*
		//print(Object_Item);
		if(Object_Item)
		{
			this.audio.Play(); //sound
			//print(desk_pos);
			//print(item_pos);
			//Debug.Break ();
			Order.SetItemInUse(desk_pos, item_pos,false);
			Order.DecreaseItemCount();
			if(Object_Item == (GameObject) Resources.LoadAssetAtPath("Assets/Prefabs/Game2/Portion_Prefab.prefab",typeof(GameObject)))
			{
				Money_Management.SetGold(7);
			}
			Destroy(Object_Item);
			//Button.exists = false;
			//Button.gold += 7;
			animation.Play ("Walk"); //Check Anime(2/3)
		}
		else
			animation.Play("Oops"); //Check Anime (3/3)

		this.transform.rotation = Quaternion.Euler (0,0,0);
		state++;
		*/
	}

	void SetText(string text)
	{
		GUIText gui_text = this.gameObject.GetComponentInChildren<GUIText>();

		gui_text.text = text;
	}

	void ShowText()
	{
		//print(this.gameObject.GetComponentInChildren<Renderer>());
		//Debug.Break ();
		if(this.gameObject.GetComponentInChildren<Renderer>().isVisible)
		{
			GUIText gui_text = this.gameObject.GetComponentInChildren<GUIText>();
		
			Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position + Vector3.up * 15);

			//pos.y += 0.2f;

			gui_text.transform.position = pos;
		}
	}
	
	void Exit()
	{
		Customer_Spawn2.customer_count--;
		Destroy(this.gameObject);
	}
}
