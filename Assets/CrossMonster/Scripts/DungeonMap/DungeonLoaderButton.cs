using UnityEngine;
using Zenject;

namespace CrossMonsters {
    public class DungeonLoaderButton : MonoBehaviour {
        [Inject]
        DungeonLoader.Factory DungeonLoaderFactory;

        private DungeonLoader mLoader;

        void Start() {
            mLoader = DungeonLoaderFactory.Create();
        }

        public void OnClick() {
            mLoader.OnClick();
        }
    }
}
