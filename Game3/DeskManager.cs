using UnityEngine;
using System.Collections;
using System.Xml;

public class DeskManager : MonoBehaviour
{
    public static Slot[] desk_slot_list;
    //public static List<DeskInfo> desk_type_list; //추가바람

    public static int OrderDesk(string name, int price, int item_count, Vector3 item_startpos, GameObject prefab)
    {
        int result = 0;
        //string desk_name = "Basic_Desk";//data.SelectSingleNode("name").InnerText;
        //int price = 5;//int.Parse(data.SelectSingleNode("price").InnerText);

        if (MoneyManager.CompareMoney(price))
        { //ok
            int index = GetSpace();

            if (index != -1)
            { //ok
                DeskSpawn(index, item_count, item_startpos, prefab);
                MoneyManager.AddMoney(price * -1);
            }
            else
            {
                result = 2;
            }
        }
        else
        {
            result = 1;
        }


        return result;
    }

    static int GetSpace()
    {
        int index = -1;

        for (int i = 0; i < desk_slot_list.Length; i++)
        {
            GameObject obj = desk_slot_list[i].GetObject();

            if (obj == null)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    static bool DeskSpawn(int index, int item_count, Vector3 item_startpos, GameObject prefab)
    {
        //desk spawn
        Vector3 pos = desk_slot_list[index].GetPos();
        GameObject desk = (GameObject)GameObject.Instantiate(prefab, pos, Quaternion.identity);

        //insert desk to desk_slot_list
        desk_slot_list[index].SetObject(desk);
        
        //extend item_slot_list
        for (int i = 0; i < item_count; i++)
            ItemManager.item_slot_list.Add(
                new Slot(pos + item_startpos + (Vector3.right * 4 * i))
                                        );

        return true;
    }
}