using System;
using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class AllRewardsPM : BasicWindowPM, IAllRewardsPM {
        public const string CAN_CONTINUE_PROPERTY = "CanContinue";

        readonly ISingleRewardPM_Spawner mSpawner;
        readonly IMessageService mMessenger;

        private int mCoveredRewardCount;
        public int CoveredRewardCount { get { return mCoveredRewardCount; } set { mCoveredRewardCount = value; } }

        private List<ISingleRewardPM> mSingleRewardPMs;
        public List<ISingleRewardPM> SingleRewardPMs { get { return mSingleRewardPMs; } set { mSingleRewardPMs = value; } }

        public AllRewardsPM( ISingleRewardPM_Spawner i_spawner, IMessageService i_messenger, List<IDungeonReward> i_rewards ) {
            mSpawner = i_spawner;
            mMessenger = i_messenger;
            mCoveredRewardCount = i_rewards.Count;

            CreateSingleRewardPMs( i_rewards );
            ListenForMessages( true );
            SetVisibleProperty( false );
            SetCanContinueProperty( false );
        }

        protected override void _Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                mMessenger.AddListener<bool>( GameMessages.GAME_OVER, OnGameOver );
            }
            else {
                mMessenger.RemoveListener<bool>( GameMessages.GAME_OVER, OnGameOver );
            }
        }

        public void OnGameOver( bool i_win ) {
            if ( i_win ) {
                Show();
            }
        }

        public void RewardUncovered() {
            ReduceCoveredRewardCount();
            AllowContinueIfAllRewardsUncovered();
        }

        private void CreateSingleRewardPMs( List<IDungeonReward> i_rewards ) {
            SingleRewardPMs = new List<ISingleRewardPM>();
            foreach ( IDungeonReward reward in i_rewards ) {
                SingleRewardPMs.Add( mSpawner.Create( reward, this ) );
            }
        }

        private void ReduceCoveredRewardCount() {
            CoveredRewardCount--;
        }

        private void AllowContinueIfAllRewardsUncovered() {
            if ( CoveredRewardCount <= 0 ) {
                SetCanContinueProperty( true );
            }
        }

        private void SetCanContinueProperty( bool i_can ) {
            ViewModel.SetProperty( CAN_CONTINUE_PROPERTY, i_can );
        }
    }
}