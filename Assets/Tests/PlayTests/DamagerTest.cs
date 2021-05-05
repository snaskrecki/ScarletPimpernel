using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DamagerTest
    {

        GameObject damagableObject;
        Damagable damagable;
        Rigidbody2D damagableBody;
        GameObject damagerObject;
        Damager damager;
        Rigidbody2D damagerBody;
        int testingMaxHealth = 5;
        int testingDamage = 1;
        int Big = 10000;
        Vector2 damagableStartingPosition = new Vector2 (100, 0);
        Vector2 damagerStartingPosition = new Vector2(0, 100);

        void SetupDamagable()
        {
            damagableObject = new GameObject();
            damagableBody = damagableObject.AddComponent<Rigidbody2D>();
            damagableBody.gravityScale = 0;
            damagableBody.position = damagableStartingPosition;
            damagableObject.AddComponent<BoxCollider2D>();
            damagable = damagableObject.AddComponent<Damagable>();
            damagable.maxHealth = testingMaxHealth;
            damagable.ResetHealth();
        }

        void SetupDamager()
        {
            damagerObject = new GameObject();
            damagerBody = damagerObject.AddComponent<Rigidbody2D>();
            damagerBody.gravityScale = 0;
            damagerBody.position = damagerStartingPosition;
            damagerObject.AddComponent<BoxCollider2D>();
            damager = damagerObject.AddComponent<Damager>();
            damager.damage = testingDamage;
        }

        [SetUp]
        public void Init()
        {
            SetupDamagable();
            SetupDamager();
        }

        [TearDown]
        public void Cleanup()
        {
            GameObject.Destroy(damagableObject);
            GameObject.Destroy(damagerObject);
        }

        public void Collide()
        {
            damagableBody.position = damagerBody.position = new Vector2(0, 0);
        }

        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator DamagerTestCollisionNormal()
        {
            yield return new WaitForFixedUpdate();
            Collide();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(testingMaxHealth - testingDamage, damagable.GetHealth());
        }

        [UnityTest]
        public IEnumerator DamagerTestWithBigDamage()
        {
            damager.damage = Big;
            yield return new WaitForFixedUpdate();
            Collide();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(0, damagable.GetHealth());
        }

        [UnityTest]
        public IEnumerator DamagerTestWithNegativeDamage()
        {
            damager.damage = -testingDamage;
            yield return new WaitForFixedUpdate();
            Collide();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(testingMaxHealth, damagable.GetHealth());
        }
    }
}
