using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public class LoginPromoDisplaysPM : PresentationModel, ILoginPromoDisplaysPM {

        readonly ILoginPromotionManager mManager;
        readonly ISingleLoginPromoPM_Spawner mSpawner;

        private List<ISingleLoginPromoDisplayPM> mDisplayPMs;
        public List<ISingleLoginPromoDisplayPM> DisplayPMs { get { return mDisplayPMs; } set { mDisplayPMs = value; } }

        public LoginPromoDisplaysPM( ISingleLoginPromoPM_Spawner i_spawner, ILoginPromotionManager i_manager ) {
            mSpawner = i_spawner;
            mManager = i_manager;

            CreatePromoPMs();
        }

        private void CreatePromoPMs() {
            DisplayPMs = new List<ISingleLoginPromoDisplayPM>();

            foreach ( ILoginPromotionData promoData in mManager.ActivePromotionData ) {
                ISingleLoginPromoDisplayPM pm = mSpawner.Create( promoData );
                DisplayPMs.Add( pm );
            }
        }
    }
}