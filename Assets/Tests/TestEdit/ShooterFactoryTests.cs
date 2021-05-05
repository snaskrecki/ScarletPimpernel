using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ShooterFactoryTests
    {
        [Test]
        public void ShootDirectionsFactoryReturnTypeTest()
        {
            var testing = new GameObject();
            var factory = testing.AddComponent<ShootDirectionsFactory>();
            Assert.IsInstanceOf<ShootDirectionsObject>(factory.MakeShooter(testing));
        }



        [Test]
        public void ShootPlayerFactoryReturnTypeTest()
        {
            var testing = new GameObject();
            var factory = testing.AddComponent<ShootPlayerFactory>();
            Assert.IsInstanceOf<ShootPlayerObject>(factory.MakeShooter(testing));
        }

        [Test]
        public void ShootDirectionsFactoryNullBehaviourTest()
        {
            var testing = new GameObject();
            var factory = testing.AddComponent<ShootDirectionsFactory>();
            Assert.DoesNotThrow(() => factory.MakeShooter(null));
        }

        [Test]
        public void ShootPlayerFactoryNullBehaviourTest()
        {
            var testing = new GameObject();
            var factory = testing.AddComponent<ShootPlayerFactory>();
            Assert.Throws<System.NullReferenceException>(() => factory.MakeShooter(null));
        }
    }
}
