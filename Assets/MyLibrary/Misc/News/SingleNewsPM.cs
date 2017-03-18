using Zenject;

namespace MyLibrary {
    public class SingleNewsPM : PresentationModel, ISingleNewsPM {
        public const string TITLE_PROPERTY = "Title";
        public const string BODY_PROPERTY = "Body";

        readonly IStringTableManager mStringTable;

        private IBasicNewsData mNewsData;

        public SingleNewsPM( IStringTableManager i_stringTable, IBasicNewsData i_newsData ) {
            mStringTable = i_stringTable;
            mNewsData = i_newsData;

            SetTitleText();
            SetBodyText();
        }

        private void SetTitleText() {
            string text = mStringTable.Get( mNewsData.GetTitleKey() );
            ViewModel.SetProperty( TITLE_PROPERTY, text );
        }

        private void SetBodyText() {
            string text = mStringTable.Get( mNewsData.GetBodyKey() );
            ViewModel.SetProperty( BODY_PROPERTY, text );
        }

        public class Factory : Factory<IBasicNewsData, SingleNewsPM> { }
    }
}
