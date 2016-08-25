using UnityEngine;
using System.Collections;

public class OrderItemSelectButton : MonoBehaviour
{
    public ItemType type;


    void OnClick()
    {
        OrderItemCommandButton.type = type;

        ItemType select = OrderItemCommandButton.type;

        Sound.component.GetComponent<AudioSource>().clip = Sound.component.clip[0];
        Sound.component.GetComponent<AudioSource>().Play();

        ItemPriceControl.name_label.text = select.name;
        ItemPriceControl.PriceView(select);
    }
}
