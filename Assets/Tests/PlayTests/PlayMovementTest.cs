using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayMovementTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Test_Horizontal_Movement()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody2D>();
            var player = gameObject.AddComponent<MainCharacterController>();

            //SETUP
            player.controllerInput = Substitute.For<IControllerInput>();
            player.speed = 4f;
            player.controllerInput.Horizontal.Returns(1f);
            player.controllerInput.Vertical.Returns(0f);
            player.controllerInput.GetTime.Returns(1f);

            yield return null;
            yield return new WaitForFixedUpdate();
            Vector2 posEnd = player.transform.position;
            Assert.Greater(posEnd.x, 3.9f);
            Assert.Less(Mathf.Abs(posEnd.y), 0.1f);
        }

        [UnityTest]
        public IEnumerator Test_Vertical_Movement()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody2D>();
            var player = gameObject.AddComponent<MainCharacterController>();

            //SETUP
            player.controllerInput = Substitute.For<IControllerInput>();
            player.speed = 4f;
            player.controllerInput.Horizontal.Returns(0f);
            player.controllerInput.Vertical.Returns(1f);
            player.controllerInput.GetTime.Returns(1f);

            yield return null;
            yield return new WaitForFixedUpdate();
            Vector2 posEnd = player.transform.position;
            Assert.Greater(posEnd.y, 3.9f);
            Assert.Less(Mathf.Abs(posEnd.x), 0.1f);
        }

        [UnityTest]
        public IEnumerator Test_Composite_Movement()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody2D>();
            var player = gameObject.AddComponent<MainCharacterController>();

            //SETUP
            player.controllerInput = Substitute.For<IControllerInput>();
            player.speed = 4f;
            player.controllerInput.Horizontal.Returns(1f);
            player.controllerInput.Vertical.Returns(1f);
            player.controllerInput.GetTime.Returns(1f);

            yield return null;
            yield return new WaitForFixedUpdate();
            Vector2 posEnd = player.transform.position;
            Assert.Greater(posEnd.y, 2f);
            Assert.Greater(posEnd.x, 2f);
        }

        [UnityTest]
        public IEnumerator Test_Default_MovementController()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody2D>();
            var player = gameObject.AddComponent<MainCharacterController>();

            //SETUP
            player.speed = 4f;

            yield return null;
            yield return new WaitForFixedUpdate();
            Vector2 posEnd = player.transform.position;
            Assert.Less(Mathf.Abs(posEnd.y), 0.1f);
            Assert.Less(Mathf.Abs(posEnd.x), 0.1f);
        }
    }
}
