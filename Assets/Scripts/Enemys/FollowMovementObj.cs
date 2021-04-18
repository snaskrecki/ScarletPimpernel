using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovementObj : EnemyMovementObject
{
   
    private float speed;

    public float rangeToChasePlayer;

    public Transform transform;

    private bool isChasing;

    public FollowMovementObj(float speed, Transform transform, float range)
    {
        this.speed = speed;
        this.transform = transform;
        this.rangeToChasePlayer = range;
        isChasing = false;
    }


    // Update is called once per frame
    public Vector2 Move(float deltaTime)
    {

        Vector2 moveDirection = Vector2.zero;
        if (isChasing)
        {
            moveDirection = MainCharacterController.instance.transform.position - transform.position;
        }
        else if (Vector2.Distance(transform.position, MainCharacterController.instance.transform.position) < rangeToChasePlayer)
        {
            isChasing = true;
        }

        moveDirection.Normalize();

        return moveDirection * speed;
    }
}



