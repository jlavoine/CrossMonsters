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
    public class TestAccountLinkDonePM : ZenjectUnitTestFixture {
        [Inject]
        IStringTableManager MockStringTable;

        [Inject]
        AccountLinkDonePM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IStringTableManager>().FromInstance( Substitute.For<IStringTableManager>() );
            Container.Bind<AccountLinkDonePM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenSuccessIsSet_TextPropertySetAsExpected() {
            MockStringTable.Get( AccountLinkDonePM.SUCCESS_KEY ).Returns( "Success" );

            systemUnderTest.SetLinkSuccess( true );

            Assert.AreEqual( "Success", systemUnderTest.ViewModel.GetPropertyValue<string>( AccountLinkDonePM.TEXT_PROPERTY ) );
        }

        [Test]
        public void WhenFailIsSet_TextPropertySetAsExpected() {
            MockStringTable.Get( AccountLinkDonePM.FAIL_KEY ).Returns( "Failure" );

            systemUnderTest.SetLinkSuccess( false );

            Assert.AreEqual( "Failure", systemUnderTest.ViewModel.GetPropertyValue<string>( AccountLinkDonePM.TEXT_PROPERTY ) );
        }
    }
}
