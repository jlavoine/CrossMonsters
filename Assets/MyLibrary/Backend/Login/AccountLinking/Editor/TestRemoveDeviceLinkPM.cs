using NUnit.Framework;
using NSubstitute;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414


namespace MyLibrary {
    [TestFixture]
    public class TestRemoveDeviceLinkPM : ZenjectUnitTestFixture {
        [Inject]
        IStringTableManager MockStringTable;

        [Inject]
        IBackendManager MockBackendManager;

        [Inject]
        ISceneManager MockSceneManager;

        [Inject]
        RemoveDeviceLinkPM systemUnderTest;

        private IBasicBackend MockBackend;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IStringTableManager>().FromInstance( Substitute.For<IStringTableManager>() );
            Container.Bind<IBackendManager>().FromInstance( Substitute.For<IBackendManager>() );
            Container.Bind<ISceneManager>().FromInstance( Substitute.For<ISceneManager>() );
            Container.Bind<RemoveDeviceLinkPM>().AsSingle();
            Container.Inject( this );

            MockBackend = Substitute.For<IBasicBackend>();
            MockBackendManager.GetBackend<IBasicBackend>().Returns( MockBackend );
        }

        [Test]
        public void WhenCreated_TextPropertySetAsExpected() {
            MockStringTable.Get( RemoveDeviceLinkPM.TEXT_KEY ).Returns( "Test" );

            RemoveDeviceLinkPM systemUnderTest = new RemoveDeviceLinkPM( MockStringTable, MockBackendManager, MockSceneManager );

            Assert.AreEqual( "Test", systemUnderTest.ViewModel.GetPropertyValue<string>( RemoveDeviceLinkPM.TEXT_PROPERTY ) );
        }

        [Test]
        public void WhenRemovingDeviceFromAccount_PM_IsHidden() {
            systemUnderTest.RemoveDeviceFromAccount();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( RemoveDeviceLinkPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenRemovingDeviceFromAccount_BackendMethodIsCalled() {
            systemUnderTest.RemoveDeviceFromAccount();

            MockBackend.Received().UnlinkDeviceFromAccount( Arg.Any<Callback<bool>>() );
        }

        [Test]
        public void WhenRemovingDeviceFromAccount_LoginSceneIsLoaded() {
            systemUnderTest.RemoveDeviceFromAccount();

            MockSceneManager.Received().LoadScene( "Login" );
        }
    }
}