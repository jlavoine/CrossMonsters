using UnityEngine;

namespace MyLibrary {
    public class SetChildAnimatorTriggerView : PropertyView {
        private Animator mAnimator;
        public GameObject ChildTarget;
        public Animator Animator
        {
            get
            {
                if (mAnimator == null)
                {
                    mAnimator = ChildTarget.GetComponent<Animator>();
                }

                return mAnimator;
            }
        }

        public override void UpdateView()
        {
            string trigger = GetValue<string>();
            if (Animator)
            {
                mAnimator.SetTrigger(trigger);
            }
        }
    }
}
