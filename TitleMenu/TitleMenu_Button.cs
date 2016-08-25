using UnityEngine;
using System.Collections;

public class TitleMenu_Button : MonoBehaviour {

	float width_center;
	float height_start;
	float bt_width, bt_height;

	float gap;

	// Use this for initialization
	void Start () {
		bt_width = 200;
		bt_height = 75;

		gap = bt_height * 1.5f;

		width_center = Screen.width / 2;
		height_start = Screen.height / 2 - bt_height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if(GUI.Button(button_pos(0), "How To Play"))
		{

		}
		if(GUI.Button(button_pos(1), "Play"))
		{
			Debug.Log ("Play");
            Application.LoadLevel(1);//"Game2_Scene");
		}
		if(GUI.Button(button_pos(2), "Exit"))
		{
			
		}
	}

	Rect button_pos(int pos)
	{

		return new Rect(width_center - bt_width/2, height_start + pos*gap, bt_width, bt_height);
	}
}
