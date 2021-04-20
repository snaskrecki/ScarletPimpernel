﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public bool needGeneration;
	private RoomTemplates templates;
	private int[,] map_grid;
	private const int DEFAULT_NUMBER_OF_ROOMS = 15;
	private const int DEFAULT_GRID_SIZE = 23;
	
	private const float WIDTH = 19.2F;
	private const float HEIGHT = 10.8F;
	private Vector3 bottomLeftCorner = new Vector3(-WIDTH * (DEFAULT_GRID_SIZE / 2), -HEIGHT * (DEFAULT_GRID_SIZE / 2), 0);
	
    // Start is called before the first frame update
    void Start()
    {
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        needGeneration = false;
		Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!needGeneration) return;
		
		//clean
		for(int x = 0; x < DEFAULT_GRID_SIZE; x++)
		{
			for(int y = 0; y < DEFAULT_GRID_SIZE; y++)
			{
				if(map_grid[x, y] == 0) continue;
				
				var pos = new Vector3(bottomLeftCorner.x + x * WIDTH, bottomLeftCorner.y + y * HEIGHT, 0);
				
			}
		}
		
		Generate();
		needGeneration = false;
    }
	
	void Generate()
	{
		map_grid = GenerateGraph();
		
		for(int x = 0; x < DEFAULT_GRID_SIZE; x++)
		{
			for(int y = 0; y < DEFAULT_GRID_SIZE; y++)
			{
				if(map_grid[x, y] == 0) continue;
				
				var pos = new Vector3(bottomLeftCorner.x + x * WIDTH, bottomLeftCorner.y + y * HEIGHT, 0);
				Instantiate(templates.allRooms[map_grid[x, y] - ROOM - 1], pos, Quaternion.identity);
			}
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
  	private const int ROOM = 1 << 4;

  	//types of rooms
  	//no room is 0
  	//room has 1 bit ROOM and bits for each of its doors
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

  	private int[,] GenerateGraph(int numberOfRooms = DEFAULT_NUMBER_OF_ROOMS, int grid_size = DEFAULT_GRID_SIZE)
  	{
  		int[,] grid = new int[grid_size, grid_size];
  		Tuple<int, int>[] freeNeighbour = new Tuple<int, int>[grid_size * grid_size * 4];
  		int freeNSize = 0;

  		//first room
  		grid[grid_size / 2, grid_size / 2] = ROOM;
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
  			grid[newRoomPos.Item1, newRoomPos.Item2] = ROOM;

  			//opening doors betweem rooms and adding new possible room positions
  			for(int i = 0; i < DIRECTIONS; i++)
  			{
  				var neighbour = new Tuple<int, int>(newRoomPos.Item1 + dx[i], newRoomPos.Item2 + dy[i]);

				if(neighbour.Item1 < 0 || neighbour.Item1 >= DEFAULT_GRID_SIZE || neighbour.Item2 < 0 || neighbour.Item2 >= DEFAULT_GRID_SIZE)
				{
					continue;
				}

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
}