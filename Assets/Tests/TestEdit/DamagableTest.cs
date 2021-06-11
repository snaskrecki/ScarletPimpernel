using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DamagableTest
    {
        int testingMaxHealth = 5;
        int Big = 1000;
        // A Test behaves as an ordinary method
        [Test]
        public void DamagableTestResetHealth()
        {
            var g = new GameObject();
            var damagable = g.AddComponent<Damagable>();
            damagable.maxHealth = testingMaxHealth;
            damagable.ResetHealth();
            Assert.AreEqual(testingMaxHealth, damagable.GetHealth());
        }

        [Test]
        public void DamagableTestAddingHealth()
        {
            var g = new GameObject();
            var damagable = g.AddComponent<Damagable>();
            damagable.maxHealth = testingMaxHealth;
            damagable.ChangeHealth(1);
            Assert.AreEqual(1, damagable.GetHealth());
        }

        [Test]
        public void DamagableTestSubtractingHealth()
        {
            var g = new GameObject();
            var damagable = g.AddComponent<Damagable>();
            damagable.maxHealth = testingMaxHealth;
            damagable.ResetHealth();
            damagable.ChangeHealth(-1);
            Assert.AreEqual(testingMaxHealth - 1, damagable.GetHealth());
        }

        [Test]
        public void DamagableTestAddingTooMuchHealth()
        {
            var g = new GameObject();
            var damagable = g.AddComponent<Damagable>();
            damagable.maxHealth = testingMaxHealth;
            damagable.ResetHealth();
            damagable.ChangeHealth(Big);
            Assert.AreEqual(testingMaxHealth, damagable.GetHealth());
        }

        [Test]
        public void DamagableTestUpdateMaxHealth()
        {
            var g = new GameObject();
            var damagable = g.AddComponent<Damagable>();
            damagable.maxHealth = testingMaxHealth;
            damagable.ResetHealth();
            damagable.UpdateMaxHealth(Big);
            Assert.AreEqual(testingMaxHealth + Big, damagable.GetHealth());
        }
    }
}
