using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestAppUpgradeRequiredPM : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreated_IsVisibile_IsFalse() {
            AppUpgradeRequiredPM systemUnderTest = new AppUpgradeRequiredPM( Substitute.For<IStringTableManager>() );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AppUpgradeRequiredPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreated_TextPropertiesSetAsExpected() {
            IStringTableManager mockStringTable = Substitute.For<IStringTableManager>();
            mockStringTable.Get( AppUpgradeRequiredPM.BODY_KEY ).Returns( "Body" );
            mockStringTable.Get( AppUpgradeRequiredPM.TITLE_KEY ).Returns( "Title" );

            AppUpgradeRequiredPM systemUnderTest = new AppUpgradeRequiredPM( mockStringTable );

            Assert.AreEqual( "Title", systemUnderTest.ViewModel.GetPropertyValue<string>( AppUpgradeRequiredPM.TITLE_TEXT_PROPERTY ) );
            Assert.AreEqual( "Body", systemUnderTest.ViewModel.GetPropertyValue<string>( AppUpgradeRequiredPM.BODY_TEXT_PROPERTY ) );
        }

        [Test]
        public void OnShow_IsVisible_IsTrue() {
            AppUpgradeRequiredPM systemUnderTest = new AppUpgradeRequiredPM( Substitute.For<IStringTableManager>() );

            systemUnderTest.Show();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( AppUpgradeRequiredPM.VISIBLE_PROPERTY ) );
        }
    }
}
