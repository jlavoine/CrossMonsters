using UnityEngine;
using Zenject;

namespace CrossMonsters {
    public class MainMenuFlowObject : MonoBehaviour {

        [Inject]
        MainMenuFlow Flow; 

        void Start() {
            Flow.Start();
        }
    }
}
