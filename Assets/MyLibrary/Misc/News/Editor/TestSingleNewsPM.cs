using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestSingleNewsPM : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreating_TextsSetAsExpected() {
            IBasicNewsData mockNews = Substitute.For<IBasicNewsData>();
            mockNews.GetTitleKey().Returns( "Title" );
            mockNews.GetBodyKey().Returns( "Body" );
            IStringTableManager mockStringTable = Substitute.For<IStringTableManager>();
            mockStringTable.Get( "Title" ).Returns( "MyTitle" );
            mockStringTable.Get( "Body" ).Returns( "MyBody" );

            SingleNewsPM systemUnderTest = new SingleNewsPM( mockStringTable, mockNews );

            Assert.AreEqual( "MyTitle", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleNewsPM.TITLE_PROPERTY ) );
            Assert.AreEqual( "MyBody", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleNewsPM.BODY_PROPERTY ) );
        }
    }
}
