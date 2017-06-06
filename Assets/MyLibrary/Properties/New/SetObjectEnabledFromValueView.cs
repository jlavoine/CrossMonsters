using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyLibrary {
    public class SetObjectEnabledFromValueView : PropertyView {
        [Serializable]
        public class EnabledPair {
            public GameObject Object;
            public string TargetValue;
        }

        public List<EnabledPair> Objects;

        public override void UpdateView() {
            string value = GetValue<string>();

            SetObjectsEnabledState( value );
        }

        private void SetObjectsEnabledState( string i_targetValue ) {
            foreach ( EnabledPair pair in Objects ) {
                bool isTarget = pair.TargetValue == i_targetValue;
                GameObject obj = pair.Object;
                obj.SetActive( isTarget );
            }
        }
    }
}