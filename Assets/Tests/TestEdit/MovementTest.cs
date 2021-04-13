using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovementTest
    {
        [Test]
        public void TestHorizontalMovement()
        {
            var gameObject = new GameObject();
            var character = gameObject.AddComponent<MainCharacterController>();
            Assert.AreEqual(
                new Vector2(5, 0),
                character.CalculateMovement(new Vector2(1, 0), 5, 1));
        }

        [Test]
        public void TestVerticalMovement()
        {
            var gameObject = new GameObject();
            var character = gameObject.AddComponent<MainCharacterController>();
            Assert.AreEqual(
                new Vector2(0, 5),
                character.CalculateMovement(new Vector2(0, 1), 5, 1));
        }

        [Test]
        public void TestCompositeMovement()
        {
            var gameObject = new GameObject();
            var character = gameObject.AddComponent<MainCharacterController>();
            Assert.AreEqual(
                new Vector2(2, 2),
                character.CalculateMovement(new Vector2(1, 1), 1, 2));
        }

    }
}
