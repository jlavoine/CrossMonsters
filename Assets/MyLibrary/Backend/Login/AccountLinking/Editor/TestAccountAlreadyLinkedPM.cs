using NUnit.Framework;
using NSubstitute;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414


namespace MyLibrary {
    [TestFixture]
    public class TestAccountAlreadyLinkedPM : ZenjectUnitTestFixture {
        [Inject]
        IStringTableManager MockStringTable;

        [Inject]
        AccountAlreadyLinkedPM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IStringTableManager>().FromInstance( Substitute.For<IStringTableManager>() );
            Container.Bind<AccountAlreadyLinkedPM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreated_TextPropertyAsExpected() {
            MockStringTable.Get( AccountAlreadyLinkedPM.TEXT_KEY ).Returns( "SomeText" );

            AccountAlreadyLinkedPM systemUnderTest = new AccountAlreadyLinkedPM( MockStringTable );

            Assert.AreEqual( "SomeText", systemUnderTest.ViewModel.GetPropertyValue<string>( AccountAlreadyLinkedPM.TEXT_PROPERTY ) );
        }

        [Test]
        public void WhenForceLinkingAccount_LinkMethodIsCalled() {
            ILinkAccountButton mockLinkMethod = Substitute.For<ILinkAccountButton>();
            systemUnderTest.LinkMethod = mockLinkMethod;
            systemUnderTest.ForceLink();

            mockLinkMethod.Received().ForceLinkAccount();
        }

        [Test]
        public void WhenForceLinkingAccount_PM_IsHidden() {
            ILinkAccountButton mockLinkMethod = Substitute.For<ILinkAccountButton>();
            systemUnderTest.LinkMethod = mockLinkMethod;
            systemUnderTest.ForceLink();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AccountAlreadyLinkedPM.VISIBLE_PROPERTY ) );
        }
    }
}