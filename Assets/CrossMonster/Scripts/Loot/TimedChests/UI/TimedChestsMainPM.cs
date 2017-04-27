using MyLibrary;
using Zenject;
using System.Collections.Generic;

namespace MonsterMatch {
    public class TimedChestsMainPM : BasicWindowPM, ITimedChestsMainPM {
        readonly ITimedChestDataManager ChestDataManager;

        readonly ITimedChestPM_Spawner PM_Spawner;

        private List<ITimedChestPM> mChestPMs;
        public List<ITimedChestPM> ChestPMs { get { return mChestPMs; } set { mChestPMs = value; } }

        public TimedChestsMainPM( ITimedChestDataManager i_dataManager, ITimedChestPM_Spawner i_spawner ) {
            ChestDataManager = i_dataManager;
            PM_Spawner = i_spawner;

            SetVisibleProperty( false );

            CreateTimedChestPMs();
        }

        private void CreateTimedChestPMs() {
            ChestPMs = new List<ITimedChestPM>();

            if ( ChestDataManager.TimedChestData != null ) {
                foreach ( ITimedChestData chestData in ChestDataManager.TimedChestData ) {
                    ChestPMs.Add( PM_Spawner.Create( chestData ) );
                }
            }
        }
    }
}