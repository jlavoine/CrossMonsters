﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameManager : CrossMonstersUnitTest {

        [Test]
        public void WhenCreated_StateIsPlaying() {
            GameManager systemUnderTest = new GameManager();

            Assert.AreEqual( GameStates.Playing, systemUnderTest.State );
        }
    }
}
