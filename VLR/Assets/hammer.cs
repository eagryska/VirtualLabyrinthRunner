using UnityEngine;
using System.Collections;

public class hammer : MonoBehaviour
{

    private float swingAngle;
    private bool swinging;
    private bool backSwing;
    private int numHits;

    public MazeBroken brokenPrefab;

    // Use this for initialization
    void Start()
    {
        swingAngle = 0.0f;
        swinging = false;
        backSwing = false;
        numHits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && tag == "inventory")
        {
            swinging = true;
            backSwing = false;
            breakWall();
        }

        if (swinging)
        {
            if (swingAngle < 40 && !backSwing)
            {
                swingAngle += Time.deltaTime * 200;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + Time.deltaTime * 200, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
            }
            else
            {
                backSwing = true;
                swingAngle -= Time.deltaTime * 100;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + Time.deltaTime * -100, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
            }
            if (swingAngle <= 0)
            {
                swinging = false;
            }
        }

    }

    private void breakWall()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.gameObject.tag == "breakableWall")
            {
                numHits++;
                if(numHits == 3)
                {
                    Transform t = hit.transform;
                    Material m = t.GetComponent<Renderer>().material;
                    MazeBroken mazeObject = Instantiate(brokenPrefab) as MazeBroken;
                    (mazeObject).name = brokenPrefab.name + " " + t.position.x + ", " + t.position.z;
                    mazeObject.transform.rotation = t.parent.rotation;
                    mazeObject.transform.position = t.parent.position;
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                numHits = 0;
            }
        }
    }
}
