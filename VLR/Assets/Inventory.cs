using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public GameObject emptyItem;
    public GameObject mydefault;
    public GameObject defaultBow;
    public GameObject defaultTorch;
    public GameObject defaultHammer;
    public GameObject defaultGun;


    ArrayList inventory = new ArrayList();
    int currentItemIndex;

	// Use this for initialization
	void Start () {
        currentItemIndex = 0;
        inventory.Add(emptyItem);
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
                    if (hit.collider.gameObject.name == "Elven Long Bow")
                    {
                        hit.collider.gameObject.transform.position = defaultBow.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultBow.gameObject.transform.rotation;
                    }
                    else if (hit.collider.gameObject.name == "Torch")
                    {
                        hit.collider.gameObject.transform.position = defaultTorch.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultTorch.gameObject.transform.rotation;
                    }
                    else if (hit.collider.gameObject.name.Contains("hammer"))
                    {
                        hit.collider.gameObject.transform.position = defaultHammer.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultHammer.gameObject.transform.rotation;
                    }
                    else if (hit.collider.gameObject.name.Contains("gun"))
                    {
                        hit.collider.gameObject.transform.position = defaultGun.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultGun.gameObject.transform.rotation;
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
    }
}
