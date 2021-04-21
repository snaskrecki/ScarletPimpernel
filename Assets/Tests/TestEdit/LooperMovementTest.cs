using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LooperMovementTest
    {
		// A Test behaves as an ordinary method
        [Test]
        public void LooperMovementTestVectorUp()
        {
            Vector2[] vectors = { Vector2.up };
            LoopMovementObj looper = new LoopMovementObj(1, 1, vectors);
            Assert.AreEqual(Vector2.up, looper.Move(0));
        }

        [Test]
        public void LooperMovementTestSpeedMultiplier()
        {
            Vector2[] vectors = { Vector2.up };
            LoopMovementObj looper = new LoopMovementObj(1, 2, vectors);
            Assert.AreEqual(2 * Vector2.up, looper.Move(0));
        }

        [Test]
        public void LooperMovementTestChangeDirection()
        {
            Vector2[] vectors = { Vector2.up, Vector2.down };
            LoopMovementObj looper = new LoopMovementObj(1, 1, vectors);
            Assert.AreEqual(Vector2.down, looper.Move(1.5f));
        }

    }
}
