using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {


    private GameObject centerEye;

    // Use this for initialization
    void Start () {
        centerEye = GameObject.Find("OVRPlayerVLR/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
    }
	
	// Update is called once per frame
	void Update () {
        if (tag == "item")
        {
            Vector3 oldRot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(oldRot.x, oldRot.y + Time.deltaTime * 20, oldRot.z));
        } else if ((Input.GetMouseButtonDown(0) || Input.GetAxis("Oculus_GearVR_RIndexTrigger") > 0.3f) && tag == "inventory")
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(ray, out hit, 5))
            if (Physics.Raycast(centerEye.transform.position, centerEye.transform.forward, out hit, 5))
            {
                if (hit.collider.gameObject.tag == "wall")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
