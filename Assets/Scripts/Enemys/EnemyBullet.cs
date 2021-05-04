using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed;
    public GameObject targetToShoot;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = targetToShoot.transform.position - transform.position;
        moveDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
       // moveDirection = targetToShoot.transform.position - transform.position;
       // moveDirection.Normalize();
        transform.position += moveDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D obstacle)
    {
        Destroy(gameObject);
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }
}
