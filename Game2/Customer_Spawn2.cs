using UnityEngine;
using System.Collections;

public class Customer_Spawn2 : MonoBehaviour {

	public GameObject customer;
	public float term = 1; //spawn second(s)
	public static int customer_count;
	float nextTime = 1;

	public int customer_MAX = 100;
	public float time_cycle = 0.5f;

	Vector3 temp_pos;

	// Use this for initialization
	void Start () {
		nextTime = 1;
		Customer_Spawn2.customer_count = 0;

		temp_pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Time.time);

		//Debug.Log (Mathf.FloorToInt(Time.time));
		if(time() && Customer_Spawn2.customer_count < customer_MAX && Object_Management.GetTotalItemCount() > 0) //Order.GetItemCount() > 0) // && Button.exists // if item exists
		{
			//Debug.Log (Object_Management.GetTotalItemCount());
			//Debug.Break ();
			//Debug.Log("Spawn is Ready");
			if(rand())
			{
				GameObject.Instantiate(customer, this.transform.position, this.transform.rotation);
				//Debug.Log ("Customer Spawned");
				Customer_Spawn2.customer_count++;
			}
			//nextTime = Mathf.FloorToInt(Time.time) + time_cycle;
			nextTime = Mathf.RoundToInt(Time.time) + time_cycle;

			//temp_pos.z = Random.Range(5,50); //position
			this.transform.position = temp_pos;
		}
	}

	bool time()
	{
		//return Mathf.FloorToInt(Time.time) >= nextTime;
		return Time.time >= nextTime;
		//return true;
	}

	bool rand()
	{
		return Random.Range (0,100)<70;
		//return Random.Range (0,100)==0;
		//return true;
	}


}
