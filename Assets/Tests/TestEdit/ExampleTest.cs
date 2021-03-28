using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ExampleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ExampeSum()
        {
            // Use the Assert class to test conditions
            var gameObject = new GameObject();
            var character = gameObject.AddComponent<MainCharacterController>();
            Assert.AreEqual(4, character.ExampleFunction(2, 2));
            Assert.AreEqual(6, character.ExampleFunction(2, 4));
        }

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
