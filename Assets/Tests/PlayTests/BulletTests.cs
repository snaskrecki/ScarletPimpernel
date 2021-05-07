using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BulletTests
    {
        GameObject bulletObject;
        Rigidbody2D bulletBody;
        EnemyBullet bullet;
        
        [SetUp]
        public void Init()
        {
            bulletObject = new GameObject();
            bulletBody = bulletObject.AddComponent<Rigidbody2D>();
            bullet = bulletObject.AddComponent<EnemyBullet>();
        }

        [TearDown]
        public void Cleanup()
        {
            GameObject.Destroy(bulletObject);
        }

        [UnityTest]
        public IEnumerator BulletTestLaunchPositiveSpeed()
        {
            bullet.Launch(Vector2.up, 1);
            Assert.AreEqual(Vector2.up, bulletBody.velocity);
            yield return null;
        }


        [UnityTest]
        public IEnumerator BulletTestLaunch0Speed()
        {
            bullet.Launch(Vector2.up, 0);
            Assert.AreEqual(Vector2.zero, bulletBody.velocity);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BulletTestLaunchNegativeSpeed()
        {
            bullet.Launch(Vector2.up, -1);
            Assert.AreEqual(Vector2.down, bulletBody.velocity);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BulletTestLaunchHighSpeed()
        {
            bullet.Launch(Vector2.up, 1000);
            Assert.AreEqual(Vector2.up * 1000, bulletBody.velocity);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BulletTestLaunchBigVectorLowSpeed()
        {
            bullet.Launch((Vector2.up + Vector2.right) * 10000, 1);
            Assert.AreEqual((Vector2.up + Vector2.right).normalized, bulletBody.velocity);
            yield return null;
        }

    }
}
