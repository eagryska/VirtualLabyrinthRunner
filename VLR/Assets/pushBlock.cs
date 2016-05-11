using UnityEngine;
using System.Collections;

public class pushBlock : MonoBehaviour {

 void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.position.z == transform.parent.position.z)
            {
                if(transform.position.x < transform.parent.position.x)
                    transform.parent.position += new Vector3(Time.deltaTime, 0, 0);
                else
                    transform.parent.position -= new Vector3(Time.deltaTime, 0, 0);

            }
            else
            {
                if (transform.position.z < transform.parent.position.z)
                    transform.parent.position += new Vector3(0, 0, Time.deltaTime);
                else
                    transform.parent.position -= new Vector3(0, 0, Time.deltaTime);
            }
        }

    }
}
