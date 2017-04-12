using NUnit.Framework;
using NSubstitute;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414


namespace MyLibrary {
    [TestFixture]
    public class TestAccountLinker : ZenjectUnitTestFixture {
        [Inject]
        IGoogleLinker GoogleLinker;

        [Inject]
        AccountLinker systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGoogleLinker>().FromInstance( Substitute.For<IGoogleLinker>() );
            Container.Bind<AccountLinker>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenLinkingWithGoogle_GoogleLinkerAttemptsToLink_NoForce() {
            systemUnderTest.AttemptToLink( LoginMethods.Google, ( result ) => { } );

            GoogleLinker.Received().AttemptLink( Arg.Any<Callback<AccountLinkResultTypes>>(), false );
        }

        [Test]
        public void WhenForceLinkingWithGoogle_GoogleLinkerAttemptsToLink_WithForce() {
            systemUnderTest.AttemptForceLink( LoginMethods.Google, ( result ) => { } );

            GoogleLinker.Received().AttemptLink( Arg.Any<Callback<AccountLinkResultTypes>>(), true );
        }
    }
}
