using UnityEngine;

namespace MyLibrary {
    public class SetAnimatorEnabledView : PropertyView {
        private Animator mAnimator;
        public Animator Animator {
            get {
                if ( mAnimator == null ) {
                    mAnimator = GetComponent<Animator>();
                }

                return mAnimator;
            }

            set {
                mAnimator = value;
            }
        }

        public override void UpdateView() {
            bool isOn = GetValue<bool>();

            if ( Animator ) {
                Animator.enabled = isOn;
            }
        }
    }
}