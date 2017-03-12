using NUnit.Framework;
using NSubstitute;
using MyLibrary;

namespace CrossMonsters {
    [TestFixture]
    public abstract class CrossMonstersUnitTest {

        [SetUp]
        public virtual void BeforeTest() {            
            MyMessenger.Instance = Substitute.For<IMessageService>();
            StringTableManager.Instance = Substitute.For<IStringTableManager>();
        }

        [TearDown]
        public virtual void AfterTest() {            
            MyMessenger.Instance = null;
            StringTableManager.Instance = null;
        }
    }
}
