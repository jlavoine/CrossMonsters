using UnityEngine;

namespace MyLibrary {
    public class SetAnimatorTriggerView : PropertyView {
        private Animator mAnimator;
        public Animator Animator {
            get {
                if (mAnimator == null ) {
                    mAnimator = GetComponent<Animator>();
                }

                return mAnimator;
            }
        }

        public override void UpdateView() {
            string trigger = GetValue<string>();
            Animator.SetTrigger( trigger );
        }
    }
}
