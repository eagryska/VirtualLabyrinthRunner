using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {
    public int distance;
    public GameObject mydefault;
    public GameObject defaultBow;
    public GameObject defaultTorch;

    private int counter;
	// Use this for initialization
	void Start () {
        distance = 5;
    }
	
	// Update is called once per frame
	void Update () {
        counter = 1;
        Collect();   
	}
    void Collect()
    {
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, distance))
            {
                if(hit.collider.gameObject.tag == "item")
                {
                    Debug.Log("picked");
                    hit.collider.gameObject.transform.parent = this.transform;
                    if (hit.collider.gameObject.name == "Elven Long Bow")
                    {
                        hit.collider.gameObject.transform.position = defaultBow.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultBow.gameObject.transform.rotation;
                    } else if(hit.collider.gameObject.name == "Torch")
                    {
                        hit.collider.gameObject.transform.position = defaultTorch.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = defaultTorch.gameObject.transform.rotation;
                    } else
                    {
                        hit.collider.gameObject.transform.position = mydefault.gameObject.transform.position;
                        hit.collider.gameObject.transform.rotation = mydefault.gameObject.transform.rotation;
                    }
                    hit.collider.gameObject.active = false;
                    foreach (Transform child in transform)
                    {
                        //Debug.Log(child.tag);
                        if (child.tag == "item")
                        {
                            child.name = counter.ToString();
                            counter += 1;
                        }
                    }
                }
            }

        }
    }
}
