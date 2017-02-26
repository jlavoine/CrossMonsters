
namespace MyLibrary {
    public abstract class PresentationModel : IPresentationModel {
        private ViewModel mModel;
        public ViewModel ViewModel { get { return mModel; } private set { mModel = value; } }

        public PresentationModel() {
            ViewModel = new ViewModel();
        }

        public virtual void Dispose() { }
    }
}