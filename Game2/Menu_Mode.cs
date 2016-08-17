using UnityEngine;
using System.Collections;

public class Menu_Mode : MonoBehaviour {
	public GameObject menuboard;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(Screen.lockCursor) //menu mode
			{
				//print(this.gameObject.GetComponents<MouseLook>().Length); //1
				//print(this.gameObject.GetComponentsInChildren<MouseLook>().Length); //2
				//print(this.gameObject.GetComponentsInParent<MouseLook>().Length); //1
				for (int i=0;i<2;i++)
					this.gameObject.GetComponentsInChildren<MouseLook>()[i].enabled = false;
				this.GetComponent<CharacterController>().enabled = false;
				Screen.lockCursor = false;
			}
			else //character control mode
			{
				for (int i=0;i<2;i++)
					this.gameObject.GetComponentsInChildren<MouseLook>()[i].enabled = true;
				this.GetComponent<CharacterController>().enabled = true;
				Screen.lockCursor = true;
			}
		}
	}
}
