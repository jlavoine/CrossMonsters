using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class PlayerInfoView : GroupView {

        [Inject]
        IPlayerInfoPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}
