using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary {
    public class SetBlocksRaycasts : PropertyView {
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
            bool state = GetValue<bool>();            

            if ( CanvasGroup != null ) {
                CanvasGroup.blocksRaycasts = state;
            }
            else {
                MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Error, "No CanvasGroup element for SetBlocksRaycasts: " + PropertyName, "UI" );
            }
        }
    }
}
