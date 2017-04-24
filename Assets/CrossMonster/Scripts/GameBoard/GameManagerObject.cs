using UnityEngine;
using Zenject;

namespace MonsterMatch {
    public class GameManagerObject : MonoBehaviour {
        [Inject]
        IGameManager GameManager;

        void Start() {
        }

        void OnDestroy() {
            GameManager.Dispose();
        }
    }
}
