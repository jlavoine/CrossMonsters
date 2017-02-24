using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary {
    public class SetOutlineColorView : PropertyView {
        private Outline mOutline;
        public Outline Outline {
            get {
                if ( mOutline == null ) {
                    mOutline = GetComponent<Outline>();
                }

                return mOutline;
            }
        }

        public override void UpdateView() {
            Color color = GetValue<Color>();

            if ( Outline != null ) {
                Outline.effectColor = color;
            }
        }
    }
}