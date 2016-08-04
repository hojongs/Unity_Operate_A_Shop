using UnityEngine;
using System.Collections;

public class Customer_Spawn : MonoBehaviour {

	public GameObject customer;
	public float term = 1; //spawn second(s)
	private int nextTime = 1;
	static public int customer_count;

	// Use this for initialization
	void Start () {
		nextTime = 1;
		customer_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Time.time);

		//Debug.Log (Mathf.FloorToInt(Time.time));
		if(Mathf.FloorToInt(Time.time) >= nextTime && Gui.exists && customer_count < 2) // if item exists
		{
			//Debug.Log("Spawn is Ready");
			if(Random.Range (0,1)==0)
			{
				GameObject.Instantiate(customer, this.transform.position, this.transform.rotation);
				//Debug.Log ("Customer Spawned");
				Customer_Spawn.customer_count++;
			}
			nextTime = Mathf.FloorToInt(Time.time) + 2;
		}
	}
}
