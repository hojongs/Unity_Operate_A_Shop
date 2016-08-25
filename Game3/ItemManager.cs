using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemType
{
    public string name;
    public int price;
    public int popularity;
    public int profit;
    public GameObject prefab;
    public List<GameObject> obj_list;


    public ItemType(string name, int price, int popularity, GameObject prefab)
    {
        this.name = name;
        this.price = price;
        this.popularity = popularity;
        this.profit = 20;
        this.prefab = prefab;
        obj_list = new List<GameObject>();
    }
}

public class ItemManager : MonoBehaviour
{

    public static List<Slot> item_slot_list = new List<Slot>();
    public static List<ItemType> item_type_list;
    public static int item_total_count = 0;
    
    public static int OrderItem(ItemType type)//XmlNode data);
    {
        int result = 0;

        //Debug.Log(type.name);

        if (MoneyManager.CompareMoney(type.price))
        { //ok
            int index = GetSpace();

            if (index != -1)
            { //ok
                ItemSpawn(index, type);
                MoneyManager.AddMoney(type.price * -1);
            }
            else result = 2; //no space
        }
        else result = 1; //no money


        return result;
    }

    static int GetSpace()
    {
        int result = -1;
        for (int i = 0; i < item_slot_list.Count; i++)
        {
            GameObject obj = item_slot_list[i].GetObject();

            if (obj == null)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    static bool ItemSpawn(int index, ItemType type)
    {
        Vector3 pos = item_slot_list[index].GetPos();
        GameObject item = (GameObject)GameObject.Instantiate(type.prefab, pos, Quaternion.identity);

        ItemManager component = item.AddComponent<ItemManager>();
        component.type = type;
        

        item_slot_list[index].SetObject(item);
        type.obj_list.Add(item);

        item_total_count++;

        //for (int i = 0; i < item_slot_list.Count; i++)
        //    Debug.Log(item_slot_list[i].GetObject());
        //Debug.Break();

        //Debug.Log(item_type_list[0].obj_list.Count);
        //Debug.Log(item_type_list[0].obj_list[0]);
        //Debug.Break();

        return true;
    }

    public ItemType type;
}
