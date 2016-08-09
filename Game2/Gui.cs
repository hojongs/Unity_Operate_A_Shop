using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	int bt_width, bt_height;
	//Dictionary

	// Use this for initialization
	void Start () {
		bt_width = 100;
		bt_height = 50;

		if(Order.Order_Init() == false)
		{
			print("Order_Init Error");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


	
	void OnGUI()
	{
		if(GUI.Button (new Rect(10,
		                        10,
		                        this.bt_width, 
		                        this.bt_height), "Portion"))
		{
			if(Money_Management.GetGold()>=Order.GetPrice(0) && Order.meth_Item_Order("Portion"))
			{
				audio.Play();
			}
			else
			{
				print("Not Enough Money");
			}
		}

		if(GUI.Button (new Rect(10+this.bt_width, 
		                        10, 
		                        this.bt_width,
		                        this.bt_height), "Basic_Desk"))
		{
			int result = Desk_Management.OrderDesk("Basic_Desk");
			if(result == 1)
				Debug.Log ("Desk Slot is all using");
			else if(result == -1)
				Debug.Log ("Error Occured");
			else if(result == 0) //success
				audio.Play ();
				


			//if(Order.meth_Desk_Order("Basic_Desk"))
			//	audio.Play();
			//else
				//audio.Play();
		}
	}
}
