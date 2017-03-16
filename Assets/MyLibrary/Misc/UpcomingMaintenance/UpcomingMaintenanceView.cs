using Zenject;

namespace MyLibrary {
    public class UpcomingMaintenanceView : GroupView {
        [Inject]
        IUpcomingMaintenancePM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void Dismiss() {
            PM.Dismiss();
        }
    }
}
