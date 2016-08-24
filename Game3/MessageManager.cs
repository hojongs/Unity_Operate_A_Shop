using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour {

    public static MessageManager messageBox;

    void Start()
    {
        MessageManager.messageBox = this;
    }

    public IEnumerator printMessage(string msg)
    {
        int sec = 1;

        UILabel component = GetComponent<UILabel>();

        if (component.text != "")
        {
            component.text += "\n";
            sec = 2;
        }
        component.text += msg;

        string temp = component.text;

        yield return new WaitForSeconds(sec);

        if (component.text == temp)
            component.text = "";
    }

    public void clearMessage()
    {
        UILabel component = GetComponent<UILabel>();

        component.text = "";
    }
}
