using UnityEngine;
using System.Collections;

public class switchWeapon : MonoBehaviour
{
    private int counter;
    private string current;
    // Use this for initialization
    void Start()
    {
        counter = 0;
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
                if(child.tag == "inventory")
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
            temp += 1;
            if (temp > counter) temp = 1;
            current = temp.ToString();
        }
    }
}
