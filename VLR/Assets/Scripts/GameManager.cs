using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Maze mazeBrick1;
    public Maze mazeBrick2;
    public Maze mazeMetal;
    public Maze mazeTron;

    public hammer hammerPrefab;
    public potion potionPrefab;
    public boots bootsPrefab;

    public IntVector2 mazePos1;
    public IntVector2 mazePos2;
    public IntVector2 mazePos3;
    public IntVector2 mazePos4;

    private Maze mazeInstanceBrick1;
    private Maze mazeInstanceBrick2;
    private Maze mazeInstanceMetal;
    private Maze mazeInstanceTron;

    private GameObject tunnel;
    private GameObject beachStore;
    private GameObject brickMetalTransition;
    private GameObject mazeEntranceArea;

    private void Start () {
        tunnel = GameObject.Find("Tunnel");
        beachStore = GameObject.Find("BeachStore");
        brickMetalTransition = GameObject.Find("BrickMetalTransition");
        mazeEntranceArea = GameObject.Find("MazeEntranceArea");

        brickMetalTransition.SetActive(false);
    }
	
	private void Update () {
		
        if (Input.GetKeyDown(KeyCode.M)) {
            genRest();
		}
        
        
	}

    private void genRest()
    {
        mazeInstanceBrick1 = Instantiate(mazeBrick1) as Maze;
        mazeInstanceBrick1.setPosition(mazePos1);
        mazeInstanceBrick1.SetStartPoint(0, 1);
        mazeInstanceBrick1.SetEndPoint(20, 19);
        mazeInstanceBrick1.Generate(false, 8);

        mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
        mazeInstanceBrick2.setPosition(mazePos2);
        mazeInstanceBrick2.SetStartPoint(0, 19);
        mazeInstanceBrick2.SetEndPoint(19, 0);
        mazeInstanceBrick2.Generate(false, 8);

        mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
        mazeInstanceMetal.setPosition(mazePos3);
        mazeInstanceMetal.SetStartPoint(19, 20);
        mazeInstanceMetal.SetEndPoint(0, 1);
        mazeInstanceMetal.Generate(true, 8);

        mazeInstanceTron = Instantiate(mazeTron) as Maze;
        mazeInstanceTron.setPosition(mazePos4);
        mazeInstanceTron.SetStartPoint(20, 1);
        mazeInstanceTron.SetEndPoint(1, 20);
        mazeInstanceTron.Generate(false, 8);
    }

	private void RestartGame () {
		Destroy(mazeInstanceBrick1.gameObject);
        Destroy(mazeInstanceBrick2.gameObject);
        Destroy(mazeInstanceMetal.gameObject);
        Destroy(mazeInstanceTron.gameObject);
	}

    public void regenerate(float x, float z, Vector3 v)
    {
        if(x == 112 && z == -97)
        {
            //cleanup or prep
            Destroy(tunnel);
            Destroy(beachStore);
            Destroy(mazeEntranceArea);

            //regen 1st
            Destroy(mazeInstanceBrick1.gameObject);
            mazeInstanceBrick1 = Instantiate(mazeBrick1) as Maze;
            mazeInstanceBrick1.setPosition(mazePos1);
            mazeInstanceBrick1.SetStartPoint(0, 1);
            mazeInstanceBrick1.SetEndPoint(20, 19);
            mazeInstanceBrick1.Generate(true, 2);

            //spawn reward: hammer
            v.x = 300;
            v.y = (v.y - 90) % 360;
            v.z = 90;
            createLoot<hammer>(new IntVector2(112, -97), hammerPrefab, v);
        }
        else if(x == 236 && z == -97)
        {
            //cleanup or prep
            brickMetalTransition.SetActive(true);

            //regend 2nd
            Destroy(mazeInstanceBrick2.gameObject);
            mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
            mazeInstanceBrick2.setPosition(mazePos2);
            mazeInstanceBrick2.SetStartPoint(0, 19);
            mazeInstanceBrick2.SetEndPoint(19, 0);
            mazeInstanceBrick2.Generate(true, 4);

            //spawn reward: double jump
            v.x = 0;
            v.y = (v.y - 90) % 360;
            v.z = 0;
            createLoot<boots>(new IntVector2(236, -97), bootsPrefab, v);


        }
        else if(x == 236 && z == -217)
        {
            //regen 3rd
            Destroy(mazeInstanceMetal.gameObject);
            mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
            mazeInstanceMetal.setPosition(mazePos3);
            mazeInstanceMetal.SetStartPoint(19, 20);
            mazeInstanceMetal.SetEndPoint(0, 1);
            mazeInstanceMetal.Generate(true, 6);

            //spawn reward: potion
            //boots1
            v.x = 325;
            v.y = (v.y - 90) % 360;
            v.z = 0;
            createLoot<potion>(new IntVector2(236, -217), potionPrefab, v);
        }
        else
        {
            //regen 4th
            Destroy(mazeInstanceTron.gameObject);
            mazeInstanceTron = Instantiate(mazeTron) as Maze;
            mazeInstanceTron.setPosition(mazePos4);
            mazeInstanceTron.SetStartPoint(20, 1);
            mazeInstanceTron.SetEndPoint(1, 20);
            mazeInstanceTron.Generate(true, 8);

            //spawn reward: ??
        }
    }

    private T createLoot<T>(IntVector2 coordinates, MonoBehaviour type, Vector3 rotation) where T : MonoBehaviour
    {
        T mazeObject = Instantiate(type) as T;
        (mazeObject).name = type.name + " " + coordinates.x + ", " + coordinates.z;
        mazeObject.transform.position = new Vector3(coordinates.x, -1, coordinates.z);
        mazeObject.transform.rotation = Quaternion.Euler(rotation);
        return mazeObject;
    }

    public void deleteMaze(string mazeToDelete)
    {
        if(mazeToDelete == "brick1")
        {
            Destroy(mazeInstanceBrick1.gameObject);
        }
        if(mazeToDelete == "brick2")
        {
            Destroy(mazeInstanceBrick2.gameObject);
        }
        if(mazeToDelete == "metal")
        {
            Destroy(mazeInstanceMetal.gameObject);
        }
    }

    public void generateMaze(string mazeToGen)
    {
        if (mazeToGen == "brick1")
        {
            mazeInstanceBrick1 = Instantiate(mazeBrick1) as Maze;
            mazeInstanceBrick1.setPosition(mazePos1);
            mazeInstanceBrick1.SetStartPoint(0, 1);
            mazeInstanceBrick1.SetEndPoint(20, 19);
            mazeInstanceBrick1.Generate(false, 1);
        }
        if (mazeToGen == "brick2")
        {
            mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
            mazeInstanceBrick2.setPosition(mazePos2);
            mazeInstanceBrick2.SetStartPoint(0, 19);
            mazeInstanceBrick2.SetEndPoint(19, 0);
            mazeInstanceBrick2.Generate(false, 3);
        }
        if (mazeToGen == "metal")
        {
            mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
            mazeInstanceMetal.setPosition(mazePos3);
            mazeInstanceMetal.SetStartPoint(19, 20);
            mazeInstanceMetal.SetEndPoint(0, 1);
            mazeInstanceMetal.Generate(false, 5);
        }
        if(mazeToGen == "tron")
        {
            mazeInstanceTron = Instantiate(mazeTron) as Maze;
            mazeInstanceTron.setPosition(mazePos4);
            mazeInstanceTron.SetStartPoint(20, 1);
            mazeInstanceTron.SetEndPoint(1, 20);
            mazeInstanceTron.Generate(false, 7);
        }
    }
}