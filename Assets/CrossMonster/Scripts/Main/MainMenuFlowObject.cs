using UnityEngine;
using Zenject;

namespace MonsterMatch {
    public class MainMenuFlowObject : MonoBehaviour {

        [Inject]
        MainMenuFlow Flow; 

        void Start() {
            Flow.Start();
        }
    }
}
