using UnityEngine.UI;

namespace MyLibrary {
    public class SetImageFillView : BaseImageView {

        public override void UpdateView() {
            float amount = GetValue<float>();

            if ( Image != null ) {
                Image.fillAmount = amount;
            }
        }
    }
}
