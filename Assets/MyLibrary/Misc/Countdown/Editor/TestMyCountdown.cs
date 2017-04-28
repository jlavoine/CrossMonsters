using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;
using System;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    public class TestMyCountdown : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreatingCountdown_TargetTime_AsExpected() {
            MyCountdown systemUnderTest = new MyCountdown( Substitute.For<IBackendManager>(), 1000, Substitute.For<ICountdownCallback>() );

            Assert.AreEqual( 1000, systemUnderTest.TargetTimeMs );
        }

        static object[] RemainingTimeTests = {
            new object[] { 0, 0, 0 },
            new object[] { 0, 100, 100 },
            new object[] { 100, 0, 0 }
        };

        [Test, TestCaseSource( "RemainingTimeTests" )]
        public void WhenCreatingCountdown_RemainingTime_AsExpected( long i_serverTime, long i_targetTime, long i_expectedRemainingTime ) {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            mockBackend.GetDateTime().Returns( new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( i_serverTime ) );

            IBackendManager mockBackendManager = Substitute.For<IBackendManager>();
            mockBackendManager.GetBackend<IBasicBackend>().Returns( mockBackend );

            MyCountdown systemUnderTest = new MyCountdown( mockBackendManager, i_targetTime, Substitute.For<ICountdownCallback>() );

            Assert.AreEqual( i_expectedRemainingTime, systemUnderTest.RemainingTimeMs );
        }

        static object[] TickTests = {
            new object[] { 0, 0, 0 },
            new object[] { 0, 1, 0 },
            new object[] { 100, 1, 99 },
            new object[] { 100, 0, 100 }
        };

        [Test, TestCaseSource( "TickTests" )]
        public void CountdownTicks_AsExpected( long i_remainingTimeMs, long i_tickTimeMs, long i_expectedRemainingTimeMs ) {
            MyCountdown systemUnderTest = new MyCountdown( Substitute.For<IBackendManager>(), 0, Substitute.For<ICountdownCallback>() );
            systemUnderTest.RemainingTimeMs = i_remainingTimeMs;

            systemUnderTest.Tick( i_tickTimeMs );

            Assert.AreEqual( i_expectedRemainingTimeMs, systemUnderTest.RemainingTimeMs );
        }

        [Test]
        public void WhenCountdownIsTicked_AndNoRemainingTime_CallbackIsSent_AndNulled() {
            ICountdownCallback mockCallback = Substitute.For<ICountdownCallback>();
            MyCountdown systemUnderTest = new MyCountdown( Substitute.For<IBackendManager>(), 0, mockCallback );
            systemUnderTest.RemainingTimeMs = 0;

            systemUnderTest.Tick( 0 );

            mockCallback.Received().SendCallback();
            Assert.IsNull( systemUnderTest.Callback );
        }

        [Test]
        public void WhenCountdownIsTicked_AndNoRemainingTime_IfNullCallback_NoError() {
            MyCountdown systemUnderTest = new MyCountdown( Substitute.For<IBackendManager>(), 0, Substitute.For<ICountdownCallback>() );
            systemUnderTest.RemainingTimeMs = 0;

            systemUnderTest.Tick( 0 );
        }

        [Test]
        public void WhenCountdownIsTicked_AndRemainingTime_CallbackIsNotSent() {
            ICountdownCallback mockCallback = Substitute.For<ICountdownCallback>();
            MyCountdown systemUnderTest = new MyCountdown( Substitute.For<IBackendManager>(), 0, mockCallback );
            systemUnderTest.RemainingTimeMs = 100;

            systemUnderTest.Tick( 0 );

            mockCallback.DidNotReceive().SendCallback();
            Assert.AreEqual( mockCallback, systemUnderTest.Callback );
        }
    }
}
