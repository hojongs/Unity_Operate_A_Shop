using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    int nexttime;
    int time_cycle;
    int freq;
    int customer_height;

    public GameObject prefab;

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
            nexttime = Mathf.FloorToInt(Time.time) + time_cycle;

            //if customer count
            //  customer count ++
            if (Random.Range(0, 100) < freq)
            {
                Vector3 pos = this.transform.position;
                pos.y = customer_height;
                GameObject customer = (GameObject) GameObject.Instantiate(prefab, pos, this.transform.rotation);

                //decide customer role (dont select item customer, select&buy customer, thief)
                int extra = 0;//100;
                int normal = 20;
                int thief = 2;
                int choice = Random.Range(0, extra + normal + thief);
                if (ItemManager.item_total_count <= 0 || choice < extra) //extra
                {
                    customer.GetComponent<Customer3>().type = 0;
                    customer.GetComponent<Customer3>().state = 3;
                    //Debug.Log("extra");
                }
                else
                {
                    choice -= extra;
                    if (choice < normal) // normal
                    {
                        customer.GetComponent<Customer3>().type = 1;
                        //Debug.Log("normal");
                    }
                    else //thief
                    {
                        customer.GetComponent<Customer3>().type = 2;
                        //Debug.Log("thief");
                    }
                    
                    //select item (expensive item, choice low)
                    Slot slot = SelectItem();
                    customer.GetComponent<Customer3>().item = slot;
                    customer.GetComponent<Customer3>().state = 0;
                }
            }
        }
    }

    Slot SelectItem()
    {
        Dictionary<ItemType, List<int>> random_list = new Dictionary<ItemType, List<int>>();
        int total = 0;
        List<ItemType> key_list = new List<ItemType>();

        int len = ItemManager.item_slot_list.Count;
        //round item_slot_list
        for (int i=0;i<len;i++)
        {
            GameObject obj = ItemManager.item_slot_list[i].GetObject();
            if (obj == null)
                continue;
            ItemType type = obj.GetComponent<ItemManager>().type;
            if (random_list.ContainsKey(type) == false) //first
            {
                random_list.Add(type, new List<int>());
                total += type.percent;
                key_list.Add(type);
            }
            random_list[type].Add(i);
        }

        int rand1 = Random.Range(0, total);
        int j;
        for (j = 0; j < random_list.Count; j++) 
        {
            ItemType type = key_list[j];

            if (rand1 < type.percent)
                break;
            else
                rand1 -= type.percent;

            //random_list[index]
        }

        ItemType index = key_list[j];

        int rand2 = Random.Range(0, random_list[index].Count);

        Debug.Log(random_list.Count);
        Debug.Log(random_list[index].Count);
        Debug.Log(random_list[index]);
        Debug.Log(rand2);

        int slot_index = random_list[index][rand2];

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
                        all += ItemManager.item_type_list[j].percent;
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
            //Debug.Log(ItemManager.item_type_list[j].percent);
            //Debug.Break();
            if (chance < ItemManager.item_type_list[j].percent)
            {
                //select i
                //customer.GetComponent<Customer3>.SelectItem(ItemManager.item_type_list[i].name);
                select_name = ItemManager.item_type_list[j].name;
                //Debug.Log(ItemManager.item_type_list[j].name);
                break;
            }
            else
            {
                chance -= ItemManager.item_type_list[i].percent;
            }
        }

        int chance2 = Random.Range(0, dict_list[select_name].Count);

        return dict_list[select_name][chance2];
        */
    }
}
