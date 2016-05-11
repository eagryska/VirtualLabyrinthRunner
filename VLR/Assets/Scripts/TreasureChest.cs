using UnityEngine;

public class TreasureChest : MonoBehaviour {

	public IntVector2 coordinates;
    public int counter;
    public MonoBehaviour[] possibleContents;

    private int contentIndex;
    private GameManager gm; 

    public void setContentIndex(int newIndex)
    {
        contentIndex = newIndex;
    }

    // Use this for initialization
    void Start()
    {
        counter = 0;
        gm = GameObject.FindObjectOfType<GameManager>();
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
            gm = GameObject.FindObjectOfType<GameManager>();
            gm.regenerate(transform.position.x, transform.position.z, transform.rotation.eulerAngles);
            counter += 1;
        }

    }
}