using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FindTargetPositionTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FindTargetPositionTestSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.AreEqual(8, Enemy.findTargetPosition(4, 1));
            Assert.AreEqual(0, Enemy.findTargetPosition(4, -1));

            Assert.AreEqual(8, Enemy.findTargetPosition(5.6f, 1));
            Assert.AreEqual(4, Enemy.findTargetPosition(5.6f, -1));

            Assert.AreEqual(4, Enemy.findTargetPosition(0.69f, 1));
            Assert.AreEqual(0, Enemy.findTargetPosition(0.69f, -1));

            Assert.AreEqual(72, Enemy.findTargetPosition(69, 1));
            Assert.AreEqual(68, Enemy.findTargetPosition(69, -1));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator FindTargetPositionTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            //GameObject playerObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            GameObject winPlatformObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/WinPlatform"));

            //Assert.

            // Use yield to skip a frame.
            yield return null;
        }
    }
}
