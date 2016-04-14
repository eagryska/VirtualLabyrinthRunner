using UnityEngine;
using System.Collections;

public class BowScript : MonoBehaviour {

	public int range = 1000;
	public LineRenderer guide = null;
	public GameObject arrow = null;

	private Vector3 startPos = Vector3.zero;
	private Vector3 direction = Vector3.zero;
	private Vector3 endPos = Vector3.zero;
	private bool isOut = false;

	// Use this for initialization
	void Start () {
		guide = this.gameObject.GetComponent<LineRenderer> ();
        startPos = this.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        /*
		startPos = this.transform.position;
		direction = this.transform.TransformDirection (Vector3.forward);
		guide.SetPosition (0, startPos);
		RaycastHit hit;
		if (Physics.Raycast (startPos, direction, out hit, range)) {
			guide.SetPosition (1, hit.point);

		}
		endPos = startPos + direction * range;
		//guide.SetPosition (1, endPos);
        */
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Mouse0) && transform.position != startPos) {
			isOut = true;
		}

		if (isOut) {
			GameObject tempArrow = (GameObject) Instantiate (arrow, startPos, Quaternion.identity);
            tempArrow.transform.parent = transform;
			tempArrow.transform.position = this.transform.position;
            tempArrow.transform.rotation = this.transform.rotation;
            tempArrow.transform.rotation *= Quaternion.Euler(270, 0, 0);
            
			isOut = false;

		}
	}

}
