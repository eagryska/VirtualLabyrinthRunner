using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Maze mazeBrick1;
    public Maze mazeBrick2;
    public Maze mazeMetal;
    public Maze mazeTron;

    public IntVector2 mazePos1;
    public IntVector2 mazePos2;
    public IntVector2 mazePos3;
    public IntVector2 mazePos4;

    private Maze mazeInstanceBrick1;
    private Maze mazeInstanceBrick2;
    private Maze mazeInstanceMetal;
    private Maze mazeInstanceTron;

    private void Start () {
		BeginGame();
	}
	
	private void Update () {
		/*
        if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
        */
        
	}

	private void BeginGame () {
        mazeInstanceBrick1 = Instantiate(mazeBrick1) as Maze;
        mazeInstanceBrick1.setPosition(mazePos1);
        mazeInstanceBrick1.SetStartPoint(0, 1);
        mazeInstanceBrick1.SetEndPoint(20, 19);
        mazeInstanceBrick1.Generate(false, 1);
        /*
        mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
        mazeInstanceBrick2.setPosition(mazePos2);
        mazeInstanceBrick2.SetStartPoint(0, 19);
        mazeInstanceBrick2.SetEndPoint(19, 0);
        mazeInstanceBrick2.Generate(false, 2);

        mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
        mazeInstanceMetal.setPosition(mazePos3);
        mazeInstanceMetal.SetStartPoint(19, 20);
        mazeInstanceMetal.SetEndPoint(0, 1);
        mazeInstanceMetal.Generate(false, 3);

        mazeInstanceTron = Instantiate(mazeTron) as Maze;
        mazeInstanceTron.setPosition(mazePos4);
        mazeInstanceTron.SetStartPoint(20, 1);
        mazeInstanceTron.SetEndPoint(1, 20);
        mazeInstanceTron.Generate(false, 4);
        */
    }

	private void RestartGame () {
		Destroy(mazeInstanceBrick1.gameObject);
        Destroy(mazeInstanceBrick2.gameObject);
        Destroy(mazeInstanceMetal.gameObject);
        Destroy(mazeInstanceTron.gameObject);
        BeginGame();
	}

    public void regenerate(float x, float z)
    {
        if(x == 112 && z == -97)
        {
            //regen 1st
            Destroy(mazeInstanceBrick1.gameObject);
            mazeInstanceBrick1 = Instantiate(mazeBrick1) as Maze;
            mazeInstanceBrick1.setPosition(mazePos1);
            mazeInstanceBrick1.SetStartPoint(0, 1);
            mazeInstanceBrick1.SetEndPoint(20, 19);
            mazeInstanceBrick1.Generate(true, 1);

            //gen 2nd
            mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
            mazeInstanceBrick2.setPosition(mazePos2);
            mazeInstanceBrick2.SetStartPoint(0, 19);
            mazeInstanceBrick2.SetEndPoint(19, 0);
            mazeInstanceBrick2.Generate(false, 2);
        }
        else if(x == 236 && z == -97)
        {
            //destroy 1st
            Destroy(mazeInstanceBrick1.gameObject);
            //regend 2nd
            Destroy(mazeInstanceBrick2.gameObject);
            mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
            mazeInstanceBrick2.setPosition(mazePos2);
            mazeInstanceBrick2.SetStartPoint(0, 19);
            mazeInstanceBrick2.SetEndPoint(19, 0);
            mazeInstanceBrick2.Generate(true, 2);

            //gen 3rd
            mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
            mazeInstanceMetal.setPosition(mazePos3);
            mazeInstanceMetal.SetStartPoint(19, 20);
            mazeInstanceMetal.SetEndPoint(0, 1);
            mazeInstanceMetal.Generate(false, 3);
        }
        else if(x == 236 && z == -217)
        {
            //destroy 2nd
            Destroy(mazeInstanceBrick2.gameObject);
            //regen 3rd
            Destroy(mazeInstanceMetal.gameObject);
            mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
            mazeInstanceMetal.setPosition(mazePos3);
            mazeInstanceMetal.SetStartPoint(19, 20);
            mazeInstanceMetal.SetEndPoint(0, 1);
            mazeInstanceMetal.Generate(true, 3);

            //gen 4th
            mazeInstanceTron = Instantiate(mazeTron) as Maze;
            mazeInstanceTron.setPosition(mazePos4);
            mazeInstanceTron.SetStartPoint(20, 1);
            mazeInstanceTron.SetEndPoint(1, 20);
            mazeInstanceTron.Generate(false, 4);
        }
        else
        {
            //destroy 3rd
            Destroy(mazeInstanceMetal.gameObject);
            //regen 4th
            Destroy(mazeInstanceTron.gameObject);
            mazeInstanceTron = Instantiate(mazeTron) as Maze;
            mazeInstanceTron.setPosition(mazePos4);
            mazeInstanceTron.SetStartPoint(20, 1);
            mazeInstanceTron.SetEndPoint(1, 20);
            mazeInstanceTron.Generate(true, 4);
        }
    }
}