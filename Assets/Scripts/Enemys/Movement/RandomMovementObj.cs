using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementObj : EnemyMovementObject
{
    private float changeTime;

    private float timePassed;

    private float speed;

    private int currDir;

    private Vector2[] directions;

    public RandomMovementObj(float changeTime, float speed, Vector2[] directions)
    {
        this.changeTime = changeTime;
        this.speed = speed;
        this.directions = directions;

    }

    public Vector2 Move(float deltaTime)
    {
        timePassed += deltaTime;
        if (timePassed > changeTime)
        {
            timePassed = 0;
            currDir = Random.Range(0, directions.Length);
        }
        return directions[currDir].normalized * speed;
    }

}
