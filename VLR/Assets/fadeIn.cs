using UnityEngine;
using System.Collections;

public class fadeIn : MonoBehaviour {

    public GameObject objectToFade;

    private bool fadingIn;
    private bool done;
    private Color origColor;
    private float alpha;
	// Use this for initialization
	void Start () {
        alpha = 0;
        fadingIn = false;
        done = false;
        origColor = objectToFade.GetComponent<MeshRenderer>().material.color;
        objectToFade.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(origColor.r, origColor.g, origColor.b, alpha));
    }
	
	// Update is called once per frame
	void Update () {
        if (fadingIn && !done)
        {
            alpha += Time.deltaTime/2;
            objectToFade.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(origColor.r, origColor.g, origColor.b, alpha));
            if (alpha >= 1)
            {
                done = true;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fadingIn = true;
        }
    }
}
