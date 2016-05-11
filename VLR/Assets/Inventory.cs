using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public GameObject emptyItem;
    public GameObject mydefault;
    public GameObject defaultBow;
    public GameObject defaultTorch;
    public GameObject defaultHammer;
    public GameObject defaultGun;
    public GameObject defaultPotion;

    private OVRPlayerController ovrpc;
    private GameObject centerEye;

    ArrayList inventory = new ArrayList();
    int currentItemIndex;

    public int coins;

	// Use this for initialization
	void Start () {
        coins = 0;
        currentItemIndex = 0;
        inventory.Add(emptyItem);
        ovrpc = GameObject.Find("OVRPlayerVLR").GetComponent<OVRPlayerController>();
        centerEye = GameObject.Find("OVRPlayerVLR/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Y_Button") || Input.GetKeyDown(KeyCode.Tab))
        {
            ((GameObject)inventory[currentItemIndex]).SetActive(false);
            currentItemIndex = (currentItemIndex + 1) % inventory.Count;
            ((GameObject)inventory[currentItemIndex]).SetActive(true);
        }


        if (Input.GetMouseButtonUp(1) || Input.GetButtonDown("X_Button")) // pickup script
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //ray = Physics.Raycast(centerEye.transform.position, centerEye.transform.forward, out hit, 5);

            //if (Physics.Raycast(ray, out hit, 5))
            if(Physics.Raycast(centerEye.transform.position, centerEye.transform.forward, out hit, 5))
            {
                string hitTag = hit.collider.gameObject.tag;
                if (hitTag == "item" || hitTag == "item_swing" || hitTag == "shop_item")
                {
                    string hitName = hit.collider.gameObject.name;
                    if (hitTag == "shop_item")
                    {
                        int cost = int.Parse(hitName.Substring(hitName.Length - 2, 2));
                        if(cost <= coins)
                        {
                            coins -= cost;
                        } else
                        {
                            return;
                        }
                    }
                    hit.collider.gameObject.transform.parent = this.transform;
                    if (hitName.Contains("bow"))
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
                        if(hitName.Contains("boots1"))
                            ovrpc.JumpForce = 1.1f;
                        if (hitName.Contains("boots2"))
                            ovrpc.Acceleration = .15f;
                        return;
                    }
                    else if (hitName.Contains("potion"))
                    {
                        hit.collider.gameObject.transform.position = defaultPotion.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultPotion.gameObject.transform.rotation;
                        hit.collider.gameObject.tag = "inventory";
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
                else if(hitTag == "coin")
                {
                    Destroy(hit.collider.gameObject);
                    coins++;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button"))
        {
            ovrpc.Jump();
        }
    }
}
