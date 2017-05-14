using Zenject;
using MyLibrary;
using UnityEngine;

namespace MonsterMatch {
    public class LoginPromoDisplaysView : GroupView {
        public GameObject ShortDisplayPrefab;
        public GameObject DisplayAreaParent;

        [Inject]
        ILoginPromoDisplaysPM PM;

        void Start() {
            SetModel( PM.ViewModel );

            CreatePromoDisplays();            
        }

        private void CreatePromoDisplays() {
            foreach ( ISingleLoginPromoDisplayPM pm in PM.DisplayPMs ) {
                string prefabKey = pm.GetPrefab();
                GameObject prefab = GetPrefabFromKey( prefabKey );

                if ( prefab != null ) {
                    GameObject displayObject = gameObject.InstantiateUI( prefab, DisplayAreaParent );
                    SingleLoginPromoDisplayView viewScript = displayObject.GetComponent<SingleLoginPromoDisplayView>();
                    viewScript.Init( pm );
                }
            }
        }

        private GameObject GetPrefabFromKey(string i_key) {
            switch ( i_key ) {
                case "ShortDisplay":
                    return ShortDisplayPrefab;
                default:
                    return null;
            }
        }
    }
}