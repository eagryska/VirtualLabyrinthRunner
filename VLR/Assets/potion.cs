using UnityEngine;
using System.Collections;

public class potion : MonoBehaviour {

    private bool rising;
    private float height;

    public GameObject defaultPotionSwing;
    public GameObject defaultPotion;

    // Use this for initialization
    void Start () {
        height = 0;
        rising = true;

        defaultPotionSwing = GameObject.Find("OVRPlayerVLR/defaultPotionSwing");
        defaultPotion = GameObject.Find("OVRPlayerVLR/defaultPotion");
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
        if(tag == "inventory")
        {
            transform.position = Vector3.Lerp(transform.position, defaultPotionSwing.gameObject.transform.position, 10*Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultPotionSwing.gameObject.transform.rotation, 10*Time.deltaTime);
        }
        if (transform.position == defaultPotionSwing.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
