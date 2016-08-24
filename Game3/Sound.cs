using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{
    public static Sound component;

    public AudioClip[] clip;

    // Use this for initialization
    void Start()
    {
        component = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
