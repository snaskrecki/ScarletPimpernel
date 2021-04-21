using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RandomMovementTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void RandomOneDirectionTest()
        {
            Vector2[] vectors = { Vector2.up };
            const int SPEED = 5;
            RandomMovementObj follower = new RandomMovementObj(1, SPEED, vectors);
            Assert.AreEqual(Vector2.up * SPEED, follower.Move(0));
        }


        [Test]
        public void ShouldMoveUpLeftTest()
        {
            Vector2[] vectors = { Vector2.up, Vector2.left };
            const int SPEED = 10;

            RandomMovementObj follower = new RandomMovementObj(1, SPEED, vectors);
            Vector2 position = new Vector2(0, 0);
            const int DURATION = 100;
            for (int i = 0; i < DURATION; i++)
            {
                position += follower.Move(0);
            }

            Assert.IsTrue(position.x + position.y == DURATION * SPEED);
        }

        [Test]
        public void DoNotMoveBecauseZeroVectorTest()
        {
            Vector2[] vectors = { Vector2.zero };
            const int SPEED = 100;

            RandomMovementObj follower = new RandomMovementObj(1, SPEED, vectors);
            Vector2 position = new Vector2(0, 0);
            const int DURATION = 100;
            for (int i = 0; i < DURATION; i++)
            {
                position += follower.Move(0);
            }

            Assert.IsTrue(position.x + position.y == 0);
        }

        [Test]
        public void DoNotMoveBecauseZeroSpeedTest()
        {
            Vector2[] vectors = { Vector2.left, Vector2.right };
            const int SPEED = 0;

            RandomMovementObj follower = new RandomMovementObj(1, SPEED, vectors);
            Vector2 position = new Vector2(0, 0);
            const int DURATION = 100;
            for (int i = 0; i < DURATION; i++)
            {
                position += follower.Move(0);
            }

            Assert.IsTrue(position.x + position.y == 0);
        }



        [Test]
        public void MoveAllAroundTheWorldTest()
        {
            Vector2[] vectors = { Vector2.left, Vector2.up, Vector2.right, Vector2.down,
                                    Vector2.left + Vector2.up, Vector2.left + Vector2.down,
                                    Vector2.right + Vector2.up, Vector2.right + Vector2.down };
            const int SPEED = 50;

            RandomMovementObj follower = new RandomMovementObj(1, SPEED, vectors);
            Vector2 position = new Vector2(0, 0);
            const int DURATION = 100000;
            for (int i = 0; i < DURATION; i++)
            {
                Vector2 old_position = position;
                position.x += Mathf.Abs(follower.Move(0).x);
                position.y += Mathf.Abs(follower.Move(0).x);
                Assert.IsTrue(position.x + position.y > old_position.x + old_position.y);
            }
        }

        [Test]
        public void MoveAllAroundTheWorldNegativeSpeedTest()
        {
            Vector2[] vectors = { Vector2.left, Vector2.up, Vector2.right, Vector2.down,
                                    Vector2.left + Vector2.up, Vector2.left + Vector2.down,
                                    Vector2.right + Vector2.up, Vector2.right + Vector2.down };
            const int SPEED = -100;

            RandomMovementObj follower = new RandomMovementObj(1, SPEED, vectors);
            Vector2 position = new Vector2(0, 0);
            const int DURATION = 100000;
            for (int i = 0; i < DURATION; i++)
            {
                Vector2 old_position = position;
                position.x += Mathf.Abs(follower.Move(0).x);
                position.y += Mathf.Abs(follower.Move(0).x);
                Assert.IsTrue(position.x + position.y > old_position.x + old_position.y);
            }
        }
    }
}
