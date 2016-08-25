using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        MenuboardManager.component.menu_on();

        if (ButtonSpawner.OrderDeskButton_Init() == false)
        {
            Debug.Log("Error Occured");
        }
        if (ButtonSpawner.OrderItemSelectButton_Init() == false)
        {
            Debug.Log("Error Occured");
        }

        ButtonSpawner.PriceButtonInit();

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
