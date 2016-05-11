using UnityEngine;
using System.Collections;

public class cutVine : MonoBehaviour {
    LineRenderer line;
    private bool flag;
    // Use this for initialization
    void Start () {
        flag = false;
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            flag = true;
        }
        RaycastHit hit;
        Ray landingRay = new Ray(this.transform.position, transform.up);
        //Debug.DrawLine(this.transform.position, transform.up, Color.red);
        line.SetPosition(0, landingRay.origin);
        if (Physics.Raycast(landingRay, out hit, 2))
        {
            if (transform.parent.tag == "Player")
            {
                //line.SetPosition(1, hit.point);
                if (hit.collider.tag == "vine" && flag == true)
                {
                    Destroy(hit.transform.gameObject);
                    flag = false;
                }
            }
        }
        flag = false;
    }
}
