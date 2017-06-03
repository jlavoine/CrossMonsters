using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestExpeditionUnitSaveData : ZenjectUnitTestFixture {

        [Inject]
        ExpeditionUnitSaveData systemUnderTest;

        [Inject]
        IPlayerInventoryManager MockInventory;

        [Inject]
        IExpeditionUnitSpawner MockUnitSpawner;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IPlayerInventoryManager>().FromInstance( Substitute.For<IPlayerInventoryManager>() );
            Container.Bind<IExpeditionUnitSpawner>().FromInstance( Substitute.For<IExpeditionUnitSpawner>() );
            Container.Bind<ExpeditionUnitSaveData>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenInited_ExpeditionUnitsCreated_ForEachItemWithTag_InPlayersInventory() {
            IMyItemInstance expeditionUnitItem_1 = CreateExpeditionUnitItemWithId( "1" );            
            IMyItemInstance expeditionUnitItem_2 = CreateExpeditionUnitItemWithId( "2" );
            IMyItemInstance expeditionUnitItem_3 = CreateExpeditionUnitItemWithId( "3" );
            List<IMyItemInstance> expeditionUnitItems = new List<IMyItemInstance>() { expeditionUnitItem_1, expeditionUnitItem_2, expeditionUnitItem_3 };
            MockInventory.GetItemsWithTag( ExpeditionUnitSaveData.EXPEDITION_UNIT_ITEM_TAG ).Returns( expeditionUnitItems );

            systemUnderTest.Init();

            MockUnitSpawner.Received().Create( expeditionUnitItem_1 );
            MockUnitSpawner.Received().Create( expeditionUnitItem_2 );
            MockUnitSpawner.Received().Create( expeditionUnitItem_3 );

            Assert.AreEqual( 3, systemUnderTest.ExpeditionUnits.Count );

            Assert.IsTrue( systemUnderTest.ExpeditionUnits.ContainsKey( "1" ) );
            Assert.IsTrue( systemUnderTest.ExpeditionUnits.ContainsKey( "2" ) );
            Assert.IsTrue( systemUnderTest.ExpeditionUnits.ContainsKey( "3" ) );
        }

        private IMyItemInstance CreateExpeditionUnitItemWithId( string i_id ) {
            IMyItemInstance expeditionUnitItem = Substitute.For<IMyItemInstance>();
            expeditionUnitItem.GetId().Returns( i_id );

            return expeditionUnitItem;
        }
    }
}