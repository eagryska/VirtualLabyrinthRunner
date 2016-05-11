using UnityEngine;
using System.Collections;

public class beachDoor : MonoBehaviour {

    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject leftHinge;
    public GameObject rightHinge;

    public bool opened;
    public bool closed;
    
    public float DoorOpenAngle;
	
	// Update is called once per frame
	void Update () {
        if(opened && DoorOpenAngle < 70)
        {
            DoorOpenAngle += Time.deltaTime * 20;
            leftDoor.transform.RotateAround(leftHinge.transform.position, Vector3.up, Time.deltaTime * -20);
            rightDoor.transform.RotateAround(rightHinge.transform.position, Vector3.up, Time.deltaTime * 20);
        }
        else if(closed && DoorOpenAngle > 0)
        {
            DoorOpenAngle -= Time.deltaTime * 60;
            leftDoor.transform.RotateAround(leftHinge.transform.position, Vector3.up, Time.deltaTime * 60);
            rightDoor.transform.RotateAround(rightHinge.transform.position, Vector3.up, Time.deltaTime * -60);
        }
    }

    public void openDoor()
    {
        if (!opened)
        {
            opened = true;
            closed = false;
        }
    }

    public void closeDoor()
    {
        if (!closed)
        {
            closed = true;
            opened = false;
        }
    }
}
