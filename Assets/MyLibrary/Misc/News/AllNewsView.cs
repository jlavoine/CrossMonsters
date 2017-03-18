using Zenject;

namespace MyLibrary {
    public class AllNewsView : GroupView {

        [Inject]
        INewsManager NewsManager;

        [Inject]
        IAllNewsPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void ShowNews() {
            PM.Show();
        }

        public void HideNews() {
            PM.Hide();
        }
    }
}
