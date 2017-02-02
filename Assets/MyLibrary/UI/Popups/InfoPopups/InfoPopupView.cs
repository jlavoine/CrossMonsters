﻿
namespace MyLibrary {
    public class InfoPopupView : GroupView {

        protected override void OnDestroy() {
            base.OnDestroy();

            MyMessenger.Instance.Send( InfoPopupEvents.CLOSE );
        }
    }
}
