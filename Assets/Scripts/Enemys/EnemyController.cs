using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyMovementObject moover;
    Rigidbody2D body;
    public GameObject bullet;
    public Transform bulletPoint;
    public int baseFireInterval;
    public int startFireInterval;
    // Start is called before the first frame update
    void Start()
    {
        moover = GetComponent<EnemyMovementAI>().GetMoover();
        body = GetComponent<Rigidbody2D>();
        moover.Move(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (startFireInterval == 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            startFireInterval = baseFireInterval;
        }
        else
        {
            startFireInterval--;
        }

    }

    private void FixedUpdate()
    {
        body.velocity = moover.Move(Time.deltaTime);
    }
}
