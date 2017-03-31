using NUnit.Framework;
using NSubstitute;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414


namespace MyLibrary {
    [TestFixture]
    public class TestRemoveAccountLinkPM : ZenjectUnitTestFixture {

        [Inject]
        IStringTableManager MockStringTable;

        [Inject]
        RemoveAccountLinkPM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IStringTableManager>().FromInstance( Substitute.For<IStringTableManager>() );
            Container.Bind<RemoveAccountLinkPM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_TextPropertySetAsExpected() {
            MockStringTable.Get( RemoveAccountLinkPM.TEXT_KEY ).Returns( "Test" );

            RemoveAccountLinkPM systemUnderTest = new RemoveAccountLinkPM( MockStringTable );

            Assert.AreEqual( "Test", systemUnderTest.ViewModel.GetPropertyValue<string>( RemoveAccountLinkPM.TEXT_PROPERTY ) );
        }

        [Test]
        public void WhenAttemptingToRemoveLink_PM_IsHidden() {
            systemUnderTest.LinkMethod = Substitute.For<ILinkAccountButton>();

            systemUnderTest.AttemptToUnlinkAccount();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( RemoveAccountLinkPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenAttemptingToRemoveLink_UnlinkMethodIsCalled() {
            systemUnderTest.LinkMethod = Substitute.For<ILinkAccountButton>();

            systemUnderTest.AttemptToUnlinkAccount();

            systemUnderTest.LinkMethod.Received().UnlinkAccount();
        }
    }
}