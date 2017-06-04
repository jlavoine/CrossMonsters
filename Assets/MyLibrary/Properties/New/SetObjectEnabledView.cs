using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary {
    public class SetObjectEnabledView : PropertyView {
        public enum EnabledStates {
            Enabled,
            Disabled
        }

        public EnabledStates SetToState;
        public List<GameObject> Objects;

        public override void UpdateView() {
            bool value = GetValue<bool>();
            bool activeState = GetActiveState( value );

            SetObjectsEnabledState( activeState );
        }

        private void SetObjectsEnabledState( bool i_enabled ) {
            foreach ( GameObject obj in Objects ) {
                obj.SetActive( i_enabled );
            }
        }

        private bool GetActiveState( bool i_propertyValue ) {
            // this method may be a little confusing. This script allows the enabling or disabling of an object
            // based on a property. If the property is TRUE, the state of the object will be set to what SetToState is.
            // if the property is FALSE, it's the opposite.
            if ( i_propertyValue ) {
                return SetToState == EnabledStates.Enabled;
            } else {
                return SetToState != EnabledStates.Enabled;
            }
        }
    }
}