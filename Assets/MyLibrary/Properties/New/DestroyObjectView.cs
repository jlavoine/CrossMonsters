
namespace MyLibrary {
    public class DestroyObjectView : PropertyView {

        public override void UpdateView() {
            bool propertyValue = GetValue<bool>();

            if ( propertyValue ) {
                Destroy( gameObject );
            }
        }
    }
}
