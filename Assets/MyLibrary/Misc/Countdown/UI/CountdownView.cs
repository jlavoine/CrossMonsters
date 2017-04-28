using UnityEngine;
using System;

namespace MyLibrary {
    public class CountdownView : BaseLabelView {
        public string CountdownFormatProperty;

        private IMyCountdown mCountdown;

        public override void UpdateView() {
            mCountdown = GetValue<IMyCountdown>();    
        }

        void Update() {
            if ( mCountdown != null && mCountdown.RemainingTimeMs > 0 ) {
                mCountdown.Tick( (long)( Time.deltaTime * 1000 ) );
                UpdateText();
            }
        }

        private void UpdateText() {
            Action<long, Action<string>> formatAction = Model.GetPropertyValue<Action<long, Action<string>>>( CountdownFormatProperty );
            if ( formatAction != null ) {
                formatAction( mCountdown.RemainingTimeMs, ( result ) => {
                    SetText( result );
                } );
            }
        }
    }
}