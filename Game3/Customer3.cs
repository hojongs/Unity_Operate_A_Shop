using UnityEngine;
using System.Collections;
using System;

public class Customer3 : MonoBehaviour
{
    public int state;

    private bool move_init;

    private Vector3 src_pos;
    private Vector3 dst_pos;
    Vector3 exit_pos;
    int height = 9;

    private float start_time;

    public int type;
    public Slot item;
    private float t;
    private float distance_time;

    // Use this for initialization
    void Start()
    {
        if (type != 0 && item.GetObject() == null)
            Destroy(this.gameObject);

        height = Mathf.FloorToInt(this.transform.position.y);
        distance_time = 30;
        GetComponent<Animation>().Play("Walk");
        move_init = false;
        exit_pos = new Vector3(100, height, 100);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(item);
        //Debug.Log(state);

        switch (state)
        {
            case 0: //go to the desk
                {
                    Move();
                    CheckItem();
                    break;
                }
            case 1: //arrived to the desk
                {
                    if (type != 0)
                    {
                        buy();
                    }
                    break;
                }
            case 2:
                turn();
                break;
            case 3: //go to the exit
                {
                    Move();
                    if(type != 0)
                        ShowText();
                    break;
                }
            case 4:
                {
                    Exit();
                    break;
                }
        }
    }

    void Move()
    {
        if (move_init == false)
        {
            start_time = Time.time;
            src_pos = this.transform.position;
            switch (state)
            {
                case 0: //go to the item
                    {
                        GameObject obj = item.GetObject();
                        if (obj == null) // same time (move_init, sold out)
                            return;
                        Transform tr = obj.transform;
                        //dst_pos = Order.GetDeskspace(desk_pos).desk_obj.transform.position + new Vector3 (0,0.5f,2);//desk;
                        dst_pos = tr.position + tr.forward * 12;
                        dst_pos.y = height;
                        //Debug.Log (dst_pos);
                        break;
                    }
                case 3: //go to the exit
                    {
                        //dst_pos = new Vector3 (50,9,5);//exit;
                        dst_pos = exit_pos;
                        break;
                    }
            }
            move_init = true;
        }
        else
        {
            t = ((Time.time - start_time) * distance_time) / Vector3.Distance(src_pos, dst_pos);
            //print(t);
            //print (src_pos);
            //print (dst_pos);
            //Debug.Break ();
            this.transform.position = Vector3.Lerp(src_pos, dst_pos, t);
            this.transform.rotation = Quaternion.LookRotation(dst_pos - src_pos);
            this.transform.Rotate(new Vector3(0, 90, 0));
            if (t >= 1)
            {
                move_init = false;
                state++;
                //print(state);
                //Debug.Break();
            }
        }
    }

    void CheckItem()
    {
        GameObject obj = item.GetObject();
        if (obj == null)
        {
            move_init = false;

            SetText("Oops! There is not the item.");
            GetComponent<Animation>().Play("Oops");

            state = 2;
        }
    }

    private bool buy()
    {
        bool result;

        GameObject obj = item.GetObject();

        if (obj != null)
        {
            ItemType type = obj.GetComponent<ItemManager>().type;
            float percent = 100 + type.profit;
            //Debug.Log(type.profit);
            int price = Mathf.FloorToInt(type.price * percent*0.01f);
            MoneyManager.AddMoney(price);
            MoneyManager.sales += price;
            ItemManager.item_total_count--;
            Destroy(obj);

            SetText("Thank you!");
            GetComponent<Animation>().Play("Walk");
            AudioSource audio = Sound.component.GetComponent<AudioSource>();
            audio.clip = Sound.component.clip[2];
            audio.Play();

            result = true;
        }
        else result = false;


        state = 2;

        return result;
    }

    void SetText(string text)
    {
        GUIText gui_text = this.gameObject.GetComponentInChildren<GUIText>();

        gui_text.text = text;
    }
    void ShowText()
    {
        //print(this.gameObject.GetComponentInChildren<Renderer>());
        //Debug.Break ();
        if (this.gameObject.GetComponentInChildren<Renderer>().isVisible)
        {
            GUIText gui_text = this.gameObject.GetComponentInChildren<GUIText>();

            Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position + Vector3.up * 15);

            //pos.y += 0.2f;

            gui_text.transform.position = pos;
        }
    }

    private void turn()
    {
        state++;
    }

    void Exit()
    {
        //Customer_Spawn2.customer_count--;
        Destroy(this.gameObject);
    }
}
