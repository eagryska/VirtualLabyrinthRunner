using UnityEngine;
using System.Collections;

public class beachDoor : MonoBehaviour {

    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject leftHinge;
    public GameObject rightHinge;

    private bool opened;

    float smooth = 2.0f;
    float DoorOpenAngle;

    private Vector3 defaultRot;
    private Vector3 openRot;

    // Use this for initialization
    void Start () {
        opened = false;
        DoorOpenAngle = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !opened)
        {
            opened = true;
        }
        if(opened && DoorOpenAngle < 70)
        {
            DoorOpenAngle += Time.deltaTime * 20;
            leftDoor.transform.RotateAround(leftHinge.transform.position, Vector3.up, Time.deltaTime * -20);
            rightDoor.transform.RotateAround(rightHinge.transform.position, Vector3.up, Time.deltaTime * 20);
        }
    }
}
