using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour, EnemyMovementAI
{
    public float speed;

    public float changeTime;

    public Vector2[] directions = { Vector2.left, Vector2.up, Vector2.right, Vector2.down,
                                    Vector2.left + Vector2.up, Vector2.left + Vector2.down,
                                    Vector2.right + Vector2.up, Vector2.right + Vector2.down,
                                    Vector2.zero};

    RandomMovementObj moover;

    void Awake()
    {
        moover = new RandomMovementObj(changeTime, speed, directions);
    }

    public EnemyMovementObject GetMoover() => moover;

}
