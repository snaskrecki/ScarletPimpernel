using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    private const int DIR_NEED_BOTTOM_DOOR = 1;
    private const int DIR_NEED_TOP_DOOR = 2;
    private const int DIR_NEED_LEFT_DOOR = 3;
    private const int DIR_NEED_RIGHT_DOOR = 4;

	private const int DEFAULT_GRID_SIZE = 11;
	private const int DEFAULT_NUMBER_OF_ROOMS = 8;

    private RoomTemplates templates;
    private bool spawned;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        int rand;

        if (!spawned)
        {
            if (openingDirection == DIR_NEED_BOTTOM_DOOR)
            {
                rand = UnityEngine.Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == DIR_NEED_TOP_DOOR)
            {
                rand = UnityEngine.Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == DIR_NEED_LEFT_DOOR)
            {
                rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == DIR_NEED_RIGHT_DOOR)
            {
                rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
			
            spawned = true;
        }
    }
	
	//bit masks representing types of rooms
	
	int[] dx = new int[]{0, 1, 0, -1};
	int[] dy = new int[]{1, 0, -1, 0};
	const int DIRECTIONS = 4;
	
	//bits representing doors
	//2^direction gives correct door
	//example:
	//dx[0] = 0, dy[0] = 1
	//so direction 0 is "up" and 2^0 = 1 means top door  
	private const int TOP_DOOR = 1;
	private const int RIGHT_DOOR = 1 << 1;
	private const int BOTTOM_DOOR = 1 << 2;
	private const int LEFT_DOOR = 1 << 3;
	private const int CLOSED_ROOM = 1 << 4;
	
	//types of rooms
	//no room is 0
	//room has 1 bit CLOSED_ROOM and bits for each of its doors
	private const int T_ROOM = TOP_DOOR | CLOSED_ROOM;
	private const int R_ROOM = RIGHT_DOOR | CLOSED_ROOM;
	private const int TR_ROOM = TOP_DOOR | RIGHT_DOOR | CLOSED_ROOM;
	private const int B_ROOM = BOTTOM_DOOR | CLOSED_ROOM;
	private const int TB_ROOM = TOP_DOOR | BOTTOM_DOOR | CLOSED_ROOM;
	private const int RB_ROOM = RIGHT_DOOR | BOTTOM_DOOR | CLOSED_ROOM;
	private const int TRB_ROOM = TOP_DOOR | RIGHT_DOOR | BOTTOM_DOOR | CLOSED_ROOM;
	private const int L_ROOM = LEFT_DOOR | CLOSED_ROOM;
	private const int TL_ROOM = TOP_DOOR | LEFT_DOOR | CLOSED_ROOM;
	private const int RL_ROOM = RIGHT_DOOR | LEFT_DOOR | CLOSED_ROOM;
	private const int TRL_ROOM = TOP_DOOR | RIGHT_DOOR | LEFT_DOOR | CLOSED_ROOM;
	private const int BL_ROOM = BOTTOM_DOOR | LEFT_DOOR | CLOSED_ROOM;
	private const int TBL_ROOM = TOP_DOOR | BOTTOM_DOOR | LEFT_DOOR | CLOSED_ROOM;
	private const int RBL_ROOM = RIGHT_DOOR | BOTTOM_DOOR | LEFT_DOOR | CLOSED_ROOM;
	private const int TRBL_ROOM = TOP_DOOR | RIGHT_DOOR | BOTTOM_DOOR | LEFT_DOOR | CLOSED_ROOM;

	private int[,] generateGraph(int numberOfRooms = DEFAULT_NUMBER_OF_ROOMS, int grid_size = DEFAULT_GRID_SIZE)
	{
		int[,] grid = new int[grid_size, grid_size];
		Tuple<int, int>[] freeNeighbour = new Tuple<int, int>[grid_size * grid_size * 4];
		int freeNSize = 0;
		
		//first room
		grid[grid_size / 2, grid_size / 2] = CLOSED_ROOM;
		numberOfRooms--;
		
		for(int i = 0; i < DIRECTIONS; i++)
		{
			freeNeighbour[freeNSize] = new Tuple<int, int>(grid_size / 2 + dx[i], grid_size / 2 + dy[i]);
			freeNSize++;
		}
		
		//main loop
		while(numberOfRooms > 0)
		{
			//finding a random free field on a grid that neighbours with a room
			int id;
			Tuple<int, int> newRoomPos;

			do
			{
				id = UnityEngine.Random.Range(0, freeNSize);
				(freeNeighbour[id], freeNeighbour[freeNSize - 1]) = (freeNeighbour[freeNSize - 1], freeNeighbour[id]);
				newRoomPos = freeNeighbour[freeNSize - 1];
				freeNSize--;
			}
			while(grid[newRoomPos.Item1, newRoomPos.Item2] != 0);
			
			//place for new room found
			grid[newRoomPos.Item1, newRoomPos.Item2] = CLOSED_ROOM;
			
			//opening doors betweem rooms and adding new possible room positions
			for(int i = 0; i < DIRECTIONS; i++)
			{
				var neighbour = new Tuple<int, int>(newRoomPos.Item1 + dx[i], newRoomPos.Item2 + dy[i]);
				
				if(grid[neighbour.Item1, neighbour.Item2] == 0)
				{
					//i-th neighbour is a free field
					freeNeighbour[freeNSize++] = neighbour;
				}
				else
				{
					//opening door between two neighbouring
					grid[newRoomPos.Item1, newRoomPos.Item2] |= 1 << i;
					grid[neighbour.Item1, neighbour.Item2] |= 1 << ((i + 2) % 4);
				}
			}
			
			numberOfRooms--;
		}
		
		return grid;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
			if(!other.GetComponent<RoomSpawner>().spawned && !spawned)
			{
				Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
			}
			
			spawned = true;
        }
    }
}
