using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
    static int money;
    
    public static bool MoneyInit(int money)
    {
        MoneyManager.money = money;

        return true;
    }
    
    public static bool CompareMoney(int price)
    {
        return (MoneyManager.money >= price);
    }
    public static void AddMoney(int price)
    {
        money += price;
    }

    public static int GetMoney()
    {
        return MoneyManager.money;
    }
    
    int moneyview_width = 100;
    int moneyview_height = 50;

    void OnGUI()
    {
        GUI.Button(new Rect(Screen.width - moneyview_width, Screen.height - moneyview_height, moneyview_width, moneyview_height), "Gold : " + MoneyManager.GetMoney().ToString());
    }
}
