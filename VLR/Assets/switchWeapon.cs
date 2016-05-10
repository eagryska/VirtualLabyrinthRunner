using UnityEngine;
using System.Collections;

public class switchWeapon : MonoBehaviour
{
    public GameObject defaultSwing;
    public GameObject mydefault;
    public GameObject player;
    private GameObject myitem;
    private int counter;
    private string current;
    private string swing;
    private int flag;
    // Use this for initialization
    void Start()
    {
        flag = 0;
        counter = 0;
        swing = "1";
        current = "1";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            counter = 0;
            foreach (Transform child in transform)
            {
                //Debug.Log(child.tag);
                if(child.tag == "inventory" || child.tag == "inventory_swing")
                {
                    if(child.name == current)
                    {
                        child.gameObject.active = true;
                    }
                    else
                    {
                        child.gameObject.active = false;
                    }
                    counter += 1;
                }
            }
            //Debug.Log(current);
            int temp = int.Parse(current);
            swing = temp.ToString();
            temp += 1;
            if (temp > counter) temp = 1;
            current = temp.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            myitem = GameObject.Find(swing);
            if(myitem.tag == "inventory_swing")
                flag = 1;
            else
            {
                flag = 0;
            }
        }
        if(flag == 1)
        {
            myitem.transform.position = Vector3.Lerp(myitem.transform.position, defaultSwing.gameObject.transform.position, 10*Time.deltaTime);
            myitem.transform.rotation = Quaternion.Lerp(myitem.transform.rotation, defaultSwing.gameObject.transform.rotation, 10*Time.deltaTime);
            if(Vector3.Distance(myitem.transform.position, defaultSwing.gameObject.transform.position) < 0.05)
            {
                flag = 2;
            }
        }
        if(flag == 2)
        {
            myitem.transform.position = Vector3.Lerp(myitem.transform.position, mydefault.gameObject.transform.position, 10 * Time.deltaTime);
            myitem.transform.rotation = Quaternion.Lerp(myitem.transform.rotation, mydefault.gameObject.transform.rotation, 10 * Time.deltaTime);
        }
    }
}
