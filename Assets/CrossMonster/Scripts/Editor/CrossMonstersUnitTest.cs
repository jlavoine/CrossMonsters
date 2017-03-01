using NUnit.Framework;
using NSubstitute;
using MyLibrary;

namespace CrossMonsters {
    [TestFixture]
    public abstract class CrossMonstersUnitTest {

        [SetUp]
        public virtual void BeforeTest() {
            BackendManager.Instance = Substitute.For<IBackendManager>();
            MyMessenger.Instance = Substitute.For<IMessageService>();
            StringTableManager.Instance = Substitute.For<IStringTableManager>();
            GameRules.Instance = Substitute.For<IGameRules>();
            ChainManager.Instance = Substitute.For<IChainManager>();
            DamageCalculator.Instance = Substitute.For<IDamageCalculator>();
            GameManager.Instance = Substitute.For<IGameManager>();
        }

        [TearDown]
        public virtual void AfterTest() {
            BackendManager.Instance = null;
            MyMessenger.Instance = null;
            StringTableManager.Instance = null;
            GameRules.Instance = null;
            ChainManager.Instance = null;
            DamageCalculator.Instance = null;
            GameManager.Instance = null;
        }
    }
}
