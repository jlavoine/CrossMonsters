using MyLibrary;

namespace MonsterMatch {
    public class InGameSettingsPM : BasicWindowPM, IInGameSettingsPM {
        readonly IGameManager mGameManager;

        public InGameSettingsPM( IGameManager i_gameManager ) {
            mGameManager = i_gameManager;
            Hide();
        }

        protected override void OnShown() {
            mGameManager.SetState( GameStates.Paused );
        }

        protected override void OnHidden() {
            mGameManager.SetState( GameStates.Playing );
        }
    }
}
