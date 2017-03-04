
namespace MyLibrary {
    public abstract class PresentationModel : IPresentationModel {
        private ViewModel mModel;
        public ViewModel ViewModel { get { return mModel; } private set { mModel = value; } }

        private IBusinessModel mBusinessModel;

        // After CrossMonsters, remove this ctor!!!
        public PresentationModel() {
            ViewModel = new ViewModel();
        }

        public PresentationModel( IBusinessModel i_businessModel ) {
            ViewModel = new ViewModel();
            SetBusinessModel( i_businessModel );
            SubscribeToBusinessModelUpdate( true );
        }

        public void Dispose() {
            if ( mBusinessModel != null ) {
                SubscribeToBusinessModelUpdate( false );
            }

            _Dispose();
        }

        protected virtual void OnModelUpdated() { }
        protected virtual void _Dispose() { }

        private void SetBusinessModel( IBusinessModel i_model ) {
            mBusinessModel = i_model;
        }

        private void SubscribeToBusinessModelUpdate( bool i_subscribe ) {
            if ( i_subscribe ) {
                mBusinessModel.ModelUpdated += OnModelUpdated;
            } else {
                mBusinessModel.ModelUpdated -= OnModelUpdated;
            }
        }
    }
}