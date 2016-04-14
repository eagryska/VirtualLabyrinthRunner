using UnityEngine;
using System.Collections;

public class launchArrow : MonoBehaviour {

	public int speed = 2;
    private bool fired;
    
    void Start ()
    {
        //this.transform.rotation = new Quaternion(this.transform.rotation.x + 90, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        fired = false;
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
        }
    }
}
