using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Object_Management.Object_Management_Init();
		Destroy(this.gameObject);
	}
}
