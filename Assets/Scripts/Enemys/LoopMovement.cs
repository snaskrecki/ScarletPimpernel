using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMovement : MonoBehaviour, EnemyMovementAI
{
    [SerializeField] public float speed;

    public float changeTime;

    float timePassed;

    Vector2[] directions = { Vector2.left, Vector2.up, Vector2.right, Vector2.down };

    int currDir;

    public Vector2 Move(float deltaTime)
    {
        timePassed += deltaTime;
        if(timePassed > changeTime)
        {
            timePassed = 0;
            currDir = (currDir + 1) % directions.Length;
        }
        return directions[currDir] * speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        currDir = 0;
    }

}
