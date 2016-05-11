using UnityEngine;
using System.Collections;

public class enableGameObject : MonoBehaviour
{
    public GameObject[] gos;

    void Start()
    {
        foreach (GameObject g in gos)
        {
            g.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(GameObject g in gos)
            {
                g.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject g in gos)
            {
                g.SetActive(false);
            }
        }
    }
}
