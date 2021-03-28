using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ExampleTest
    {
        [Test]

        public void MovementTest()
        {
            var gameObject = new GameObject();
            var character = gameObject.AddComponent<MainCharacterController>();
            Assert.AreEqual(
                new Vector2(1, 1),
                character.CalculateMovement(new Vector2(1, 1), 1, 1));
            Assert.AreEqual(
                new Vector2(5, 0),
                character.CalculateMovement(new Vector2(1, 0), 5, 1));
            Assert.AreEqual(
                new Vector2(5, 0),
                character.CalculateMovement(new Vector2(1, 0), 1, 5));
        }

    }
}
