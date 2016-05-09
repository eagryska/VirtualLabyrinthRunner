using UnityEngine;
using System.Collections;

public class conversation : MonoBehaviour {

    public string[] lines;
    public string[] exhaustedLines;
    public GameObject textBubble;
    public beachDoor beachDoors;
    public bool beachGob;

    private int curLine;
    private bool talking;
    private TextMesh t;
    private bool exhausted;
	// Use this for initialization
	void Start () {
        talking = false;
        exhausted = false;
        t = textBubble.GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!exhausted)
        {
            if (talking && Input.GetKeyDown(KeyCode.F))
            {
                curLine = (curLine + 1);
                if(curLine == lines.Length)
                {
                    curLine = -1;
                    exhausted = true;
                    if (beachGob)
                    {
                        beachDoors.openDoor();
                    }
                }
            }
            if (talking && !exhausted)
            {
                string newLine = lines[curLine].Replace("NEWLINE", "\n");
                t.text = newLine;
            }
        }
        if(exhausted){
            if (talking && Input.GetKeyDown(KeyCode.F))
            {
                curLine = (curLine + 1) % exhaustedLines.Length;
            }
            if (talking)
            {
                string newLine = exhaustedLines[curLine].Replace("NEWLINE", "\n");
                t.text = newLine;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            talking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            talking = false;
            curLine = 0;
            t.text = "";
        }
    }
}
