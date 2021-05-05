using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyMovementObject moover;
    Rigidbody2D body;
    EnemyShootingObject shooter;
    // Start is called before the first frame update
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        moover = GetComponent<EnemyMovementAI>().MakeMoover(player);
        body = GetComponent<Rigidbody2D>();
        shooter = GetComponent<EnemyShootingAI>().MakeShooter(player);

    }

    // Update is called once per frame
    void Update()
    {
        shooter.ShootDecision(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        body.velocity = moover.Move(Time.deltaTime);
    }
}
