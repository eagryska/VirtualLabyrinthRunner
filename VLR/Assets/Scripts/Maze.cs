using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{

    public IntVector2 mazeSize;

    public MazeWall wallPrefab;
    public MazeDoor doorPrefab;
    public TreasureChest treasurePrefab;

    private WallType[,] mazeData;

    private IntVector2 size;
    private IntVector2 start;
    private IntVector2 doorEntrance;
    private IntVector2 door;
    private MazeDirection dir;

    public WallType GetType(IntVector2 coordinates)
    {
        return mazeData[coordinates.x, coordinates.z];
    }

    public void SetType(IntVector2 coordinates, WallType type)
    {
        mazeData[coordinates.x, coordinates.z] = type;
    }

    public void SetType(int x, int z, WallType type)
    {
        mazeData[x, z] = type;
    }

    public void Generate()
    {
        start = new IntVector2(1, 1);
        size = new IntVector2(mazeSize.x * 2 + 1, mazeSize.z * 2 + 1);
        door = mazeSize.clone();
        doorEntrance = mazeSize.clone();
        mazeData = new WallType[size.x, size.z];
        wallPrefab.name = "Maze Wall";
        doorPrefab.name = "Maze Door";
        treasurePrefab.name = "Treasure Chest";

        initMazeData();
        createMazeData();
        addDoor();
        populateMaze();
        
    }

    private void initMazeData()
    {
        //initialize entire array
        for(int x = 0; x < size.x; x++)
        {
            for(int z = 0; z < size.z; z++)
            {
                SetType(x, z, WallType.SolidWall);
            }
        }
        //set start
        SetType(start, WallType.Blank);
        //loot room at the center of maze
        for (int x = mazeSize.x - 2; x <= mazeSize.x + 2; x++)
        {
            for (int z = mazeSize.z - 2; z <= mazeSize.z + 2; z++)
            {
                if (x == mazeSize.x - 2 || x == mazeSize.x + 2 || z == mazeSize.z - 2 || z == mazeSize.z + 2)
                {
                    SetType(x, z, WallType.PermanentWall);
                }
                else
                {
                    SetType(x, z, WallType.LootRoom);
                }

            }
        }
        //randomly decide entrance to loot room
        dir = MazeDirections.randomEnum<MazeDirection>();
        if (dir == MazeDirection.South)
        {
            door.z -= 2;
            doorEntrance.z -= 3;
        }
        else if (dir == MazeDirection.North)
        {
            door.z += 2;
            doorEntrance.z += 3;
        }
        else if (dir == MazeDirection.West)
        {
            door.x -= 2;
            doorEntrance.x -= 3;
        }
        else
        {
            door.x += 2;
            doorEntrance.x += 3;
        }
        //add Treasure Chest
        SetType(mazeSize, WallType.TreasureChest);
    }

    private void createMazeData()
    {
        IntVector2 position = start.clone();
        Stack<IntVector2> moves = new Stack<IntVector2>();
        moves.Push(position);
        List<MazeDirection> possibleDirections = new List<MazeDirection>();
        while (moves.Count > 0)
        {
            if ((position.x - 2 >= 0) && (mazeData[position.x - 2, position.z] == WallType.SolidWall) && (position.x - 2 != 0) && (position.x - 2 != size.x - 1))
            {
                possibleDirections.Add(MazeDirection.West);
            }

            if ((position.x + 2 < size.x - 1) && (mazeData[position.x + 2, position.z] == WallType.SolidWall) && (position.x + 2 != 0))
            {
                possibleDirections.Add(MazeDirection.East);
            }

            if ((position.z - 2 >= 0) && (mazeData[position.x, position.z - 2] == WallType.SolidWall) && (position.z - 2 != 0) && (position.z - 2 != size.z - 1))
            {
                possibleDirections.Add(MazeDirection.South);
            }

            if ((position.z + 2 < size.z) && (mazeData[position.x, position.z + 2] == WallType.SolidWall) && (position.z + 2 != 0) && (position.z + 2 != size.z - 1))
            {
                possibleDirections.Add(MazeDirection.North);
            }

            if (possibleDirections.Count > 0)
            {
                int move = Random.Range(0, possibleDirections.Count);
                if (possibleDirections[move] == MazeDirection.South)
                {
                    SetType(position.x, position.z - 1, WallType.Blank);
                    position.z -= 2;
                    SetType(position, WallType.Blank);
                }
                else if (possibleDirections[move] == MazeDirection.North)
                {
                    SetType(position.x, position.z + 1, WallType.Blank);
                    position.z += 2;
                    SetType(position, WallType.Blank);
                }
                else if (possibleDirections[move] == MazeDirection.West)
                {
                    SetType(position.x - 1, position.z, WallType.Blank);
                    position.x -= 2;
                    SetType(position, WallType.Blank);
                }
                else
                {
                    SetType(position.x + 1, position.z, WallType.Blank);
                    position.x += 2;
                    SetType(position, WallType.Blank);
                }
                possibleDirections.Clear();
                moves.Push(position);
            }
            else
            {
                position = moves.Pop();
            }
        }
    }


    private void addDoor()
    {
        SetType(door, WallType.Door);
        SetType(doorEntrance, WallType.Blank);
    }

    private void populateMaze()
    {
        SetType(0, 1, WallType.Blank);
        IntVector2 pos = new IntVector2(0, 0);
        for (; pos.x < size.x; pos.x++)
        {
            pos.z = 0;
            for (; pos.z < size.z; pos.z++)
            {

                if (GetType(pos) == WallType.SolidWall || GetType(pos) == WallType.PermanentWall)
                {
                    createPrefab<MazeWall>(pos, dir, wallPrefab);
                }
                if (GetType(pos) == WallType.Door)
                {
                    createPrefab<MazeDoor>(pos, dir, doorPrefab);
                }
                if (GetType(pos) == WallType.TreasureChest)
                {
                    createPrefab<TreasureChest>(pos, dir, treasurePrefab);
                }
            }
        }
    }

    private T createPrefab<T>(IntVector2 coordinates, MonoBehaviour type) where T : MonoBehaviour
    {
        T mazeObject = Instantiate(type) as T;
        (mazeObject).name = type.name + " " + coordinates.x + ", " + coordinates.z;
        mazeObject.transform.parent = transform;
        mazeObject.transform.localPosition = new Vector3(coordinates.x * 4, 0f, coordinates.z * 4);
        return mazeObject;
    }

    private T createPrefab<T>(IntVector2 coordinates, MazeDirection facing, MonoBehaviour type) where T : MonoBehaviour
    {
        T mazeObject = createPrefab<T>(coordinates, type);
        mazeObject.transform.rotation = MazeDirections.ToRotation(facing);
        return mazeObject;
    }
}