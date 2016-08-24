using UnityEngine;
using System.Collections;

public class OrderDeskButton : MonoBehaviour
{
    //XmlNode data;
    public string name;
    public int price;
    public GameObject prefab;
    public int item_count;
    public Vector3 item_startpos;

    public void init(string name, int price, int item_count, Vector3 item_startpos, GameObject prefab)
    {
        this.name = name;
        this.price = price;
        this.item_count = item_count;
        this.item_startpos = item_startpos;
        this.prefab = prefab;
    }

    void OnClick()
    {
        string msg;
        switch(DeskManager.OrderDesk(name, price, item_count, item_startpos, prefab))
        {
            case 0: //success
                Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[0];
                break;
            case 1:
                Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[1];
                msg = "no money";
                Debug.Log(msg);

                StartCoroutine(MessageManager.messageBox.printMessage(msg));

                break;
            case 2:
                Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[1];
                msg = "no space";
                Debug.Log(msg);

                StartCoroutine(MessageManager.messageBox.printMessage(msg));

                break;
        }
        Sound.component.GetComponent<AudioSource>().Play();
    }
}
