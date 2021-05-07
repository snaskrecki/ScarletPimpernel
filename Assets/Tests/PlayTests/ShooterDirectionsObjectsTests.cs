using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ShooterDirectionsObjectsTests
    {
        GameObject enemyObject;
        ShootDirectionsFactory enemyFactory;
        ShootDirectionsObject enemyShooter;
        GameObject bulletObject;

        float epsilon = 0.1f;

        void BulletSetup()
        {
            bulletObject = new GameObject();
            bulletObject.AddComponent<Rigidbody2D>();
            bulletObject.AddComponent<EnemyBullet>();
        }

        void FactorySetup()
        {
            enemyFactory = enemyObject.AddComponent<ShootDirectionsFactory>();
            BulletSetup();
            enemyFactory.bullet = bulletObject;
            enemyFactory.directions = new Vector2[] { Vector2.up, Vector2.down };
            enemyFactory.bulletSpeed = 1;
            enemyFactory.shootCooldown = 2;
        }

        [SetUp]
        public void Init()
        {
            enemyObject = new GameObject();
            FactorySetup();
            enemyShooter = enemyFactory.MakeShooter(null) as ShootDirectionsObject;
        }

        [TearDown]
        public void Cleanup()
        {
            GameObject.Destroy(enemyObject);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            foreach(var bullet in bullets)
            {
                GameObject.Destroy(bullet);
            }
        }

        
        /* In all test we have one bullet pre-spawned */

        [UnityTest]
        public IEnumerator ShooterDirectionsTooLittleTimeTest()
        {
            enemyShooter.ShootDecision(1);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(1, bullets.Length);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterDirectionsOneTimeTest()
        {
            enemyShooter.ShootDecision(2 + epsilon);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(3, bullets.Length);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterDirectionsManyTimesOneCallTest()
        {
            enemyShooter.ShootDecision(10);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(3, bullets.Length);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterDirectionsTwoCallsTest()
        {
            enemyShooter.ShootDecision(2 + epsilon);
            enemyShooter.ShootDecision(2 + epsilon);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(5, bullets.Length);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShooterDirectionsNoDirectionsTest()
        {
            enemyFactory.directions = new Vector2[] { };
            enemyShooter = enemyFactory.MakeShooter(null) as ShootDirectionsObject;
            enemyShooter.ShootDecision(2 + epsilon);
            enemyShooter.ShootDecision(2 + epsilon);
            var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
            Assert.AreEqual(1, bullets.Length);
            yield return null;
        }
    }
}
