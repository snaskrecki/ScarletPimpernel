using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyMovementObject moover;
    Rigidbody2D body;
    // Start is called before the first frame update
    void OnEnable()
    {
        moover = GetComponent<EnemyMovementAI>().GetMoover();
        body = GetComponent<Rigidbody2D>();
        // moover.Move(0);
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
