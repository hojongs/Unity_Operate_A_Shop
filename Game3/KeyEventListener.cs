using UnityEngine;
using System.Collections;
using System.Xml;

public class KeyEventListener : MonoBehaviour
{
    public GameObject menuboardManager;
    public GameObject messageManager;

    public AudioClip[] clip;

    // Use this for initialization
    void Start()
    {
        //menuboard = GameObject.Find("UI Root (2D)") as GameObject;
        //Debug.Log(menuboard);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(Cursor.lockState);
            if (Cursor.lockState == CursorLockMode.Locked) //menu mode
            {
                menuboardManager.GetComponent<MenuboardManager>().menu_on();
            }
            else //character control mode
            {
                menuboardManager.GetComponent<MenuboardManager>().menu_off();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            /*
            switch(DeskManager.OrderDesk("Basic_Desk", 5))
            {
                case 0://success
                    GetComponent<AudioSource>().clip = clip[0];
                    break;
                case 1:
                    GetComponent<AudioSource>().clip = clip[1];
                    StartCoroutine(messageManager.GetComponent<MessageManager>().msg_control("Not Enough Money (Desk)"));
                    Debug.Log("no money");
                    break;
                case 2:
                    GetComponent<AudioSource>().clip = clip[1];
                    StartCoroutine(messageManager.GetComponent<MessageManager>().msg_control("Not Enough Space (Desk)"));
                    Debug.Log("no space");
                    break;
            }
            GetComponent<AudioSource>().Play();

            switch (ItemManager.OrderItem("Health_Portion", 5))
            {
                case 0://success
                    GetComponent<AudioSource>().clip = clip[0];
                    break;
                case 1:
                    GetComponent<AudioSource>().clip = clip[1];
                    StartCoroutine(messageManager.GetComponent<MessageManager>().msg_control("Not Enough Money (Item)"));
                    Debug.Log("no money");
                break;
                case 2:
                    GetComponent<AudioSource>().clip = clip[1];
                    StartCoroutine(messageManager.GetComponent<MessageManager>().msg_control("Not Enough Space (Item)"));
                    Debug.Log("no space");
                break;
            }
            GetComponent<AudioSource>().Play();
            */
        }
    }
}
