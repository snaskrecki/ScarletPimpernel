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
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == DIR_NEED_TOP_DOOR)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == DIR_NEED_LEFT_DOOR)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == DIR_NEED_RIGHT_DOOR)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
			
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
			if(!other.GetComponent<RoomSpawner>().spawned && !spawned)
			{
				Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
			}
			
			spawned = true;
        }
    }
}
