using UnityEngine;

namespace MyLibrary {
    [RequireComponent( typeof( CanvasGroup ) )]
    public class SetCanvasVisibilityView : PropertyView {
        public bool IsAlphaBoolean;

        private CanvasGroup mCanvasGroup;
        public CanvasGroup CanvasGroup {
            get {
                if ( mCanvasGroup == null ) {
                    mCanvasGroup = GetComponent<CanvasGroup>();
                }

                return mCanvasGroup;
            }
        }

        public override void UpdateView() {
            SetCanvasGroupAlpha();
            SetCanvasInteractables();
        }

        private void SetCanvasGroupAlpha() {
            CanvasGroup.alpha = GetAlphaValue();
        }

        private void SetCanvasInteractables() {
            bool isInteractable = GetAlphaValue() > 0;
            CanvasGroup.interactable = isInteractable;
            CanvasGroup.blocksRaycasts = isInteractable;         
        }

        private float GetAlphaValue() {
            if ( IsAlphaBoolean ) {
                bool on = GetValue<bool>();
                return on ? 1f : 0f;
            }
            else {
                return GetValue<float>();
            }
        }
    }
}
