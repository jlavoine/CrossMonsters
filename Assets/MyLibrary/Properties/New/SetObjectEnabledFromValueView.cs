using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyLibrary {
    public class SetObjectEnabledFromValueView : PropertyView {
        [Serializable]
        public class EnabledPair {
            public GameObject Object;
            public int TargetValue;
        }

        public List<EnabledPair> Objects;

        public override void UpdateView() {
            int value = GetValue<int>();

            SetObjectsEnabledState( value );
        }

        private void SetObjectsEnabledState( int i_targetValue ) {
            foreach ( EnabledPair pair in Objects ) {
                bool isTarget = pair.TargetValue == i_targetValue;
                GameObject obj = pair.Object;
                obj.SetActive( isTarget );
            }
        }
    }
}