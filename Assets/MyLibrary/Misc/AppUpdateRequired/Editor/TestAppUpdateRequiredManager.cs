using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestAppUpdateRequiredManager : ZenjectUnitTestFixture {
        [Inject]
        IAppUpdateRequiredPM MockUpgradePM;

        [Inject]
        AppUpdateRequirdManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IAppUpdateRequiredPM>().FromInstance( Substitute.For<IAppUpdateRequiredPM>() );
            Container.Bind<AppUpdateRequirdManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_LatestVersionIsDownloaded() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( AppUpdateRequirdManager.LATEST_VERSION_TITLE_KEY, Arg.Any<Callback<string>>() );
        }

        [Test]
        public void WhenCheckingToTriggerPopup_IfCurrentVersionLessThanAppVersion_PopupIsTriggered() {
            systemUnderTest.LatestAppVersion = AppUpdateRequirdManager.CURRENT_VERSION + 1;
            systemUnderTest.TriggerUpgradeViewIfRequired();

            MockUpgradePM.Received().Show();
        }

        [Test]
        public void WhenCheckingToTriggerPopup_IfCurrentVersionGreaterThanAppVersion_PopupIsTriggered() {
            systemUnderTest.LatestAppVersion = AppUpdateRequirdManager.CURRENT_VERSION - 1;
            systemUnderTest.TriggerUpgradeViewIfRequired();

            MockUpgradePM.DidNotReceive().Show();
        }

        [Test]
        public void WhenCheckingToTriggerPopup_IfCurrentVersionEqualsAppVersion_PopupIsTriggered() {
            systemUnderTest.LatestAppVersion = AppUpdateRequirdManager.CURRENT_VERSION;
            systemUnderTest.TriggerUpgradeViewIfRequired();

            MockUpgradePM.DidNotReceive().Show();
        }
    }
}
