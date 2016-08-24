using UnityEngine;
using System.Collections;

public class MenuTab : MonoBehaviour
{
    public GameObject[] panel;
    public int num;

    void OnClick()
    {
        for (int i = 0; i < panel.Length; i++)
        {
            if(i==num)
                panel[i].active = true;
            else
                panel[i].active = false;
        }
    }
}
