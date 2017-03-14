using Zenject;

namespace MyLibrary {
    public class AppUpdateRequiredView : GroupView {
        [Inject]
        IAppUpdateRequiredPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}