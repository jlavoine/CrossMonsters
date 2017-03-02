using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class GamePlayerView : GroupView {
        [Inject]
        IGamePlayerPM PM;

        void Start() {
            UnityEngine.Debug.LogError( "Creating the player pm: " + PM );
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}
