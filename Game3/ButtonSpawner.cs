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

public class ButtonSpawner
{
    static GameObject label_prefab = (GameObject)Resources.Load("Prefabs/Game3/ButtonLabel_Prefab");

    static Vector3 bt_startpos = new Vector3(-175, 115, 0);
    static Vector3 label_startpos = bt_startpos + new Vector3(65, 2.5f, 0);
    static Vector3 bt_scale = new Vector3(500, 100, 1);
    static Vector3 label_scale = new Vector3(24, 24, 1);
    static Vector3 height_gap = Vector3.up * -120;

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
        XmlNodeList desk_type_list = xmldoc.SelectNodes("root/desk_type");
        for (int i = 0; i < desk_type_list.Count; i++)
        {
            //button
            GameObject button = NGUITools.AddChild(deskpanel.gameObject, bt_prefab); //instantiate
            button.transform.localScale = bt_scale; //scale
            button.transform.localPosition = bt_startpos + height_gap * i; //position
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
            label.transform.localScale = label_scale; //scale
            label.transform.localPosition = label_startpos +  height_gap * i; //position
            label.GetComponent<UILabel>().depth = 4; //depthW
            label.GetComponent<UILabel>().text = name + " : " + price + " Gold"; //text

            //Debug.Log(node.SelectSingleNode("name").InnerText);
            //Debug.Log(node.SelectSingleNode("price").InnerText);
        }
        
        //init desk_SLOT_list
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
    public static bool OrderItemSelectButton_Init()
    {
        string filename = "Item";
        TextAsset textAsset = (TextAsset)Resources.Load("GameData/" + filename);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        //init item_TYPE_list
        XmlNodeList node_list = xmldoc.SelectNodes("root/item");
        List<ItemType> item_type_list = new List<ItemType>();
        for (int i = 0; i < node_list.Count; i++)
        {
            XmlNode node = node_list.Item(i);

            string name = node.SelectSingleNode("name").InnerText;
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            int popularity = int.Parse(node.SelectSingleNode("popularity").InnerText);
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Game3/" + name + "_Prefab");

            item_type_list.Add(new ItemType(name, price, popularity, prefab));
        }

        ItemManager.item_type_list = item_type_list;

        /* create OrderItemObjectButtons code */

        GameObject panel = MenuboardManager.component.menuboard;
        Transform itempanel = panel.transform.FindChild("Panel1");
        GameObject bt_prefab = (GameObject)Resources.Load("Prefabs/Game3/OrderButton_Prefab");
        GameObject label_prefab = (GameObject)Resources.Load("Prefabs/Game3/ButtonLabel_Prefab");
        for (int i=0;i< item_type_list.Count;i++)
        {
            //button
            GameObject button = NGUITools.AddChild(itempanel.gameObject, bt_prefab);
            button.transform.localScale = bt_scale;
            button.transform.localPosition = bt_startpos + height_gap * i;
            button.GetComponent<UISprite>().depth = 3;

            //data
            OrderItemSelectButton component = button.AddComponent<OrderItemSelectButton>();
            component.type = item_type_list[i];

            //label
            GameObject label = NGUITools.AddChild(itempanel.gameObject, label_prefab);
            label.transform.localScale = label_scale;
            label.transform.localPosition = label_startpos + height_gap * i;
            label.GetComponent<UILabel>().depth = 4;
            label.GetComponent<UILabel>().text = item_type_list[i].name + " : " + item_type_list[i].price + " Gold";
        }

        //OrderCommandButton
        GameObject com_prefab = (GameObject)Resources.Load("Prefabs/Game3/OrderCommandButton_Prefab");
        //button
        GameObject com_button = NGUITools.AddChild(itempanel.gameObject, com_prefab);
        com_button.transform.localScale = new Vector3 (200,100,1);
        com_button.transform.localPosition = new Vector3 (250,-200,0);
        com_button.GetComponent<UISprite>().depth = 3;

        //label
        GameObject com_label = NGUITools.AddChild(itempanel.gameObject, label_prefab);
        com_label.transform.localScale = new Vector3 (40,40,1);
        com_label.transform.localPosition = com_button.transform.localPosition;
        com_label.GetComponent<UILabel>().depth = 4;
        com_label.GetComponent<UILabel>().text = "Order";


        /* test
        for (int i = 0; i < ItemManager.item_type_list.Count; i++)
        {
            Debug.Log(ItemManager.item_type_list[i].name);
            Debug.Log(ItemManager.item_type_list[i].popularity);
            Debug.Log(ItemManager.item_type_list[i].price);
        }
        Debug.Break();
        */

        return true;
    }

    struct param
    {
        public GameObject panel;
        public Transform itempanel;
        public Vector3 scale;
        public Vector3 pos;
        public int control_value;
    };

    public static void PriceButtonInit()
    {
        param param;


        param.panel = MenuboardManager.component.menuboard;
        param.itempanel = param.panel.transform.FindChild("Panel1");
        param.scale = new Vector3(50, 50, 1);
        param.pos = new Vector3(250, -100, 0);
        param.control_value = 5;


        //name label
        GameObject name_label = NGUITools.AddChild(param.itempanel.gameObject, label_prefab);
        name_label.transform.localScale = new Vector3(20, 20, 1);
        name_label.transform.localPosition = param.pos + Vector3.up * 50;
        name_label.GetComponent<UILabel>().depth = 4;
        ItemPriceControl.name_label = name_label.GetComponent<UILabel>();
        ItemPriceControl.name_label.text = "";

        //percentage label
        GameObject label = NGUITools.AddChild(param.itempanel.gameObject, label_prefab);
        label.transform.localScale = new Vector3(20, 20, 1);
        label.transform.localPosition = param.pos;
        label.GetComponent<UILabel>().depth = 4;
        ItemPriceControl.price_label = label.GetComponent<UILabel>();
        ItemPriceControl.price_label.text = "";


        PriceButtonSpawn(param, "Decrease", -1);
        PriceButtonSpawn(param, "Increase", 1);
    }

    static void PriceButtonSpawn(param param, string name, int direction)
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Game3/Price_"+name+"_Prefab");
        GameObject button = NGUITools.AddChild(param.itempanel.gameObject, prefab);
        button.transform.localScale = param.scale;
        button.transform.localPosition = param.pos + Vector3.right * 100 * direction;
        button.GetComponent<UISprite>().depth = 3;
        button.AddComponent<UIButton>();
        ItemPriceControl in_component = button.AddComponent<ItemPriceControl>();
        in_component.value = param.control_value * direction;
    }
}
