using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour, EnemyMovementAI
{
    public float speed;

    FollowMovementObj moover;

    public float range;

    void Awake()
    {
        moover = new FollowMovementObj(speed, transform, range);
    }

    public EnemyMovementObject GetMoover() => moover;

}
