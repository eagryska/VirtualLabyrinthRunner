using UnityEngine;
using System.Collections;

public class launchArrow : MonoBehaviour {
    LineRenderer line;
    public int speed = 2;
    private bool fired;
    
    void Start ()
    {
        //this.transform.rotation = new Quaternion(this.transform.rotation.x + 90, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        fired = false;
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            fired = true;
            transform.parent = null;
        }
        if(fired)
        {
            this.transform.Translate(Vector3.down * speed * Time.deltaTime);
			RaycastHit hit;
			Ray landingRay = new Ray(this.transform.position, transform.forward);
			//Debug.DrawLine(this.transform.position, transform.up, Color.red);
			line.SetPosition(0, landingRay.origin);
			if (Physics.Raycast(landingRay, out hit, 2))
			{
				line.SetPosition(1, hit.point);
				if (hit.collider.tag == "wall")
				{
					Destroy(hit.transform.gameObject);
                    fired = false;
				}
			}
        }
    }
}
