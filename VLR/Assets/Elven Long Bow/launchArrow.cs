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
        if (Input.GetKeyUp(KeyCode.Mouse0) && this.transform.parent.name == "Point")
        {
            fired = true;
            transform.parent = null;
        }
        if(fired)
        {
            this.transform.Translate(Vector3.down * speed * Time.deltaTime);
			/*RaycastHit hit;
			Ray landingRay = new Ray(this.gameObject.transform.position, this.gameObject.transform.up);
			Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.up, Color.red);
			
			if (Physics.Raycast(landingRay, out hit, 2))
			{
				if (hit.collider.tag == "item")
				{
                    Debug.Log("haha");
                    //Destroy(hit.collider.gameObject);
                    fired = false;
				}
			}*/
        }
    }

    void OnTriggerEnter(Collider other)
    {
        fired = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
