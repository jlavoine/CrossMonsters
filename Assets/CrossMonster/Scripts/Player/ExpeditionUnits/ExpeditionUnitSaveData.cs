using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;
using System;
using Zenject;

namespace MonsterMatch {
    public class ExpeditionUnitSaveData : IExpeditionUnitSaveData {
        public const string EXPEDITION_UNIT_ITEM_TAG = "ExpeditionUnit";

        [Inject]
        IPlayerInventoryManager Inventory;

        [Inject]
        IExpeditionUnitSpawner UnitSpawner;

        private Dictionary<string, IExpeditionUnit> mExpeditionUnits = new Dictionary<string, IExpeditionUnit>();
        public Dictionary<string, IExpeditionUnit> ExpeditionUnits { get { return mExpeditionUnits; } set { mExpeditionUnits = value; } }

        public void Init() {
            CreateUnitsFromInventory();
        }

        private void CreateUnitsFromInventory() {
            List<IMyItemInstance> expeditionUnitItems = Inventory.GetItemsWithTag( EXPEDITION_UNIT_ITEM_TAG );

            foreach ( IMyItemInstance item in expeditionUnitItems ) {
                IExpeditionUnit unit = UnitSpawner.Create( item );
                ExpeditionUnits.Add( item.GetId(), unit );
            }
        }
    }
}