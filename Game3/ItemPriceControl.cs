using UnityEngine;
using System.Collections;

public class ItemPriceControl : MonoBehaviour
{
    public int value;
    public static UILabel name_label;
    public static UILabel price_label;

    void OnClick()
    {
        ItemType select = OrderItemCommandButton.type;

        if (select == null)
            return;

        select.profit += value;

        PriceView(select);
    }

    public static void PriceView(ItemType select)
    {
        int profit_percent = 100 + select.profit;
        int sell_price = Mathf.FloorToInt(select.price * profit_percent*0.01f);
        price_label.text = sell_price + " (" + profit_percent + "%)";

    }
}
