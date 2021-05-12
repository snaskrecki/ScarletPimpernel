using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMovementObj : EnemyMovementObject
{
    private float changeTime;

    private float timePassed;

    private float speed;

    private int currDir;

    private Vector2[] directions;

    public LoopMovementObj(float changeTime, float speed, Vector2[] directions)
    {
        this.changeTime = changeTime;
        this.speed = speed;
        this.directions = directions;

    }

    public Vector2 Move(float deltaTime)
    {
        Debug.Log("move loop");
        timePassed += deltaTime;
        if (timePassed > changeTime)
        {
            timePassed = 0;
            currDir = (currDir + 1) % directions.Length;
        }
        return directions[currDir] * speed;
    }

}
