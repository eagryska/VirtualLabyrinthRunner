using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour
{
    public int hitsToKill;

    private GameObject defaultSwing;
    private GameObject mydefault;

    private int numHits;

    private int swungState;

    // Use this for initialization
    void Start()
    {
        swungState = 0;
        numHits = 0;
        defaultSwing = GameObject.Find("OVRPlayerVLR/defaultSwing");
        mydefault = GameObject.Find("OVRPlayerVLR/default");
    }

    // Update is called once per frame
    void Update()
    { 
        if(defaultSwing == null)
        {
            Debug.Log("null");
            return;
        }
        if (Input.GetMouseButtonDown(0) && tag == "inventory")
        {
            cutVines();
            swungState = 1;
        }

        if (swungState == 1)
        {
            transform.position = Vector3.Lerp(transform.position, defaultSwing.gameObject.transform.position, 10 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultSwing.gameObject.transform.rotation, 10 * Time.deltaTime);
            if (Vector3.Distance(transform.position, defaultSwing.gameObject.transform.position) < 0.05)
            {
                swungState = 2;
            }
        }
        if (swungState == 2)
        {
            transform.position = Vector3.Lerp(transform.position, mydefault.gameObject.transform.position, 10 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, mydefault.gameObject.transform.rotation, 10 * Time.deltaTime);
        }

    }

    private void cutVines()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.gameObject.tag == "vine")
            {
                numHits++;
                if (numHits == hitsToKill)
                {
                    Destroy(hit.collider.gameObject);
                    numHits = 0;
                }
            }
            else
            {
                numHits = 0;
            }
        }
    }
}
