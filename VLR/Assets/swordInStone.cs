using UnityEngine;
using System.Collections;

public class swordInStone : MonoBehaviour {

    public beachDoor beachDoors;
    public GameObject sword;
    public GameObject textBubble;

    private bool nearby;
    private bool taken;
    // Use this for initialization
    void Start () {
        nearby = false;
        taken = false;
        textBubble.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (nearby)
        {
            //check for taken sword
            if (sword.tag != "item")
            {
                taken = true;
            }
        }
        if (taken)
        {
            Destroy(textBubble);
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !taken)
        {
            beachDoors.closeDoor();
            nearby = true;
            //show help message
            textBubble.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nearby = false;
            //hide help message
            textBubble.SetActive(false);
        }
    }
}
