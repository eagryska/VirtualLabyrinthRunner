using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Maze mazeBrick1;
    public Maze mazeBrick2;
    public Maze mazeMetal;
    public Maze mazeTron;

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
        mazeInstanceBrick1.setPosition(new IntVector2(20, 20));
        mazeInstanceBrick1.Generate();

        mazeInstanceBrick2 = Instantiate(mazeBrick2) as Maze;
        mazeInstanceBrick2.setPosition(new IntVector2(20, -70));
        mazeInstanceBrick2.Generate();

        mazeInstanceMetal = Instantiate(mazeMetal) as Maze;
        mazeInstanceMetal.setPosition(new IntVector2(-70, 20));
        mazeInstanceMetal.Generate();

        mazeInstanceTron = Instantiate(mazeTron) as Maze;
        mazeInstanceTron.setPosition(new IntVector2(-70, -100));
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