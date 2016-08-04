using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	public GameObject portion;
	public static bool exists;

	public static int gold;

	// Use this for initialization
	void Start () {
		exists = false;
		Gui.gold = 20;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetMouseButtonDown(1))
		{
			gold++;
		}
		*/
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,120,100), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(25,40,90,20), "Portion 5 / 7") && !Gui.exists) {
			//Application.LoadLevel(1);
			Gui.gold -= 5;
			GameObject.Instantiate(portion, new Vector3 (0,5.5f,0), Quaternion.identity);
			Gui.exists = !Gui.exists;
		}
		
		// Make the second button.
		//if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			//Application.LoadLevel(2);
		//}

		GUI.TextArea(new Rect(Screen.width-250,Screen.height-150,200,100), Gui.gold.ToString());
	}
}
