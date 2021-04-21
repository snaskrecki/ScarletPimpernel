using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour, EnemyMovementAI
{
    public float speed;

    FollowMovementObj moover;

    public float range;

    public GameObject toFollow;

    void OnEnable()
    {
        moover = new FollowMovementObj(speed, range, this.transform, toFollow.transform);
        moover.Move(0);
    }

    public EnemyMovementObject GetMoover() => moover;

}
