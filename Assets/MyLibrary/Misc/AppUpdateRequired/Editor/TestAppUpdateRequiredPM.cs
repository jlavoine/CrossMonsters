using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestAppUpdateRequiredPM : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreated_IsVisibile_IsFalse() {
            AppUpdateRequiredPM systemUnderTest = new AppUpdateRequiredPM( Substitute.For<IStringTableManager>() );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AppUpdateRequiredPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void OnShow_TextPropertiesSetAsExpected() {
            IStringTableManager mockStringTable = Substitute.For<IStringTableManager>();
            mockStringTable.Get( AppUpdateRequiredPM.BODY_KEY ).Returns( "Body" );
            mockStringTable.Get( AppUpdateRequiredPM.TITLE_KEY ).Returns( "Title" );

            AppUpdateRequiredPM systemUnderTest = new AppUpdateRequiredPM( mockStringTable );
            systemUnderTest.Show();

            Assert.AreEqual( "Title", systemUnderTest.ViewModel.GetPropertyValue<string>( AppUpdateRequiredPM.TITLE_TEXT_PROPERTY ) );
            Assert.AreEqual( "Body", systemUnderTest.ViewModel.GetPropertyValue<string>( AppUpdateRequiredPM.BODY_TEXT_PROPERTY ) );
        }

        [Test]
        public void OnShow_IsVisible_IsTrue() {
            AppUpdateRequiredPM systemUnderTest = new AppUpdateRequiredPM( Substitute.For<IStringTableManager>() );

            systemUnderTest.Show();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( AppUpdateRequiredPM.VISIBLE_PROPERTY ) );
        }
    }
}
