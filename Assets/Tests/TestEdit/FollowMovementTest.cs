using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FollowMovementTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void InRangeSpeed1Test()
        {
            GameObject g1 = new GameObject();
            GameObject g2 = new GameObject();

            Transform me = g1.transform;
            Transform player = g2.transform;
            me.position = new Vector3(0, 0, 0);
            player.position = new Vector3(1, 1, 0);

            FollowMovementObj follower = new FollowMovementObj(1, 5, me, player);
            Assert.AreEqual(new Vector2(1, 1).normalized, follower.Move(0));
        }

        [Test]
        public void InRangeSpeed3Test()
        {
            GameObject g1 = new GameObject();
            GameObject g2 = new GameObject();

            Transform me = g1.transform;
            Transform player = g2.transform;
            me.position = new Vector3(0, 0, 0);
            player.position = new Vector3(1, 1, 0);

            FollowMovementObj follower = new FollowMovementObj(3, 5, me, player);
            Assert.AreEqual(new Vector2(1, 1).normalized * 3, follower.Move(0));
        }

        [Test]
        public void OutRangeTest()
        {
            GameObject g1 = new GameObject();
            GameObject g2 = new GameObject();

            Transform me = g1.transform;
            Transform player = g2.transform;
            me.position = new Vector3(0, 0, 0);
            player.position = new Vector3(1000, 1000, 0);

            FollowMovementObj follower = new FollowMovementObj(3, 5, me, player);
            Assert.AreEqual(new Vector2(0, 0), follower.Move(0));
        }

        [Test]
        public void KeepChasingTest()
        {
            GameObject g1 = new GameObject();
            GameObject g2 = new GameObject();

            Transform me = g1.transform;
            Transform player = g2.transform;
            me.position = new Vector3(0, 0, 0);
            player.position = new Vector3(1, 1, 0);

            FollowMovementObj follower = new FollowMovementObj(3, 5, me, player);
            Assert.AreEqual(new Vector2(1, 1).normalized * 3, follower.Move(0));

            player.position = new Vector3(1000, 1000, 0);
            Assert.AreEqual(new Vector2(1, 1).normalized * 3, follower.Move(0));


            player.position = new Vector3(10000, 10000, 0);
            Assert.AreEqual(new Vector2(1, 1).normalized * 3, follower.Move(0));
        }


    }
}
