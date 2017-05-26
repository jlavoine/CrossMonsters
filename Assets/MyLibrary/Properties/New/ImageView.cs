using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary {
    public class ImageView : BaseImageView {
        public string Prefix;

        public override void UpdateView() {
            string propertyValue = GetValue<string>();
            if ( string.IsNullOrEmpty( propertyValue ) ) {
                return;
            }

            string imageKey = Prefix + GetValue<string>();
            Sprite sprite = SpriteExtensions.GetSpriteFromResource( imageKey );

            if ( Image != null ) {
                Image.sprite = sprite;
            }
        }
    }
}