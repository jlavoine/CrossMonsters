using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary {
    public class SetImageColorView : BaseImageView {

        public override void UpdateView() {
            Color textColor = GetValue<Color>();

            if ( Image != null ) {
                Image.color = textColor;
            }
        }
    }
}
