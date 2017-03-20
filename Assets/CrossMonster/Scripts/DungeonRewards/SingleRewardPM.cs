using MyLibrary;

namespace CrossMonsters {
    public class SingleRewardPM : PresentationModel, ISingleRewardPM {
        public const string COVER_VISIBLE_PROPERTY = "IsCoverVisible";

        public SingleRewardPM() {
            SetCoverVisibleProperty( true );
        }

        private void SetCoverVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( COVER_VISIBLE_PROPERTY, i_visible );
        }
    }
}
