using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMovement : MonoBehaviour, EnemyMovementAI
{
    public float speed;

    public float changeTime;

    public Vector2[] directions = { Vector2.left, Vector2.up, Vector2.right, Vector2.down };

    public EnemyMovementObject GetMoover() => new LoopMovementObj(changeTime, speed, directions);

}
