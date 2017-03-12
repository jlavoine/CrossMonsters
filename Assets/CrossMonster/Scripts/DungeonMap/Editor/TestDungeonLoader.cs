using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    public class TestDungeonLoader : ZenjectUnitTestFixture {

        [Test]
        public void OnClick_LoadingScreenShowIsCalled() {
            ILoadingScreenPM mockLoading = Substitute.For<ILoadingScreenPM>();
            DungeonLoader systemUnderTest = new DungeonLoader( mockLoading );

            systemUnderTest.OnClick();

            mockLoading.Received().Show();
        }
    }
}
