using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestBaseSceneStartFlowStep : MonoBehaviour {
        public class StubFlowStep : BaseSceneStartFlowStep {
            public StubFlowStep( ISceneStartFlowManager i_manager ) : base( i_manager ) { }
            public override void Start() {}
            protected override void OnDone() {}
        }

        [Test]
        public void WhenStepIsDone_ManagerDoneMethodIsCalled() {
            ISceneStartFlowManager mockManager = Substitute.For<ISceneStartFlowManager>();
            StubFlowStep systemUnderTest = new StubFlowStep( mockManager );

            systemUnderTest.Done();

            mockManager.Received().StepFinished();
        }
    }
}
