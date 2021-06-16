using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DeathPlayerAnimTest
    {
        GameObject playerPrefab = (Resources.Load("Prefabs/MainCharacter") as GameObject);

        GameObject playerObject;

        float EstAnimTime = 1.2f;
        float EstTriggerTime = 0.2f;

        [SetUp]
        public void Setup()
        {
            playerObject = GameObject.Instantiate(playerPrefab);
        }

        [TearDown]
        public void Cleanup()
        {
            GameObject.Destroy(playerObject);
        }

        [UnityTest]
        public IEnumerator PlayerDoesntDieWithPositiveHP()
        {
            Damagable dam = playerObject.GetComponent<Damagable>();
            dam.maxHealth = 5;
            dam.ResetHealth();
            dam.ChangeHealth(-1);
            yield return new WaitForSeconds(EstAnimTime);
            Assert.True(playerObject != null);
        }

        [UnityTest]
        public IEnumerator PlayerCantMoveWith0HP()
        {
            Damagable dam = playerObject.GetComponent<Damagable>();
            dam.maxHealth = 5;
            dam.ResetHealth();
            dam.ChangeHealth(-10);
            yield return new WaitForSeconds(EstTriggerTime);
            Assert.False(playerObject.GetComponent<Rigidbody2D>().simulated);
        }

    }
}
