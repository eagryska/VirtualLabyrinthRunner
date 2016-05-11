using UnityEngine;
using System.Collections;

public class conversation : MonoBehaviour {

    public string[] lines;
    public string[] exhaustedLines;
    public GameObject textBubble;

    public beachDoor doorsToOpen;
    public beachDoor doorsToClose;
    public bool triggerOpenDoor;
    public bool triggerCloseDoor;
    public bool changeMazes;
    public string mazeToDelete;
    public string mazeToGen;


    private int curLine;
    private bool talking;
    private TextMesh t;
    private bool exhausted;
    private GameManager gm;
    private bool deleted;
    // Use this for initialization
    void Start () {
        talking = false;
        exhausted = false;
        deleted = false;
        t = textBubble.GetComponent<TextMesh>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
                    if (triggerOpenDoor)
                    {
                        doorsToOpen.openDoor();
                    }
                    if (changeMazes)
                    {
                        gm.generateMaze(mazeToGen);
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
            if (triggerCloseDoor)
            {
                doorsToClose.closeDoor();
            }
            //CALL TO GAME MANAGER TO DELETE OLD MAZE
            if (changeMazes && !deleted)
            {
                gm.deleteMaze(mazeToDelete);
            }
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
