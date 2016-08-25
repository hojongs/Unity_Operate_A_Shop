using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Slot
{
    Vector3 pos;
    GameObject obj;

    public Slot(Vector3 pos)
    {
        this.pos = pos;
    }

    public Vector3 GetPos()
    {
        return pos;
    }
    public GameObject GetObject()
    {
        return obj;
    }
    public GameObject SetObject(GameObject param)
    {
        obj = param;
        return obj;
    }
}

public class OrderButtonSpawner
{
    public static bool OrderDeskButton_Init()
    {
        string filename = "Desk";
        TextAsset textAsset = (TextAsset)Resources.Load("GameData/" + filename);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        /* create OrderDeskObjectButtons code */
        GameObject panel = MenuboardManager.component.menuboard;
        Transform deskpanel = panel.transform.FindChild("Panel2");
        GameObject bt_prefab = (GameObject)Resources.Load("Prefabs/Game3/OrderButton_Prefab");
        GameObject label_prefab = (GameObject)Resources.Load("Prefabs/Game3/ButtonLabel_Prefab");
        XmlNodeList desk_type_list = xmldoc.SelectNodes("root/desk_type");
        Vector3 bt_startpos = new Vector3(-225, 115, 0);
        Vector3 label_startpos = bt_startpos + new Vector3(50, 2.5f, 0);
        for (int i = 0; i < desk_type_list.Count; i++)
        {
            //button
            GameObject button = NGUITools.AddChild(deskpanel.gameObject, bt_prefab); //instantiate
            button.transform.localScale = new Vector3(400, 100, 1); //scale
            button.transform.localPosition = bt_startpos + Vector3.up * (-120 * i); //position
            button.GetComponent<UISprite>().depth = 3; //depth

            //data
            XmlNode node = desk_type_list.Item(i);
            string name = node.SelectSingleNode("name").InnerText;
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            int item_count = int.Parse(node.SelectSingleNode("item_count").InnerText);
            Vector3 item_startpos = new Vector3(
                int.Parse(node.SelectSingleNode("item_position/x").InnerText),
                int.Parse(node.SelectSingleNode("item_position/y").InnerText),
                int.Parse(node.SelectSingleNode("item_position/z").InnerText)
                );
            GameObject prefab = Resources.Load("Prefabs/Game3/" + name + "_Prefab", typeof(GameObject)) as GameObject;
            OrderDeskButton component = button.AddComponent<OrderDeskButton>();
            component.init(name, price, item_count, item_startpos, prefab);
            
            //label
            GameObject label = NGUITools.AddChild(deskpanel.gameObject, label_prefab); //instantiate
            label.transform.localScale = new Vector3(32, 32, 1); //scale
            label.transform.localPosition = label_startpos + Vector3.up * (-120 * i); //position
            label.GetComponent<UILabel>().depth = 4; //depthW
            label.GetComponent<UILabel>().text = name; //text

            //Debug.Log(node.SelectSingleNode("name").InnerText);
            //Debug.Log(node.SelectSingleNode("price").InnerText);
        }
        
        //init desk_slot_list
        XmlNode desk_position = xmldoc.SelectSingleNode("root/desk_position");
        int width_count = int.Parse(desk_position.SelectSingleNode("max/width_count").InnerText);
        int height_count = int.Parse(desk_position.SelectSingleNode("max/height_count").InnerText);
        Vector3 startpos = new Vector3(
            int.Parse(desk_position.SelectSingleNode("start/x").InnerText),
            int.Parse(desk_position.SelectSingleNode("start/y").InnerText),
            int.Parse(desk_position.SelectSingleNode("start/z").InnerText)
            );
        Vector3 gappos = new Vector3(
            int.Parse(desk_position.SelectSingleNode("gap/x").InnerText),
            0,//int.Parse(desk_position.SelectSingleNode("gap/y").InnerText),
            0//int.Parse(desk_position.SelectSingleNode("gap/z").InnerText)
            );
        Slot[] desk_slot_list = new Slot[width_count * height_count];
        for (int i = 0; i < width_count; i++)
        {
            int index = i * width_count;
            for (int j = 0; j < height_count; j++)
            {
                desk_slot_list[index +j] = new Slot(startpos + gappos * j);
                //Debug.Log(deskpositionList[index]);
            }
        }

        //copy array
        DeskManager.desk_slot_list = desk_slot_list;

        return true;
    }
    public static bool OrderItemButton_Init()
    {
        string filename = "Item";
        TextAsset textAsset = (TextAsset)Resources.Load("GameData/" + filename);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        //init item_type_list
        XmlNodeList node_list = xmldoc.SelectNodes("root/item");
        List<ItemType> item_type_list = new List<ItemType>();
        for (int i = 0; i < node_list.Count; i++)
        {
            XmlNode node = node_list.Item(i);

            string name = node.SelectSingleNode("name").InnerText;
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            int percent = int.Parse(node.SelectSingleNode("percent").InnerText);
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Game3/" + name + "_Prefab");

            item_type_list.Add(new ItemType(name, price, percent, prefab));
        }

        ItemManager.item_type_list = item_type_list;

        /* create OrderItemObjectButtons code */

        GameObject panel = MenuboardManager.component.menuboard;
        Transform itempanel = panel.transform.FindChild("Panel1");
        Vector3 pos = new Vector3(-225, 115, 0);
        GameObject bt_prefab = (GameObject)Resources.Load("Prefabs/Game3/OrderButton_Prefab");
        GameObject label_prefab = (GameObject)Resources.Load("Prefabs/Game3/ButtonLabel_Prefab");

        for (int i=0;i< item_type_list.Count;i++)
        {
            //button
            GameObject button = NGUITools.AddChild(itempanel.gameObject, bt_prefab);
            button.transform.localScale = new Vector3(400, 100, 1);
            button.transform.localPosition = pos + Vector3.up * (-120 * i);
            button.GetComponent<UISprite>().depth = 3;

            //data
            OrderItemButton component = button.AddComponent<OrderItemButton>();
            component.type = item_type_list[i];

            //label
            GameObject label = NGUITools.AddChild(itempanel.gameObject, label_prefab);
            label.transform.localScale = new Vector3(32, 32, 1);
            label.transform.localPosition = pos + new Vector3(50, 2.5f, 0) + Vector3.up * (-120 * i);
            label.GetComponent<UILabel>().depth = 4;
            label.GetComponent<UILabel>().text = item_type_list[i].name;
        }

        /* test
        for (int i = 0; i < ItemManager.item_type_list.Count; i++)
        {
            Debug.Log(ItemManager.item_type_list[i].name);
            Debug.Log(ItemManager.item_type_list[i].percent);
            Debug.Log(ItemManager.item_type_list[i].price);
        }
        Debug.Break();
        */

        return true;
    }
}
