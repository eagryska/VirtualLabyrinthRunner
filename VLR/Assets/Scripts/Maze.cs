using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{

    public IntVector2 mazeSize;
    public bool hasCeiling;

    public MazeWall wallPrefab;
    public MazeDoor doorPrefab;
    public MazeFloor floorPrefab;
    public MazeCeiling ceilingPrefab;
    public TreasureChest treasurePrefab;

    private WallType[,] mazeData;

    private IntVector2 mazePos;
    private IntVector2 size;
    private IntVector2 start;
    private IntVector2 doorEntrance;
    private IntVector2 door;
    private MazeDirection dir;

    //0 - floor
    //1 - loot floor
    //2 - walls
    //3 - breakable wall
    //4 - broken wall
    public Material[] materials;

    public void setPosition(IntVector2 pos)
    {
        mazePos = pos;
    }

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

        initMazePrefabs();
        initMazeData();
        createMazeData();
        addDoor();
        populateMaze();
    }

    private void initMazePrefabs()
    {
        foreach (Renderer r in wallPrefab.GetComponentsInChildren<Renderer>())
        {
            r.material = materials[2];
        }
        foreach (Renderer r in floorPrefab.GetComponentsInChildren<Renderer>())
        {
            r.material = materials[0];
        }
        foreach (Renderer r in doorPrefab.GetComponentsInChildren<Renderer>())
        {
            r.material = materials[2];
        }
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
        SetType(start, WallType.Floor);
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
                    SetType(position.x, position.z - 1, WallType.Floor);
                    position.z -= 2;
                    SetType(position, WallType.Floor);
                }
                else if (possibleDirections[move] == MazeDirection.North)
                {
                    SetType(position.x, position.z + 1, WallType.Floor);
                    position.z += 2;
                    SetType(position, WallType.Floor);
                }
                else if (possibleDirections[move] == MazeDirection.West)
                {
                    SetType(position.x - 1, position.z, WallType.Floor);
                    position.x -= 2;
                    SetType(position, WallType.Floor);
                }
                else
                {
                    SetType(position.x + 1, position.z, WallType.Floor);
                    position.x += 2;
                    SetType(position, WallType.Floor);
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
        SetType(doorEntrance, WallType.Floor);
    }

    private void populateMaze()
    {
        SetType(0, 1, WallType.Floor);
        IntVector2 pos = new IntVector2(0, 0);
        for (; pos.x < size.x; pos.x++)
        {
            pos.z = 0;
            for (; pos.z < size.z; pos.z++)
            {
                WallType type = GetType(pos);
                if (type == WallType.SolidWall || type == WallType.PermanentWall)
                {
                    createPrefab<MazeWall>(pos, dir, wallPrefab);
                }
                if (type == WallType.Door)
                {
                    createPrefab<MazeDoor>(pos, dir, doorPrefab);
                    createPrefabMaterial<MazeFloor>(pos, floorPrefab, 1);
                }
                if (type == WallType.TreasureChest)
                {
                    createPrefab<TreasureChest>(pos, dir, treasurePrefab);
                    createPrefabMaterial<MazeFloor>(pos, floorPrefab, 1);
                    if (hasCeiling)
                        createPrefab<MazeCeiling>(pos, ceilingPrefab);
                }
                if(type == WallType.LootRoom)
                {
                    createPrefabMaterial<MazeFloor>(pos, floorPrefab, 1);
                    if (hasCeiling)
                        createPrefab<MazeCeiling>(pos, ceilingPrefab);
                }
                if (type == WallType.Floor)
                {
                    createPrefab<MazeFloor>(pos, dir, floorPrefab);
                    if(hasCeiling)
                        createPrefab<MazeCeiling>(pos, ceilingPrefab);
                }
            }
        }
    }

    private T createPrefab<T>(IntVector2 coordinates, MonoBehaviour type) where T : MonoBehaviour
    {
        T mazeObject = Instantiate(type) as T;
        (mazeObject).name = type.name + " " + coordinates.x + ", " + coordinates.z;
        mazeObject.transform.parent = transform;
        mazeObject.transform.localPosition = new Vector3(coordinates.x * 4 + mazePos.x, -.2f, coordinates.z * 4 + mazePos.z);
        return mazeObject;
    }

    private T createPrefab<T>(IntVector2 coordinates, MazeDirection facing, MonoBehaviour type) where T : MonoBehaviour
    {
        T mazeObject = createPrefab<T>(coordinates, type);
        mazeObject.transform.Rotate(MazeDirections.ToRotation(facing).eulerAngles);
        return mazeObject;
    }

    private T createPrefabMaterial<T>(IntVector2 coordinates, MonoBehaviour type, int materialIndex) where T : MonoBehaviour
    {
        T mazeObject = createPrefab<T>(coordinates, type);
        mazeObject.GetComponentInChildren<Renderer>().material = materials[materialIndex];
        return mazeObject;
    }
}