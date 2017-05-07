using System.Collections.Generic;

namespace MyLibrary {
    public class ActiveLoginPromoPM : BasicWindowPM, IActiveLoginPromoPM {

        readonly IActiveLoginPromoButtonPM_Spawner mSpawnerForButtonPM;
        readonly ILoginPromotionManager mManager;

        private List<IActiveLoginPromoButtonPM> mButtonPMs = new List<IActiveLoginPromoButtonPM>();
        public List<IActiveLoginPromoButtonPM> ButtonPMs { get { return mButtonPMs; } set { mButtonPMs = value; } }

        public ActiveLoginPromoPM( IActiveLoginPromoButtonPM_Spawner i_spawner, ILoginPromotionManager i_manager ) {
            mSpawnerForButtonPM = i_spawner;
            mManager = i_manager;

            SetVisibleProperty( false );
            CreateButtonPMs();
        }

        private void CreateButtonPMs() {
            foreach ( ILoginPromotionData promoData in mManager.ActivePromotionData ) {
                IActiveLoginPromoButtonPM pm = mSpawnerForButtonPM.Create( promoData );
                ButtonPMs.Add( pm );
            }
        }
    }
}
