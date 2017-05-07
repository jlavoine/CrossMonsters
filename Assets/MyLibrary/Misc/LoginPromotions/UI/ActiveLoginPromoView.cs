using Zenject;
using UnityEngine;

namespace MyLibrary {
    public class ActiveLoginPromoView : GroupView {
        public GameObject ActiveLoginPromoButtonPrefab;
        public GameObject ButtonContentArea;

        [Inject]
        IActiveLoginPromoPM PM;

        void Start() {
            SetModel( PM.ViewModel );

            CreatePromoButtonViews();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void Show() {
            PM.Show();
        }

        public void Hide() {
            PM.Hide();
        }

        private void CreatePromoButtonViews() {
            foreach ( IActiveLoginPromoButtonPM buttonPM in PM.ButtonPMs ) {
                GameObject buttonObject = gameObject.InstantiateUI( ActiveLoginPromoButtonPrefab, ButtonContentArea );
                ActiveLoginPromoButtonView viewScript = buttonObject.GetComponent<ActiveLoginPromoButtonView>();
                viewScript.Init( buttonPM );
            }
        }
    }
}
