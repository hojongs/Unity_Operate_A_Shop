using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    int nexttime;
    int time_cycle;
    int freq;
    int customer_height;

    public GameObject prefab;


    Dictionary<ItemType, List<int>> random_list = new Dictionary<ItemType, List<int>>();
    float total = 0;
    List<ItemType> key_list = new List<ItemType>();

    // Use this for initialization
    void Start()
    {
        nexttime = 1;
        time_cycle = 1;
        freq = 100;
        customer_height = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nexttime)
        {

            //if customer count
            //  customer count ++
            if (Random.Range(0, 100) < freq)
            {
                Vector3 pos = this.transform.position;
                pos.y = customer_height;
                GameObject customer = (GameObject) GameObject.Instantiate(prefab, pos, this.transform.rotation);

                float average = RoundItemSlotlist();

                //decide customer role (dont select item customer, select&buy customer, thief)
                int extra = 0;//10000;
                int normal = Mathf.FloorToInt((20 + 20*0.00001f*MoneyManager.sales) * average);
                //Debug.Log(normal);
                int thief = 50;
                int choice = Random.Range(0, extra + normal + thief);
                Customer3 component = customer.GetComponent<Customer3>();

                if (ItemManager.item_total_count <= 0 || choice < extra) //extra
                {
                    component.type = 0;
                    component.state = 3;
                    //Debug.Log("extra");
                }
                else
                {
                    choice -= extra;
                    if (choice < normal) // normal
                    {
                        component.type = 1;
                        
                        //select item (expensive item, choice low)
                        Slot slot = SelectItem();
                        component.item = slot;
                        //Debug.Log("normal");
                    }
                    else //thief
                    {
                        component.type = 2;

                        int count = ItemManager.item_slot_list.Count;
                        int rand = Random.Range(0, count);
                        component.item = ItemManager.item_slot_list[rand];
                        //Debug.Log("thief");
                    }

                    component.state = 0;
                }
            }
            random_list.Clear();
            key_list.Clear();
            total = 0;

            nexttime = Mathf.FloorToInt(Time.time) + time_cycle;
        }
    }

    float RoundItemSlotlist()
    {
        int len = ItemManager.item_slot_list.Count;
        //round item_slot_list
        for (int i = 0; i < len; i++)
        {
            GameObject obj = ItemManager.item_slot_list[i].GetObject();
            if (obj == null)
                continue;
            //else
            //    Debug.Log("Not Null");
            ItemType type = obj.GetComponent<ItemManager>().type;
            if (random_list.ContainsKey(type) == false) //first
            {
                random_list.Add(type, new List<int>());
                if(type.profit > 0)
                    total += 100 / type.profit * 1 / type.price * type.popularity;
                else
                    total += 100 * 1 / type.price * type.popularity;

                key_list.Add(type);
            }
            //Debug.Log(random_list[type]);
            random_list[type].Add(i);
        }

        float average;
        if (key_list.Count > 0) average = total / key_list.Count;
        else average = 0;

        return average;
    }

    Slot SelectItem()
    {

        //Debug.Log("total " + total);

        float rand1 = Random.Range(0, total);
        int j;
        //Debug.Log("rand1 " + rand1);
        for (j = 0; j < key_list.Count; j++) 
        {
            ItemType type = key_list[j];
            float value;
            if (type.profit > 0)
                 value = 100 / type.profit * 1 / type.price * type.popularity;
            else
                value = 100 * 1 / type.price * type.popularity;

            if (rand1 < value)
                break;
            else
                rand1 -= value;

            //random_list[index]
        }

        //Debug.Log(j);

        ItemType index = key_list[j];

        int rand2 = Random.Range(0, random_list[index].Count);

        //Debug.Log(random_list.Count);
        //Debug.Log(random_list[index].Count);
        //Debug.Log(random_list[index]);
        //Debug.Log(rand2);

        int slot_index = random_list[index][rand2];

        Debug.Log("slot_index" + slot_index);

        return ItemManager.item_slot_list[slot_index];






        /*
        Dictionary<string, List<Slot>> dict_list = new Dictionary<string, List<Slot>>();

        List<string> check_list = new List<string>();
        //int max1 = ItemManager.item_slot_list.Count;
        int max = ItemManager.item_type_list.Count;
        bool check;
        int all = 0;

        

        for (int i = 0; ItemManager.item_slot_list[i].GetObject(); i++) //add to variable 'all'
        {
            Slot slot;
            check = false;

            slot = ItemManager.item_slot_list[i];
            string name = slot.GetObject().GetComponent<ItemManager>().name;
            if(dict_list.ContainsKey(name) == false)
            {
                dict_list.Add(name, new List<Slot>());
            }
            dict_list[name].Add(slot);

            for (int j = 0; j < check_list.Count; j++)
                if (check_list[j] == name) //already added
                {
                    check = true;
                    break;
                }

            if (check == false)
                for (int j = 0; j < max; j++)
                    if (ItemManager.item_type_list[j].name == name)
                    {
                        all += ItemManager.item_type_list[j].popularity;
                        check_list.Add(name);
                        break;
                    }
        }

        //Debug.Log(all);

        //Debug.Log(check_list.Count);
        int chance = Random.Range(0, all);
        for (int i = 0; i < check_list.Count; i++) //select item
        {
            int j = 0;
            for (j = 0; j < max; j++)
                if (check_list[i] == ItemManager.item_type_list[j].name)
                    break;
            //Debug.Log(chance);
            //Debug.Log(ItemManager.item_type_list[j].popularity);
            //Debug.Break();
            if (chance < ItemManager.item_type_list[j].popularity)
            {
                //select i
                //customer.GetComponent<Customer3>.SelectItem(ItemManager.item_type_list[i].name);
                select_name = ItemManager.item_type_list[j].name;
                //Debug.Log(ItemManager.item_type_list[j].name);
                break;
            }
            else
            {
                chance -= ItemManager.item_type_list[i].popularity;
            }
        }

        int chance2 = Random.Range(0, dict_list[select_name].Count);

        return dict_list[select_name][chance2];
        */
    }
}
