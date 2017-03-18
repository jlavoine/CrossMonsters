using Zenject;
using UnityEngine;

namespace MyLibrary {
    public class AllNewsView : GroupView {
        public GameObject SingleNewsViewPrefab;
        public GameObject NewsContent;

        [Inject]
        DiContainer container;

        [Inject]
        INewsManager NewsManager;

        [Inject]
        IAllNewsPM PM;

        void Start() {
            SetModel( PM.ViewModel );

            CreateSingleNewsViews();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void ShowNews() {
            PM.Show();
        }

        public void HideNews() {
            PM.Hide();
        }

        private void CreateSingleNewsViews() {
            foreach( IBasicNewsData newsData in NewsManager.NewsList ) {
                GameObject newsObject = container.InstantiatePrefab( SingleNewsViewPrefab, NewsContent.transform );
                SingleNewsView newsView = newsObject.GetComponent<SingleNewsView>();
                newsView.Init( newsData );
            }
        }
    }
}
