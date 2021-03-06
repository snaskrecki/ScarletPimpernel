using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDirectionsObject : EnemyShootingObject
{
    float timePassed;
    float cooldown;
    GameObject bulletPrefab;
    float bulletSpeed;
    Transform me;
    Vector2[] directions;
    Animator animator;

    public ShootDirectionsObject(GameObject bulletPrefab, float cooldown, float bulletSpeed, Transform me, Vector2[] directions, Animator animator)
    {
        this.cooldown = cooldown;
        this.bulletPrefab = bulletPrefab;
        this.me = me;
        this.directions = directions;
        timePassed = 0;
        this.bulletSpeed = bulletSpeed;
        this.animator = animator;
    }

    public void ShootDecision(float deltaTime)
    {
        timePassed += deltaTime;
        if (timePassed > cooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            timePassed = 0;
            foreach (var direction in directions)
            {
                var bullet = GameObject.Instantiate(bulletPrefab, me.position + 0.2f * Vector3.up, Quaternion.identity);
                bullet.GetComponent<EnemyBullet>().Launch(direction, bulletSpeed);
            }
        }
    }
}
