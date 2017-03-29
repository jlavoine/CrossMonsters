using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    [TestFixture]
    public class TestAccountLinkButton : ZenjectUnitTestFixture {
        public class StubAccountLinkButton : LinkAccountButton {
            protected override void Authorize() {}
            protected override void LinkAccount() {}
            protected override void OnSuccessfulAuth() {}
        }

        [Inject]
        IAccountAlreadyLinkedPM MockAlreadyLinkedPM;

        [Inject]
        IAccountLinkDonePM MockLinkDonePM;

        [Inject]
        StubAccountLinkButton systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IAccountLinkDonePM>().FromInstance( Substitute.For<IAccountLinkDonePM>() );
            Container.Bind<IAccountAlreadyLinkedPM>().FromInstance( Substitute.For<IAccountAlreadyLinkedPM>() );
            Container.Bind<StubAccountLinkButton>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenAuthorizeAttemptFails_DonePM_IsShownWithError() {
            systemUnderTest.OnAuthorizeAttempt( false );

            CheckForDonePM_WithSuccess( false );
        }

        [Test]
        public void WhenLinkIsSuccess_DonePM_IsShownWithSuccess() {
            systemUnderTest.OnLinkAttemptResult( true );

            CheckForDonePM_WithSuccess( true );
        }

        [Test]
        public void WhenLinkIsFailure_DonePM_IsShownWithError() {
            systemUnderTest.OnLinkAttemptResult( false );

            CheckForDonePM_WithSuccess( false );
        }

        [Test]
        public void WhenLinkCheckError_DonePM_IsShownWithError() {
            systemUnderTest.OnLinkCheckError();

            CheckForDonePM_WithSuccess( false );
        }

        private void CheckForDonePM_WithSuccess( bool i_success ) {
            MockLinkDonePM.Received().SetLinkSuccess( i_success );
            MockLinkDonePM.Received().Show();
        }
    }
}