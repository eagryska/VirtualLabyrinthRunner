using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{

    public IntVector2 mazeSize;
    public bool hasCeiling;

    //maze core
    public MazeWall wallPrefab;
    public MazeDoor doorPrefab;
    public MazeFloor floorPrefab;
    public MazeCeiling ceilingPrefab;
    public TreasureChest treasurePrefab;

    //obstacles
    public MazeBreakable breakablePrefab;
    public MazeJumpObstacle jumpPrefab;
    public MazePushObstacle pushPrefab;
    public MazeVine vinePrefab;

    //money
    public rotate coinPrefab;

    private WallType[,] mazeData;

    private IntVector2 mazePos;
    private IntVector2 size;
    private IntVector2 start;
    private IntVector2 end;
    private IntVector2 doorEntrance;
    private IntVector2 door;
    private MazeDirection dir;
    private bool regenerating;
    private int mazeNum;

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

    public void SetStartPoint(int x, int z)
    {
        start = new IntVector2(x, z);
    }

    public void SetEndPoint(int x, int z)
    {
        end = new IntVector2(x, z);
    }

    public void Generate(bool regenerating, int mazeNum)
    {
        this.regenerating = regenerating;
        this.mazeNum = mazeNum;
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
        foreach (Renderer r in breakablePrefab.GetComponentsInChildren<Renderer>())
        {
            r.material = materials[3];
        }
        foreach (Renderer r in jumpPrefab.GetComponentsInChildren<Renderer>())
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
        //set start and end
        if(regenerating)
            SetType(end, WallType.Floor);
        else
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
        if(!regenerating)
            SetType(mazeSize, WallType.TreasureChest);
    }

    private void createMazeData()
    {
        //IntVector2 position = start.clone();
        IntVector2 position = new IntVector2(1, 1);
        SetType(position, WallType.Floor);
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
        //SetType(0, 1, WallType.Floor);
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
                    if (pos.x != doorEntrance.x && pos.z != doorEntrance.z)
                    {
                        if (rollRandom(7))
                        {
                            createPrefab<MazeVine>(pos, vinePrefab);
                            createPrefab<MazeDoor>(pos, doorPrefab);
                            randomlyAddCoin(pos, 1.5f, 10);
                        }
                        else if (mazeNum >= 2 && rollRandom(5))
                        {
                            //CALC THE ORIENTATION
                            MazeDirection facing = MazeDirection.East;
                            createPrefab<MazeBreakable>(pos, facing, breakablePrefab);
                        }
                        else if (mazeNum >= 4 && rollRandom(5))
                        {
                            createPrefab<MazeJumpObstacle>(pos, jumpPrefab);
                            randomlyAddCoin(pos, 4.5f, 10);
                        }
                        else if (mazeNum >= 6 && rollRandom(5))
                        {
                            createPrefab<MazePushObstacle>(pos, pushPrefab);
                        }
                        else
                        {
                            randomlyAddCoin(pos, 1.5f, 10);
                        }
                    }
                }
            }
        }
    }

    private bool rollRandom(int chance)
    {
        if (Random.Range(0, 100) > (100 - chance))
        {
            return true;
        }
        return false;
    }

    private bool randomlyAddCoin(IntVector2 pos, float z, int chance)
    {

        if (Random.Range(0, 100) > (100-chance))
        {
            createPrefab<rotate>(pos, z, coinPrefab);
            return true;
        }
        return false;
    }

    private T createPrefab<T>(IntVector2 coordinates, MonoBehaviour type) where T : MonoBehaviour
    {
        return createPrefab<T>(coordinates, -.195f, type);

        T mazeObject = Instantiate(type) as T;
        (mazeObject).name = type.name + " " + coordinates.x + ", " + coordinates.z;
        if (!(mazeObject is TreasureChest))
        {
            mazeObject.transform.parent = transform;
        }
        mazeObject.transform.localPosition = new Vector3(coordinates.x * 4 + mazePos.x, -.195f, coordinates.z * 4 + mazePos.z);
        return mazeObject;
    }

    private T createPrefab<T>(IntVector2 coordinates, float z, MonoBehaviour type) where T : MonoBehaviour
    {
        T mazeObject = Instantiate(type) as T;
        (mazeObject).name = type.name + " " + coordinates.x + ", " + coordinates.z;
        if (!(mazeObject is TreasureChest))
        {
            mazeObject.transform.parent = transform;
        }
        mazeObject.transform.localPosition = new Vector3(coordinates.x * 4 + mazePos.x, z, coordinates.z * 4 + mazePos.z);
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