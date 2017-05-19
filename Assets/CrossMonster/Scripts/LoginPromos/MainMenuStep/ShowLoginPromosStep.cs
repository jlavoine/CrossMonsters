﻿using MyLibrary;
using Zenject;
using System.Collections.Generic;

namespace MonsterMatch {
    public class ShowLoginPromosStep : BaseSceneStartFlowStep, IShowLoginPromosStep {
        readonly IMessageService mMessenger;
        readonly ILoginPromotionManager mPromoManager;
        readonly ILoginPromoDisplaysPM mAllPromosPM;
        readonly ILoginPromoPopupHelper mHelper;

        private List<ISingleLoginPromoProgressSaveData> mActiveSaveData = new List<ISingleLoginPromoProgressSaveData>();
        public List<ISingleLoginPromoProgressSaveData> ActivePromoSaveData { get { return mActiveSaveData; } set { mActiveSaveData = value; } }

        public ShowLoginPromosStep( ILoginPromoPopupHelper i_popupHelper, ILoginPromoDisplaysPM i_allPromosPM, ILoginPromotionManager i_manager, IMessageService i_messenger, ISceneStartFlowManager i_sceneManager ) : base (i_sceneManager ) {
            mMessenger = i_messenger;
            mPromoManager = i_manager;
            mAllPromosPM = i_allPromosPM;
            mHelper = i_popupHelper;

            ActivePromoSaveData = i_manager.GetActivePromoSaveData();            
        }

        public override void Start() {
            ListenForMessages( true );

            ProcessNextPromotion();
        }

        protected override void OnDone() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_shouldListen ) {
            if ( i_shouldListen ) {
                mMessenger.AddListener( SingleLoginPromoDisplayPM.PROMO_DISMISSED_EVENT, ProcessNextPromotion );
            } else {
                mMessenger.RemoveListener( SingleLoginPromoDisplayPM.PROMO_DISMISSED_EVENT, ProcessNextPromotion );
            }
        }

        public void ProcessNextPromotion() {
            if ( ActivePromoSaveData.Count > 0 ) {
                TryToShowNextPromo();                
            } else {
                Done();
            }
        }

        private void TryToShowNextPromo() {
            ISingleLoginPromoProgressSaveData nextPromoToShow = ActivePromoSaveData[0];
            ActivePromoSaveData.RemoveAt( 0 );

            if ( mHelper.ShouldShowPromoAsPopup( nextPromoToShow ) ) {
                mAllPromosPM.DisplayPromoAndHideOthers( nextPromoToShow.GetId() );
            } else {
                ProcessNextPromotion();
            }
        }

        public class Factory : Factory<ISceneStartFlowManager, ShowLoginPromosStep> { }
    }
}
