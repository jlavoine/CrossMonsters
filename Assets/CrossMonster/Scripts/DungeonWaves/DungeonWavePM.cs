using Zenject;
using MyLibrary;

namespace CrossMonsters {
    public class DungeonWavePM : PresentationModel, IDungeonWavePM {
        public const string WAVE_TEXT_PROPERTY = "WaveLabel";
        public const string TRIGGER_PROPERTY = "AnimationState";
        public const string SHOW_TRIGGER = "Show";
        public const string HIDE_TRIGGER = "Hide";

        public const string WAVE_LABEL_FORMAT = "Wave {0} of {1}";

        [Inject]
        IGameManager GameManager;

        private int mCurrentWaveIndex = 0;
        public int CurrentWaveIndex { get { return mCurrentWaveIndex; } set { mCurrentWaveIndex = value; } }

        private int mEndWaveIndex = 3;
        public int EndWaveIndex { get { return mEndWaveIndex; } set { mEndWaveIndex = value; } }

        public DungeonWavePM() { }

        public void Show() {
            IncrementCurrentWave();
            SetTriggerState( SHOW_TRIGGER );
            SetText( GetWaveLabel() );
        }

        public void Hide() {
            SetTriggerState( HIDE_TRIGGER );
            GameManager.BeginWaveGameplay();
        }

        public void SetEndWave( int i_index ) {
            EndWaveIndex = i_index;
        }

        private void SetTriggerState( string i_state ) {
            ViewModel.SetProperty( TRIGGER_PROPERTY, i_state );
        }

        private void IncrementCurrentWave() {
            CurrentWaveIndex++;
        }

        private void SetText( string i_text ) {            
            ViewModel.SetProperty( WAVE_TEXT_PROPERTY, i_text );
        }

        private string GetWaveLabel() {
            string text = string.Format( WAVE_LABEL_FORMAT, CurrentWaveIndex, EndWaveIndex );
            return text;
        }
    }
}
