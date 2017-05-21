﻿using MyLibrary;
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

        public void DisplayPromoAndHideOthers( string i_id ) {
            foreach ( ISingleLoginPromoDisplayPM pm in DisplayPMs ) {
                pm.UpdateVisibilityBasedOnCurrentlyDisplayedPromo( i_id );
            }
        }

        public bool DoesHaveDisplayForPromo( string i_id ) {
            foreach ( ISingleLoginPromoDisplayPM pm in DisplayPMs ) {
                if ( pm.GetId() == i_id ) {
                    return true;
                }
            }

            return false;
        }

        private void CreatePromoPMs() {
            DisplayPMs = new List<ISingleLoginPromoDisplayPM>();

            if ( mManager.ActivePromotionData != null ) {
                foreach ( KeyValuePair<string,ILoginPromotionData> kvp in mManager.ActivePromotionData ) {
                    ISingleLoginPromoDisplayPM pm = mSpawner.Create( kvp.Value );
                    DisplayPMs.Add( pm );
                }
            }
        }
    }
}