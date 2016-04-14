using UnityEngine;

public class TreasureChest : MonoBehaviour {

	public IntVector2 coordinates;
    public int counter;

    // Use this for initialization
    void Start()
    {
        counter = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (counter > 0)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OPEN");
            GetComponent<Animation>().Play("box_open");
            counter += 1;
        }

    }
}