using UnityEngine;
using Zenject;

namespace MonsterMatch {
    public class DungeonLoaderButton : MonoBehaviour {
        public string GameType;

        [Inject]
        DungeonLoader.Factory DungeonLoaderFactory;

        private DungeonLoader mLoader;

        void Start() {
            mLoader = DungeonLoaderFactory.Create();
        }

        public void OnClick() {
            mLoader.OnClick( GameType );
        }
    }
}
