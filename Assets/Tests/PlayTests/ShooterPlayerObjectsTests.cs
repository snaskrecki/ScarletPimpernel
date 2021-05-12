using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ShooterPlayerObjectsTests
    {
        GameObject enemyObject;
        ShootPlayerFactory enemyFactory;
        ShootPlayerObject enemyShooter;
        GameObject bulletObject;
        GameObject player;

        float epsilon = 0.1f;

        Vector2 playerPosition = Vector2.up;
        Vector2 enemyPosition = Vector2.zero;

        float bulletSpeed = 1;
        float cooldown = 2;

        Vector2 TestVelocity()
        {
            return (playerPosition - enemyPosition).normalized * bulletSpeed;
        }

        void BulletSetup()
        {
            bulletObject = new GameObject();
            var bulletBody = bulletObject.AddComponent<Rigidbody2D>();
            bulletBody.gravityScale = 0;
            bulletBody.velocity = TestVelocity();
            bulletObject.AddComponent<EnemyBullet>();
        }

        void FactorySetup()
        {
            enemyFactory = enemyObject.AddComponent<ShootPlayerFactory>();
            BulletSetup();
            enemyFactory.bullet = bulletObject;
            enemyFactory.bulletSpeed = bulletSpeed;
            enemyFactory.shootCooldown = cooldown;
        }

        [SetUp]
        public void Init()
        {
            enemyObject = new GameObject();
            enemyObject.transform.position = enemyPosition;
            player = new GameObject();
            player.transform.position = playerPosition;
            FactorySetup();
            enemyShooter = enemyFactory.MakeShooter(player) as ShootPlayerObject;
        }

        [TearDown]
        public void Cleanup()
        {
            GameObject.Destroy(player);
            GameObject.Destroy(enemyObject);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            foreach (var bullet in bullets)
            {
                GameObject.Destroy(bullet);
            }
        }



        /* In all test we have one bullet pre-spawned */

        void CheckBulletsVelocity(EnemyBullet[] bullets)
        {
            foreach (var bullet in bullets)
            {
                Assert.AreEqual(TestVelocity(),
                    bullet.GetComponent<Rigidbody2D>().velocity);
            }
        }

        [UnityTest]
        public IEnumerator ShooterPlayerTooLittleTimeTest()
        {
            enemyShooter.ShootDecision(1);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(1, bullets.Length);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterPlayerOneTimeTest()
        {
            enemyShooter.ShootDecision(2 + epsilon);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(2, bullets.Length);
            CheckBulletsVelocity(bullets);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterPlayerManyTimesOneCallTest()
        {
            enemyShooter.ShootDecision(10);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(2, bullets.Length);
            CheckBulletsVelocity(bullets);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterPlayerTwoCallsTest()
        {
            enemyShooter.ShootDecision(2 + epsilon);
            enemyShooter.ShootDecision(2 + epsilon);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(3, bullets.Length);
            CheckBulletsVelocity(bullets);
            yield return null;
        }
    }
}
