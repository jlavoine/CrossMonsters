﻿using Zenject;

namespace MyLibrary {
    public class AccountLinkDoneView : GroupView {

        [Inject]
        IAccountLinkDonePM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void ShowView() {
            PM.Show();
        }

        public void HideView() {
            PM.Hide();
        }
    }
}
