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
		
        if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
        
	}

	private void BeginGame () {
        mazeInstanceBrick1 = Instantiate(mazeBrick1) as Maze;
        mazeInstanceBrick1.setPosition(mazePos1);
        mazeInstanceBrick1.Generate();

        mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
        mazeInstanceBrick2.setPosition(mazePos2);
        mazeInstanceBrick2.Generate();

        mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
        mazeInstanceMetal.setPosition(mazePos3);
        mazeInstanceMetal.Generate();

        mazeInstanceTron = Instantiate(mazeTron) as Maze;
        mazeInstanceTron.setPosition(mazePos4);
        mazeInstanceTron.Generate();
    }

	private void RestartGame () {
		Destroy(mazeInstanceBrick1.gameObject);
        Destroy(mazeInstanceBrick2.gameObject);
        Destroy(mazeInstanceMetal.gameObject);
        Destroy(mazeInstanceTron.gameObject);
        BeginGame();
	}
}