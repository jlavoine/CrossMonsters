using UnityEngine;

namespace CrossMonsters {
    public class GameManagerObject : MonoBehaviour {
        private IGameManager mManager;

        void Start() {
            mManager = new GameManager();
        }

        void OnDestroy() {
            mManager.Dispose();
        }
    }
}
