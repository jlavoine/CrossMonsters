using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;
using System;
using Zenject;

namespace MonsterMatch {
    public class BoostUnitSaveData : IBoostUnitSaveData {
        public const string EXPEDITION_UNIT_ITEM_TAG = "ExpeditionUnit";

        [Inject]
        IPlayerInventoryManager Inventory;

        [Inject]
        IBoostUnitSpawner UnitSpawner;

        private Dictionary<string, IBoostUnit> mExpeditionUnits = new Dictionary<string, IBoostUnit>();
        public Dictionary<string, IBoostUnit> ExpeditionUnits { get { return mExpeditionUnits; } set { mExpeditionUnits = value; } }

        public void Init() {
            CreateUnitsFromInventory();
        }

        private void CreateUnitsFromInventory() {
            List<IMyItemInstance> expeditionUnitItems = Inventory.GetItemsWithTag( EXPEDITION_UNIT_ITEM_TAG );

            foreach ( IMyItemInstance item in expeditionUnitItems ) {
                IBoostUnit unit = UnitSpawner.Create( item );
                ExpeditionUnits.Add( item.GetId(), unit );
            }
        }
    }
}