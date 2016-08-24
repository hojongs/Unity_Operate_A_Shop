using UnityEngine;
using System.Collections;

public class Menu_Mode : MonoBehaviour {
	public GameObject menuboard;

	// Use this for initialization
	void Start ()
    {
        menuboard = GameObject.Find("UI Root (2D)") as GameObject;
        charactermode_on(true);
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
                charactermode_on(false);
            }
			else //character control mode
            {
                charactermode_on(true);
			}
		}
	}

    void charactermode_on(bool mode_switch)
    {
        this.GetComponent<CharacterMotor>().enabled = mode_switch;
        for (int i = 0; i < 2; i++)
            this.gameObject.GetComponentsInChildren<MouseLook>()[i].enabled = mode_switch;
        Screen.lockCursor = mode_switch;

        menuboard.active = !mode_switch;
        //transform.Find;
        //transform.FindChild;
        //GameObject.FindGameObjectWithTag;
        //GameObject.Find
    }
}
