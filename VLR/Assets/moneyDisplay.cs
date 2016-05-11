using UnityEngine;
using System.Collections;

public class moneyDisplay : MonoBehaviour {

    Inventory i;
    TextMesh t;
	// Use this for initialization
	void Start () {
        t = GetComponent<TextMesh>();
        i = GameObject.Find("OVRPlayerVLR").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = i.coins.ToString() + "g";
	}
}
