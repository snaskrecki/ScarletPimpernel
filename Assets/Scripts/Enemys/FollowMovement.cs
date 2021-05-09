using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour, EnemyMovementAI
{
    public float speed;

    public float range;

    public GameObject toFollow;

    public EnemyMovementObject GetMoover() => new FollowMovementObj(speed, range, this.transform, toFollow.transform);

}
