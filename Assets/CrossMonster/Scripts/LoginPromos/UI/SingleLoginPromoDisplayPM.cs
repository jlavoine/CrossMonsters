﻿using MyLibrary;
using Zenject;
using System;
using System.Collections.Generic;

namespace MonsterMatch {
    public class SingleLoginPromoDisplayPM : BasicWindowPM, ISingleLoginPromoDisplayPM {
        public const string PROMO_DISMISSED_EVENT = "PromoDismissed";

        public const string TITLE_PROPERTY = "Title";
        public const string DATE_AVAILABLE_PROPERTY = "DateRange";

        public const string DATE_AVAILABLE_FORMAT = "{0} - {1}";

        readonly IStringTableManager mStringTable;
        readonly ILoginPromotionData mData;
        readonly ISingleLoginPromoRewardPM_Spawner mRewardSpawner;
        readonly IMessageService mMessenger;  

        private List<ISingleLoginPromoRewardPM> mRewardPMs = new List<ISingleLoginPromoRewardPM>();
        public List<ISingleLoginPromoRewardPM> RewardPMs { get { return mRewardPMs; } set { mRewardPMs = value; } }

        public SingleLoginPromoDisplayPM( IMessageService i_messenger, ISingleLoginPromoRewardPM_Spawner i_rewardSpawner, IStringTableManager i_stringTable, ILoginPromotionData i_data ) {
            mMessenger = i_messenger;
            mRewardSpawner = i_rewardSpawner;
            mStringTable = i_stringTable;
            mData = i_data;

            CreateLoginRewardPMs();
            SetVisibleProperty( false );
            SetTitleProperty();
            SetActiveDateProperty();
        }

        public void UpdateVisibilityBasedOnCurrentlyDisplayedPromo( string i_id ) {
            bool isVis = i_id == mData.GetId();
            SetVisibleProperty( isVis );
        }

        public string GetPrefab() {
            return mData.GetPromoPrefab();
        }

        public string GetId() {
            return mData.GetId();
        }

        private void CreateLoginRewardPMs() {
            List<IGameRewardData> rewards = mData.GetRewardData();
            if ( rewards != null ) {
                int count = 1;
                foreach ( IGameRewardData rewardData in rewards ) {
                    RewardPMs.Add( mRewardSpawner.Create( count, rewardData ) );
                    count++;
                }
            }
        }

        private void SetTitleProperty() {
            string title = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( TITLE_PROPERTY, title );
        }

        private void SetActiveDateProperty() {
            DateTime start = mData.GetStartTime();
            DateTime end = mData.GetEndTime();
            string display = string.Format( DATE_AVAILABLE_FORMAT, start.ToString(), end.ToString() );
            ViewModel.SetProperty( DATE_AVAILABLE_PROPERTY, display );
        }

        protected override void OnHidden() {
            mMessenger.Send( PROMO_DISMISSED_EVENT );
        }

        public class Factory : Factory<ILoginPromotionData, SingleLoginPromoDisplayPM> { }
    }
}