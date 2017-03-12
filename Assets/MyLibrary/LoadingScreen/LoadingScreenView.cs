using Zenject;

namespace MyLibrary {
    public class LoadingScreenView : GroupView {
        [Inject]
        ILoadingScreenPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}
