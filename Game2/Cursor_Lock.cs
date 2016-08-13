using UnityEngine;
using System.Collections;

public class Cursor_Lock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(Screen.lockCursor)
			{
				//print(this.gameObject.GetComponents<MouseLook>().Length); //1
				//print(this.gameObject.GetComponentsInChildren<MouseLook>().Length); //2
				//print(this.gameObject.GetComponentsInParent<MouseLook>().Length); //1
				for (int i=0;i<2;i++)
					this.gameObject.GetComponentsInChildren<MouseLook>()[i].enabled = false;

				Screen.lockCursor = false;
			}
			else
			{
				for (int i=0;i<2;i++)
					this.gameObject.GetComponentsInChildren<MouseLook>()[i].enabled = true;
				Screen.lockCursor = true;
			}
		}
	}
}
