using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && tag == "inventory")
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 5))
            {
                if (hit.collider.gameObject.tag == "wall")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
