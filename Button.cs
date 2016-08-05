using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject portion;
	public static int gold;
	public int exists_MAX;

	public static GameObject[] Item_list;

	// Use this for initialization
	void Start () {
		//exists = false;
		//if(exists_init() == 0)
		//	print("exists_init complete");
		//else
		//	print("*** exists_init failed ***");

		Button.gold = 20;

		Button.Item_list = new GameObject[11];
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetMouseButtonDown(1))
		{
			gold++;
		}
		*/

		//if(Input.GetMouseButtonDown(1))
			//for(int i=0;i<11;i++)
				//Debug.Log (i+" "+Button.Item_list[i]);
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,120,100), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(25,40,90,20), "Portion 5 / 7") && Button.gold - 5 >= 0) {
			Vector3 Empty=GetEmpty();
			if(Empty != Vector3.zero)
			{
				//Application.LoadLevel(1);

				//print(Empty);
				//Debug.Break ();
	
				Button.gold -= 5;
	
				Button.Item_list[Mathf.FloorToInt((Empty.x + 5)/2)] = (GameObject) GameObject.Instantiate(portion, Empty, Quaternion.identity);
			}
		}
		
		// Make the second button.
		//if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			//Application.LoadLevel(2);
		//}

		GUI.Button(new Rect(Screen.width-200,Screen.height-100,150,30), "Gold : "+Button.gold.ToString());
	}

	Vector3 GetEmpty()
	{
		for(int i=0;i<6;i++)
		{
			if(GetExists(i) == 1) //Item dont exists
			{
				return new Vector3(-5+i*2, 5.5f, 0);
			}
		}
		return Vector3.zero;
	}

	int GetExists(int index)
	{
		//print("index : "+index);
		//print("Value : "+Button.Item_list[index]);
		if(Button.Item_list[index] != null)
			return 0; //Item exists
		else
			return 1; //Item dont exists
	}

	/*
	Vector3 Arrange()
	{
		Vector3 value = new Vector3();
		for(int i=0;i < 11;i++)
		{
			if(!Button.exists[i])
			{
				Button.exists[i] = true;
				value = new Vector3 (-5+i, 5.5f, 0);
			}
		}
		return value;
	}
	*/


	/*
	int exists_init()
	{
		exists = new bool[exists_MAX];
		for(int i=0;i<exists_MAX;i++)
			exists.SetValue(false, i);
		return 0;
	}
	*/
}
