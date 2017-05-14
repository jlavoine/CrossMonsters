using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class SingleRewardPM : PresentationModel, ISingleRewardPM {
        public const string COVER_VISIBLE_PROPERTY = "IsCoverVisible";
        public const string COUNT_PROPERTY = "Count";
        public const string NAME_PROPERTY = "Name";

        readonly IStringTableManager mStringTable;
        readonly IAllRewardsPM mAllRewardsPM;

        private IGameReward mReward;

        public SingleRewardPM( IStringTableManager i_stringTable, IGameReward i_reward, IAllRewardsPM i_allRewardsPM ) {
            mReward = i_reward;
            mStringTable = i_stringTable;
            mAllRewardsPM = i_allRewardsPM;

            SetCoverVisibleProperty( true ); 
            UpdateRewardProperties();
        }

        public void SetReward( IGameReward i_reward ) {
            mReward = i_reward;
            UpdateRewardProperties();
        }

        public void UncoverReward() {
            SetCoverVisibleProperty( false );

            // To Future Engineer: This should probably be abstracted out to an event; not every single reward belongs to an AllRewardsPM
            if ( mAllRewardsPM != null ) {
                mAllRewardsPM.RewardUncovered();
            }
        }

        private void UpdateRewardProperties() {
            // rewards may be set at a later date (from an async call)
            if ( mReward != null ) {
                SetCountProperty();
                SetNameProperty();
            }
        }

        private void SetCoverVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( COVER_VISIBLE_PROPERTY, i_visible );
        }

        private void SetCountProperty() {
            ViewModel.SetProperty( COUNT_PROPERTY, mReward.GetCount().ToString() );
        }

        private void SetNameProperty() {
            string text = mStringTable.Get( mReward.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        public class Factory : Factory<IDungeonReward, IAllRewardsPM, SingleRewardPM> { }
    }
}
