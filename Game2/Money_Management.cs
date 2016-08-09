using UnityEngine;
using System.Collections;

public class Money_Management : MonoBehaviour {

	//List<int> mylist;
	static int gold;
	int gold_width;
	int gold_height;

	static int init_gold = 20;

	// Use this for initialization
	void Start () {
		if(Money_Management.InitGold() == false) {};
		this.gold_width = 100;
		this.gold_height = 50;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.Button(new Rect(Screen.width-gold_width, Screen.height-gold_height, gold_width, gold_height), "Gold : "+Money_Management.GetGold().ToString());
	}

	static bool InitGold()
	{
		Money_Management.gold = init_gold;
		return true;
	}

	public static int GetGold()
	{
		return gold;
	}

	public static bool SetGold(int value)
	{
		gold += value;
		return true;
	}
}
