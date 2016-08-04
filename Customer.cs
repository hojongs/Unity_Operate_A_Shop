using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

	private GameObject Object_Item;

	private Vector3 src_pos;
	private Vector3 dst_pos;

	private int state;

	private bool move_init;
	private float start_time;
	public  float distance_time;
	private float t;

	// Use this for initialization
	void Start () {
		state = 0;
		move_init = false;
		Object_Item = Select_Item();
		animation.Play ("Walk"); //Check Anime (1/3)
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
			case 0: //go to the desk
			{
				Move();
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
		GameObject[] Object_Item = GameObject.FindGameObjectsWithTag("Item");
		//print (Object_Item.Length);
		int Item_Index = Random.Range(0,Object_Item.Length); //exception point (out of range)
		//print (Object_Item[Item_Index]);
		if (Object_Item[Item_Index] == null)
		{
			Destroy(this.gameObject);
			return null;
		}
		else
			return Object_Item[Item_Index];
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
					dst_pos = new Vector3(0,6,5);//desk;
					break;
				}
				case 2: //go to the exit
				{
					dst_pos = new Vector3 (25,6,5);//exit;
					break;
				}
			}
			move_init = true;
		}
		else
		{
			t = (Time.time - start_time)/distance_time;
			//print(t);
			//print (src_pos);
			//print (dst_pos);
			//Debug.Break ();
			this.transform.position = Vector3.Lerp(src_pos, dst_pos, t);
			if(t >= 1)
			{
				move_init = false;
				state++;
				//print(state);
				//Debug.Break();
			}
		}
	}
	
	void Buy()
	{
		print(Object_Item);
		if(Object_Item)
		{
			Destroy(Object_Item);
			Gui.exists = false;
			Gui.gold += 7;
			animation.Play ("Walk"); //Check Anime(2/3)
		}
		else
			animation.Play("Oops"); //Check Anime (3/3)

		this.transform.rotation = Quaternion.Euler (0,0,0);
		state++;
	}
	
	void Exit()
	{
		Destroy(this.gameObject);
		Customer_Spawn.customer_count--;
	}
}
