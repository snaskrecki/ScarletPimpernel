using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerFactory : MonoBehaviour, EnemyShootingAI
{

    public GameObject bullet;
    public float shootCooldown;
    public float bulletSpeed;
    public Animator animator;

    public EnemyShootingObject MakeShooter(GameObject target) =>
        new ShootPlayerObject(bullet, shootCooldown, this.transform, target.transform, bulletSpeed, animator);

}
