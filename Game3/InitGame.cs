using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        MenuboardManager.component.menu_on();

        if (OrderButtonSpawner.OrderDeskButton_Init() == false)
        {
            Debug.Log("Error Occured");
        }
        if (OrderButtonSpawner.OrderItemButton_Init() == false)
        {
            Debug.Log("Error Occured");
        }

        MenuboardManager.component.menu_off();


        if (MoneyManager.MoneyInit(25) == false)
        {
            Debug.Log("Error Occured");
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
