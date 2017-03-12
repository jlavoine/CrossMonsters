using Zenject;

namespace MyLibrary {
    public class AppUpgradeRequiredView : GroupView {
        [Inject]
        IAppUpgradeRequiredPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}