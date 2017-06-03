using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary {
    public class SetObjectEnabledView : PropertyView {
        public List<GameObject> Objects;

        public override void UpdateView() {
            bool value = GetValue<bool>();

            SetObjectsEnabledState( value );
        }

        private void SetObjectsEnabledState( bool i_enabled ) {
            foreach ( GameObject obj in Objects ) {
                obj.SetActive( i_enabled );
            }
        }
    }
}