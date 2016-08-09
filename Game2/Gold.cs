using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

	//List<int> mylist;
	static int gold;
	int gold_width;
	int gold_height;

	static int init_gold = 20;

	// Use this for initialization
	void Start () {
		this.gold_width = 100;
		this.gold_height = 50;

		if(InitGold() == false)
			Debug.Log ("Error Occured");

	}

	void OnGUI()
	{
		GUI.Button(new Rect(Screen.width-gold_width, Screen.height-gold_height, gold_width, gold_height), "Gold : "+Gold.GetGold().ToString());
	}

	static bool InitGold()
	{
		Gold.gold = init_gold;
		return true;
	}

	public static int GetGold()
	{
		return gold;
	}

	public static bool AddGold(int value)
	{
		gold += value;
		return true;
	}
}
