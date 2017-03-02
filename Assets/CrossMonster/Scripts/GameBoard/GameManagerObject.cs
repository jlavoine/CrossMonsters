using UnityEngine;
using Zenject;

namespace CrossMonsters {
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
