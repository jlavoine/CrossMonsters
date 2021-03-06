﻿using Zenject;

namespace MyLibrary {
    public class AccountAlreadyLinkedView : GroupView {

        [Inject]
        IAccountAlreadyLinkedPM PM;

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

        public void UseCurrentSave() {
            PM.UseCurrentSave();
        }

        public void UseExistingSave() {
            PM.UseExistingSave();
        }
    }
}
