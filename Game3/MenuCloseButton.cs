using UnityEngine;
using System.Collections;

public class MenuCloseButton : MonoBehaviour
{
    public GameObject menuboardManager;

    void OnClick()
    {
        menuboardManager.GetComponent<MenuboardManager>().menu_off();
    }
}
