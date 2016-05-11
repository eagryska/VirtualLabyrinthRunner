using UnityEngine;
using System.Collections;

public class boots : MonoBehaviour {

    private bool rising;
    private float height;

    // Use this for initialization
    void Start ()
    {
        rising = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (tag == "item")
        {
            Vector3 oldRot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(oldRot.x, oldRot.y + Time.deltaTime * 20, oldRot.z));
            if (rising)
            {
                height += Time.deltaTime;
                transform.position += new Vector3(0, Time.deltaTime, 0);
                if (height > 2.5f)
                {
                    rising = false;
                }
            }
        }
    }
}
