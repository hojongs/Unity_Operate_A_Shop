using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Desk_Management.Desk_Management_Init();
		Destroy(this.gameObject);
	}
}
