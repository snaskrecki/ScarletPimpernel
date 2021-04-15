using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyMovementAI moover;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        moover = GetComponent<EnemyMovementAI>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        body.velocity = moover.Move(Time.deltaTime);
    }
}
