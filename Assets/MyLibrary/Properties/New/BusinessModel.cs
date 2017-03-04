
namespace MyLibrary {
    public abstract class BusinessModel {
        public event ModelUpdateHandler ModelUpdated;

        protected void SendModelChangedEvent() {
            if ( ModelUpdated != null ) {
                ModelUpdated();
            }
        }
    }
}
