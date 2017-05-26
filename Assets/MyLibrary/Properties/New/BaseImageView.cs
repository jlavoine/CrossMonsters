using UnityEngine.UI;

namespace MyLibrary {
    public abstract class BaseImageView : PropertyView {
        private Image mImage;
        public Image Image {
            get {
                if ( mImage == null ) {
                    mImage = GetComponent<Image>();
                }

                return mImage;
            }
        }
    }
}