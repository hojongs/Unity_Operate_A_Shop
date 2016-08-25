using UnityEngine;
using System.Collections;

public class MenuCloseButton : MonoBehaviour
{
    public GameObject menuboardManager;

    void OnClick()
    {
        MenuboardManager.component.menu_off();
    }
}
