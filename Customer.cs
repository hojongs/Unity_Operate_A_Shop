using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

	private Vector3 dest_pos;
	private Vector3 first_pos;
	private Quaternion temp_rotation;
	//private float num=10000;
	
	//private int nextTime;

	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;

	private int state;
		//0: before_buy
		//1: buying
		//2: after_buy


	private GameObject Object_Item;

	private bool buy_ready;

	private bool Buyed;

	// Use this for initialization
	void Start () {

		state=0;

		Move_Init();

		//Select the item to purchase
		Object_Item = Select_Item();

		buy_ready = false;

		Buyed = true;
	}
	
	// Update is called once per frame
	void Update () {

		switch(state)
		{
			case 0: //before_buy

				Move(first_pos, dest_pos);
				break;

			case 1: //arrived & buying
			{
				//print(Object_Item);
				if(buy_ready)
					Buy(Object_Item);
				break;
			}

			case 2:	//after_buy

				Move(first_pos, dest_pos);
				
				Exit();
				break;

		}


		/*
		if(Input.GetMouseButtonDown(0))
		{
			//animation.Stop(animation.clip.name);
			//animation.Stop("Stand");
			animation.Play (animation.clip.name);
			//Debug.Log ("Play");
			//Debug.Log (animation.GetClipCount());
			Debug.Log (animation.clip.name);
		}
		else if(Input.GetMouseButtonDown(1))
		{
			animation.Play ("Walk");
			Debug.Log ("Walk");
		}

		if(this.transform.position != dest_pos)//if(Input.GetMouseButtonDown(0))
		{
		//Debug.Log ((dest - this.transform.position)/10);
		//while(this.transform.position != dest)
		temp_rotation = this.transform.rotation;
		this.transform.rotation = Quaternion.identity;//.Euler(0,90,0);
		//Debug.Log (Quaternion.identity);
		//Debug.Break();
		
		//this.transform.Translate((dest - this.transform.position)/100);
		////Debug.Log (dest - this.transform.position);
		//this.transform.Translate(dest - this.transform.position);
		for(int i=0;i<num;i++)
		{
			//if(Mathf.FloorToInt(Time.time) >= nextTime)
			//{
				this.transform.Translate((dest_pos - first_pos)/num);
				//nextTime++;
			//}
		}
		//this.transform.Tran
		
		this.transform.rotation = temp_rotation;
		}
		*/
	}
	
	GameObject Select_Item()
	{
		GameObject[] Object_Item = GameObject.FindGameObjectsWithTag("Item");
		
		//Debug.Log (Object_Item.Length);
		//Debug.Break();

		return Object_Item[Random.Range(0,Object_Item.Length)];
	}

	void Move_Init()
	{
		dest_pos = new Vector3 (0,6,5);
		first_pos = this.transform.position;
		//nextTime = 1;
		
		startTime = Time.time;
		journeyLength = Vector3.Distance(first_pos, dest_pos);
		
		if(first_pos != dest_pos)
		{
			//if(Buyed)
				animation.Play ("Walk");
			//else
			//	animation.Play ("Oops");
		}



	}

	void Move(Vector3 param_src,Vector3 param_dest)
	{
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		this.transform.position = Vector3.Lerp(param_src, param_dest, fracJourney);

		if(this.transform.position == dest_pos)
		{
			animation.Play ("Stand");
			this.transform.rotation = Quaternion.Euler(0,90,0);
			state++; // go to buying state

			//print("Stand");
			StartCoroutine(Stand_To_Buy());
		}
	}

	void Change_Position(Vector3 param_first_pos, Vector3 param_dest_pos)
	{
		first_pos = param_first_pos;
		dest_pos = param_dest_pos;

		startTime = Time.time;
		journeyLength = Vector3.Distance(first_pos, dest_pos);
	}

	IEnumerator Stand_To_Buy()
	{
		//print("Check : "+Object_Item);
		if(Object_Item)
		{
			//print("Check");
			yield return new WaitForSeconds(0.3f);
			Gui.exists = false;
			Gui.gold += 7;
			Buyed = true;
		}
		//else

		buy_ready = true;
	}

	void Buy(GameObject Object_Item)
	{
		//print("Buying");
		//Gold++

		//print(Object_Item);
		Destroy(Object_Item);
//		print(Object_Item);

		state = 2;
		Change_Position(this.transform.position, new Vector3 (25,6,5));
		//Debug.Log(first_pos);
		//Debug.Log(dest_pos);
		//Debug.Break ();

		if(Object_Item)
			animation.Play ("Walk");
		else
			animation.Play("Oops");
		this.transform.rotation = Quaternion.Euler (0,0,0);
	}

	void Exit()
	{
		if(this.transform.position == dest_pos)
		{
			Destroy(this.gameObject);
			Customer_Spawn.customer_count--;
		}
	}
}
