using UnityEngine;
using System.Collections;

public class fadeOut : MonoBehaviour {

    public GameObject[] objectsToFade;
    private Color[] origColors;

    private float alpha;
    private bool fadingOut;
    private bool done;

    // Use this for initialization
    void Start () {
        fadingOut = false;
        done = false;
        alpha = 1;
        origColors = new Color[objectsToFade.Length];
        int i = 0;
        foreach (GameObject go in objectsToFade)
        {
            origColors[i] = go.GetComponent<MeshRenderer>().material.color;
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (fadingOut && !done)
        {
            alpha -= Time.deltaTime/4;
            int i = 0;
            foreach (GameObject go in objectsToFade)
            {
                Color cur = origColors[i];
                go.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(cur.r, cur.g, cur.b, alpha));
                i++;
            }
            if(alpha <= 0)
            {
                done = true;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fadingOut = true;
        }
    }
}
