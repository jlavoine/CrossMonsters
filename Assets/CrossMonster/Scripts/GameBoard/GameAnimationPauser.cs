using UnityEngine;
using Zenject;
using MyLibrary;

namespace MonsterMatch {
    public class GameAnimationPauser : MonoBehaviour {

        [Inject]
        IMessageService MyMessenger;

        private bool mPaused = false;

        void Start() {
            ListenForMessages( true );
        }

        void OnDestroy() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.AddListener<GameStates>( GameMessages.GAME_STATE_CHANGED, OnGameStateChanged );
            } else {
                MyMessenger.RemoveListener<GameStates>( GameMessages.GAME_STATE_CHANGED, OnGameStateChanged );
            }
        }

        private void OnGameStateChanged( GameStates i_state ) {
            if ( i_state == GameStates.Paused ) {
                ChangeAnimationSpeed( 0f );
                mPaused = true;
            } else if ( i_state == GameStates.Playing && mPaused ) {
                ChangeAnimationSpeed( 1f );
                mPaused = false;
            }
        }

        private void ChangeAnimationSpeed( float i_speed ) {
            // this method seems really inefficient...might need to come back to this
            Animator[] animators = GetComponentsInChildren<Animator>();
            foreach ( Animator animator in animators ) {
                animator.speed = i_speed;
            }
        }
    }
}
