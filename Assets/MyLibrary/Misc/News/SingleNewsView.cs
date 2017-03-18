using Zenject;

namespace MyLibrary {
    public class SingleNewsView : GroupView {
        private ISingleNewsPM mPM;

        [Inject]
        SingleNewsPM.Factory NewsFactory;

        public void Init( IBasicNewsData i_news ) {
            mPM = NewsFactory.Create( i_news );
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}
