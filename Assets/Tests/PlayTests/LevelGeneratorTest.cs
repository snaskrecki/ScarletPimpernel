using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class LevelGeneratorTest
    {
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("MainScene");
            Time.timeScale = 10f;
        }

        [UnityTest]
        public IEnumerator IsEverythingImportantGenerated()
        {
            yield return new WaitForSeconds(3);

            Assert.IsNotEmpty(GameObject.FindGameObjectsWithTag("Door"));
            Assert.IsNotEmpty(GameObject.FindGameObjectsWithTag("Player"));
            Assert.IsNotEmpty(GameObject.FindGameObjectsWithTag("Room"));
        }
        
        [TearDown]
        public void Cleanup()
        {
            Time.timeScale = 0f;
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                GameObject.Destroy(o);
            }
            Time.timeScale = 1f;
        } 
    }
}
