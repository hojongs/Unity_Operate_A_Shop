using UnityEngine;
using System.Collections;

public class MenuboardManager : MonoBehaviour {
    
    public GameObject menuboard;
    public GameObject character;
    public static MenuboardManager component;

    void Awake()
    {
        component = this;
    }

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {

    }


    public void menu_on()
    {
        //Debug.Log("Menu On");
        menuboard.active = true;

        character.GetComponent<CharacterMotor>().enabled = false;
        for (int i = 0; i < 2; i++)
            character.GetComponentsInChildren<MouseLook>()[i].enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void menu_off()
    {
        //Debug.Log("Menu Off");
        menuboard.active = false;

        character.GetComponent<CharacterMotor>().enabled = true;
        for (int i = 0; i < 2; i++)
            character.GetComponentsInChildren<MouseLook>()[i].enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MessageManager.messageBox.clearMessage();
    }
}
