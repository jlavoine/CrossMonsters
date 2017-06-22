using UnityEngine;

namespace MonsterMatch {
    public class GamePieceAnimationForwarder : MonoBehaviour {

        public void OnAnimationComplete() {
            GamePieceView view = GetComponentInParent<GamePieceView>();
            view.OnAnimationComplete();
        }
    }
}