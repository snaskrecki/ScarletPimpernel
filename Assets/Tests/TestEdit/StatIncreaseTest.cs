using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class StatIncreaseTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void StatIncreaseTestPauseResume()
        {
            GameObject dummy = new GameObject();
            dummy.SetActive(false);

            GameObject menuObj = new GameObject();
            StatIncreaseMenu menu = menuObj.AddComponent<StatIncreaseMenu>();
            menu.statIncreaseUI = dummy;

            menu.Pause();
            Assert.AreEqual(0f, Time.timeScale);
            Assert.True(dummy.activeSelf);

            menu.Resume();
            Assert.AreEqual(1f, Time.timeScale);
            Assert.False(dummy.activeSelf);
        }

    }
}
