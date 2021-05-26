using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDirectionsFactory : MonoBehaviour, EnemyShootingAI
{
    public GameObject bullet;
    public float shootCooldown;
    public float bulletSpeed;
    public Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public Animator animator;

    public EnemyShootingObject MakeShooter(GameObject target) =>
        new ShootDirectionsObject(bullet, shootCooldown, bulletSpeed, this.transform, directions, animator);
}
