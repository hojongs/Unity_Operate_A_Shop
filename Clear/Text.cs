using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {

	float width;
	float height;

	// Use this for initialization
	void Start () {
		width = 400;
		height = 150;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect((Screen.width - width)/2, (Screen.height - height)/2, width, height), "Game Clear"))
		{
			Application.LoadLevel("Menu_Scene");
		}
	}
}
