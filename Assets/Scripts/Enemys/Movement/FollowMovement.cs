using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour, EnemyMovementAI
{
    public float speed;

    public float range;

    public EnemyMovementObject MakeMoover(GameObject target) =>
        new FollowMovementObj(speed, range, this.transform, target.transform);

}
