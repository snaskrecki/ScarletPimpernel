using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerObject : EnemyShootingObject
{
    float timePassed;
    float cooldown;
    GameObject bulletPrefab;
    Transform me, target;
    float bulletSpeed;
    Animator animator;
	AudioManager audioManager;

    public ShootPlayerObject(GameObject bulletPrefab, float cooldown, Transform me, Transform target, float bulletSpeed, Animator animator)
    {
        this.cooldown = cooldown;
        this.bulletPrefab = bulletPrefab;
        this.me = me;
        this.target = target;
        this.timePassed = 0;
        this.bulletSpeed = bulletSpeed;
        this.animator = animator;
		this.audioManager = GameObject.FindObjectOfType<AudioManager>();
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
            var bullet = GameObject.Instantiate(bulletPrefab, me.position + 0.2f * Vector3.up, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().Launch(target.position - me.position, bulletSpeed);
        }
    }
}
