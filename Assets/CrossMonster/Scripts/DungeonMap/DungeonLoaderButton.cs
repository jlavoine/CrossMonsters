using UnityEngine;
using Zenject;

namespace MonsterMatch {
    public class DungeonLoaderButton : MonoBehaviour {
        public string GameType;
        public int AreaId;
        public int DungeonId;

        [Inject]
        DungeonLoader.Factory DungeonLoaderFactory;

        private DungeonLoader mLoader;

        void Start() {
            mLoader = DungeonLoaderFactory.Create();
        }

        public void OnClick() {
            mLoader.OnClick( GameType, AreaId, DungeonId );
        }
    }
}
