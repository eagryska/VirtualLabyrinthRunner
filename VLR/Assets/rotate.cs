using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, 20 * Time.deltaTime);
    }
}
