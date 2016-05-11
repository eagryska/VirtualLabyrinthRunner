using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public GameObject emptyItem;
    public GameObject mydefault;
    public GameObject defaultBow;
    public GameObject defaultTorch;
    public GameObject defaultHammer;
    public GameObject defaultGun;

    private OVRPlayerController ovrpc;

    ArrayList inventory = new ArrayList();
    int currentItemIndex;

	// Use this for initialization
	void Start () {
        currentItemIndex = 0;
        inventory.Add(emptyItem);
        ovrpc = GameObject.Find("OVRPlayerVLR").GetComponent<OVRPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ((GameObject)inventory[currentItemIndex]).SetActive(false);
            currentItemIndex = (currentItemIndex + 1) % inventory.Count;
            ((GameObject)inventory[currentItemIndex]).SetActive(true);
        }

        if (Input.GetMouseButtonUp(1)) // pickup script
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 5))
            {
                if (hit.collider.gameObject.tag == "item" || hit.collider.gameObject.tag == "item_swing")
                {
                    Debug.Log("picked");
                    hit.collider.gameObject.transform.parent = this.transform;
                    string hitName = hit.collider.gameObject.name;
                    if (hitName == "Elven Long Bow")
                    {
                        hit.collider.gameObject.transform.position = defaultBow.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultBow.gameObject.transform.rotation;
                    }
                    else if (hitName == "Torch")
                    {
                        hit.collider.gameObject.transform.position = defaultTorch.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultTorch.gameObject.transform.rotation;
                    }
                    else if (hitName.Contains("hammer"))
                    {
                        hit.collider.gameObject.transform.position = defaultHammer.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultHammer.gameObject.transform.rotation;
                    }
                    else if (hitName.Contains("gun"))
                    {
                        hit.collider.gameObject.transform.position = defaultGun.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultGun.gameObject.transform.rotation;
                    }
                    else if (hitName.Contains("boots"))
                    {
                        Destroy(hit.collider.gameObject);
                        //do boot stuff
                        if(hitName == "boots1")
                            ovrpc.JumpForce = 1.1f;
                        if (hitName == "boots2")
                            ovrpc.Acceleration = .15f;
                        return;
                    }
                    else
                    {
                        hit.collider.gameObject.transform.position = mydefault.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = mydefault.gameObject.transform.rotation;
                    }
                    hit.collider.gameObject.tag = "inventory";
                    hit.collider.gameObject.SetActive(false);
                    inventory.Add(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("OVRPlayerVLR").GetComponent<OVRPlayerController>().Jump();
        }
    }
}
