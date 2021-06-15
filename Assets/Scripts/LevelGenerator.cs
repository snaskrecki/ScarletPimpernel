using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static bool needGeneration;
    private static RoomTemplates templates;
    private static EnemysTemplates enemys_templates;
    private static ObjectTemplates objects_templates;
    private static int[,] map_grid;
    private const int DEFAULT_NUMBER_OF_ROOMS = 8;
    private const int DEFAULT_GRID_SIZE = 13;
    public static int DEFAULT_NUMBER_OF_ENEMYS = 3;
    public static int DEFAULT_NUMBER_OF_OBJECTS = 4;
    private int MAX_ENEMY_HEALTH = 2;
    private int UPGRADE_LEVEL = 5; // some statistics are not updated during each generation
    private int level_number;
    public static int enemyList_length;

    private const float WIDTH = 19.2F;
    private const float HEIGHT = 10.8F;
    private const int DOOR_ROOM = 1 << 6;

    private static Vector3 bottomLeftCorner = new Vector3(-WIDTH * (DEFAULT_GRID_SIZE / 2), -HEIGHT * (DEFAULT_GRID_SIZE / 2), 0);

    private ArrayList objectsList;
    private ArrayList enemysList;
    private GameObject door;
    private GameObject player;
    private StatIncreaseMenu statIncreaseMenu;

    void FindObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        enemys_templates = GameObject.FindGameObjectWithTag("Enemys").GetComponent<EnemysTemplates>();
        objects_templates = GameObject.FindGameObjectWithTag("Objects").GetComponent<ObjectTemplates>();
        door = GameObject.FindGameObjectWithTag("Door");
        statIncreaseMenu = GameObject.FindGameObjectWithTag("StatIncreaseMenu").GetComponent<StatIncreaseMenu>();
        enemyList_length = enemys_templates.allEnemys.Length;
    }

    // Start is called before the first frame update
    void Start()
    {

        FindObjects();

        needGeneration = false;
        objectsList = new ArrayList();
        enemysList = new ArrayList();

        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!needGeneration) return;

        //cleaning
        foreach (UnityEngine.Object obj in enemysList)
        {
            Destroy(obj);
        }

        enemysList.Clear();

        foreach (UnityEngine.Object obj in objectsList)
        {
            Destroy(obj);
        }

        objectsList.Clear();

        MAX_ENEMY_HEALTH += 1;

        if (level_number % UPGRADE_LEVEL == 0)
        {
            DEFAULT_NUMBER_OF_ENEMYS += 2;
            DEFAULT_NUMBER_OF_OBJECTS += 2;
            player.GetComponent<PlayerCombat>().attackDamage++;
        }

        statIncreaseMenu.Pause();
        Generate();
        needGeneration = false;
    }

    private Vector3 newRandomPosition(Vector3 zero_position)
    {
      float percent = 0.9f;
      // we return a position inside the rooom; we exclude 10% of each dimension in order to avoid walls
      return new Vector3(zero_position[0] + UnityEngine.Random.Range(percent * (WIDTH / (-2)), percent * (WIDTH / 2)),
      zero_position[1] + UnityEngine.Random.Range(percent * (HEIGHT / (-2)), percent * (HEIGHT / 2)), zero_position[2]);
    }

    void generateNewEnemy(Vector3 zero_position)
    {
        int index = UnityEngine.Random.Range(0, enemyList_length);
        Vector3 new_pos = newRandomPosition(zero_position);
        Debug.Log(new_pos);
        UnityEngine.GameObject new_enemy = Instantiate(enemys_templates.allEnemys[index], zero_position, Quaternion.identity);
        new_enemy.GetComponent<Damagable>().UpdateMaxHealth(UnityEngine.Random.Range(MAX_ENEMY_HEALTH / 2, MAX_ENEMY_HEALTH));
        enemysList.Add(new_enemy);
    }

    void generateNewObject(Vector3 zero_position)
    {
        int index = UnityEngine.Random.Range(0, objects_templates.allObjects.Length);

        Vector3 new_pos =  newRandomPosition(zero_position);
        Debug.Log("Object");
        Debug.Log(new_pos);
        UnityEngine.GameObject new_object = Instantiate(objects_templates.allObjects[index], zero_position, Quaternion.identity);
        objectsList.Add(new_object);
    }

    void Generate()
    {
        map_grid = GenerateGraph();
        player.transform.position = new Vector3(0, 0, 0);

        for (int x = 0; x < DEFAULT_GRID_SIZE; x++)
        {
            for (int y = 0; y < DEFAULT_GRID_SIZE; y++)
            {
                InstantiateRoom(map_grid[x, y], x, y);
            }
        }
        level_number++;
    }

    void InstantiateRoom(int room, int x, int y)
    {
        if (room == 0) return;

        int type = room - ROOM - 1;
        int current_number_of_enemys = UnityEngine.Random.Range(1, DEFAULT_NUMBER_OF_ENEMYS);
        int current_number_of_objects = UnityEngine.Random.Range(1, DEFAULT_NUMBER_OF_OBJECTS);

        var pos = new Vector3(bottomLeftCorner.x + x * WIDTH, bottomLeftCorner.y + y * HEIGHT, 0);

        if ((room & DOOR_ROOM) != 0)
        {
            SpawnDoor(pos);
            type ^= DOOR_ROOM;

            // more enemies in the last room
            current_number_of_enemys *= 2;
        }

        UnityEngine.Object obj = Instantiate(templates.allRooms[type], pos, Quaternion.identity);
        objectsList.Add(obj);

        for (int i = 0; i < current_number_of_enemys; i++)
        {
            generateNewEnemy(pos);
        }

        for (int i = 0; i < current_number_of_objects; i++)
        {
            generateNewObject(RandomizePosition(pos));
        }
    }

    // bit masks representing types of rooms

    static int[] dx = new int[] { 0, 1, 0, -1 };
    static int[] dy = new int[] { 1, 0, -1, 0 };
    const int DIRECTIONS = 4;

	Vector3 RandomizePosition(Vector3 pos)
	{
		int r = UnityEngine.Random.Range(0, DIRECTIONS - 1);
		float newX = pos.x + ((float) dx[r] * WIDTH) / 4;
		float newY = pos.y + ((float) dy[r] * HEIGHT) / 4;

		r = UnityEngine.Random.Range(0, DIRECTIONS - 1);
		int eps = UnityEngine.Random.Range(0, (int) HEIGHT / 3);
		newX += dx[r] * eps;
		newY += dy[r] * eps;

		return new Vector3(newX, newY);
	}

    // bits representing doors
    // 2^direction gives correct door
    // example:
    // dx[0] = 0, dy[0] = 1
    // so direction 0 is "up" and 2^0 = 1 means top door
    private const int TOP_DOOR = 1;
    private const int RIGHT_DOOR = 1 << 1;
    private const int BOTTOM_DOOR = 1 << 2;
    private const int LEFT_DOOR = 1 << 3;
    private const int ROOM = 1 << 4;

    private const int EMPTY_ROOM = 15;

    // types of rooms
    // no room is 0
    // room has 1 bit ROOM and bits for each of its doors
    private const int T_ROOM = TOP_DOOR | ROOM;
    private const int R_ROOM = RIGHT_DOOR | ROOM;
    private const int TR_ROOM = TOP_DOOR | RIGHT_DOOR | ROOM;
    private const int B_ROOM = BOTTOM_DOOR | ROOM;
    private const int TB_ROOM = TOP_DOOR | BOTTOM_DOOR | ROOM;
    private const int RB_ROOM = RIGHT_DOOR | BOTTOM_DOOR | ROOM;
    private const int TRB_ROOM = TOP_DOOR | RIGHT_DOOR | BOTTOM_DOOR | ROOM;
    private const int L_ROOM = LEFT_DOOR | ROOM;
    private const int TL_ROOM = TOP_DOOR | LEFT_DOOR | ROOM;
    private const int RL_ROOM = RIGHT_DOOR | LEFT_DOOR | ROOM;
    private const int TRL_ROOM = TOP_DOOR | RIGHT_DOOR | LEFT_DOOR | ROOM;
    private const int BL_ROOM = BOTTOM_DOOR | LEFT_DOOR | ROOM;
    private const int TBL_ROOM = TOP_DOOR | BOTTOM_DOOR | LEFT_DOOR | ROOM;
    private const int RBL_ROOM = RIGHT_DOOR | BOTTOM_DOOR | LEFT_DOOR | ROOM;
    private const int TRBL_ROOM = TOP_DOOR | RIGHT_DOOR | BOTTOM_DOOR | LEFT_DOOR | ROOM;

    public static int[,] GenerateGraph(int numberOfRooms = DEFAULT_NUMBER_OF_ROOMS, int grid_size = DEFAULT_GRID_SIZE)
    {
        int[,] grid = new int[grid_size, grid_size];
        Tuple<int, int>[] freeNeighbour = new Tuple<int, int>[grid_size * grid_size * 4];
        int freeNSize = 0;

        //first room
        grid[grid_size / 2, grid_size / 2] = ROOM;
        numberOfRooms--;

        for (int i = 0; i < DIRECTIONS; i++)
        {
            freeNeighbour[freeNSize] = new Tuple<int, int>(grid_size / 2 + dx[i], grid_size / 2 + dy[i]);
            freeNSize++;
        }

        /* lower numbers will mean door room will be generated later,
        *  which means heuristically door room will be further from spawn room
        */
        int door_room_id = UnityEngine.Random.Range(1, numberOfRooms / 2);

        // main loop
        while (numberOfRooms > 0)
        {
            var newRoomPos = GenerateRoom(grid, grid_size, freeNeighbour, ref freeNSize);

            if (numberOfRooms == door_room_id)
            {
                grid[newRoomPos.Item1, newRoomPos.Item2] |= DOOR_ROOM;
            }

            numberOfRooms--;
        }

        return grid;
    }

    // generates new room in a free field
    private static Tuple<int, int> GenerateRoom(int[,] grid, int grid_size, Tuple<int, int>[] freeNeighbour, ref int freeNSize)
    {
        Tuple<int, int> newRoomPos = GetNewRoomPosition(grid, freeNeighbour, ref freeNSize);
        grid[newRoomPos.Item1, newRoomPos.Item2] = ROOM;

        //opening doors betweem rooms and adding new possible room positions
        for (int i = 0; i < DIRECTIONS; i++)
        {
            var neighbour = new Tuple<int, int>(newRoomPos.Item1 + dx[i], newRoomPos.Item2 + dy[i]);

            if (!InBounds(neighbour, grid_size)) continue;

            if (grid[neighbour.Item1, neighbour.Item2] == 0)
            {
                // i-th neighbour is a free field
                freeNeighbour[freeNSize] = neighbour;
                freeNSize++;
            }
            else
            {
                // opening door between two neighbouring
                grid[newRoomPos.Item1, newRoomPos.Item2] |= 1 << i;
                grid[neighbour.Item1, neighbour.Item2] |= 1 << ((i + 2) % 4);
            }
        }

        return newRoomPos;
    }

    // finding a random free field on a grid that neighbours with a room
    private static Tuple<int, int> GetNewRoomPosition(int[,] grid, Tuple<int, int>[] freeNeighbour, ref int freeNSize)
    {
        int id;
        Tuple<int, int> newRoomPos;

        do
        {
            id = UnityEngine.Random.Range(0, freeNSize);
            (freeNeighbour[id], freeNeighbour[freeNSize - 1]) = (freeNeighbour[freeNSize - 1], freeNeighbour[id]);
            newRoomPos = freeNeighbour[freeNSize - 1];
            freeNSize--;
        }
        while (grid[newRoomPos.Item1, newRoomPos.Item2] != 0);

        return newRoomPos;
    }

    public static bool InBounds(Tuple<int, int> point, int side)
    {
        return 0 <= point.Item1 && point.Item1 < side && 0 <= point.Item2 && point.Item2 < side;
    }

    private void SpawnDoor(Vector3 position)
    {
        door.transform.position = position;
    }
}
