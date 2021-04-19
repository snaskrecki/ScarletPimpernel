using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovementObj : EnemyMovementObject
{
   
    private float speed;

    private float rangeToChasePlayer;

    public Transform myself;
    public Transform playerToChase;

    private bool isChasing;

    public FollowMovementObj(float speed, float range, Transform myself, Transform playerToChase)
    {
        this.speed = speed;
        this.myself = myself;
        this.rangeToChasePlayer = range;
        this.playerToChase = playerToChase;
        isChasing = false;
    }


    // Update is called once per frame
    public Vector2 Move(float deltaTime)
    {
        Vector2 moveDirection = Vector2.zero;

        if (Vector2.Distance(myself.position, playerToChase.position) < rangeToChasePlayer)
        {
            isChasing = true;
        }
        if (isChasing)
        {
            moveDirection = playerToChase.position - myself.position;
        }
        

        moveDirection.Normalize();

        return moveDirection * speed;
    }
}



