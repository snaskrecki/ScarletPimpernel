using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerDamagedByEnemyTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerDamagedAfterSomeTimePasses()
        {
            //Given
            var playerRes = Resources.Load("Prefabs/MainCharacter");
            var enemyRes = Resources.Load("Prefabs/PotworekFollow");

            //When
            GameObject player = GameObject.Instantiate(playerRes, Vector3.zero, Quaternion.identity) as GameObject;
            GameObject enemy = GameObject.Instantiate(enemyRes, Vector3.up, Quaternion.identity) as GameObject;
            Time.timeScale = 10f;
            yield return new WaitForSeconds(6);
            
            //Then
            Assert.Less(player.GetComponent<Damagable>().GetHealth(), player.GetComponent<Damagable>().maxHealth);
        }

        [TearDown]
        public void DestroyAll()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                GameObject.Destroy(o);
            }
            Time.timeScale = 1f;
        }
    }
}
