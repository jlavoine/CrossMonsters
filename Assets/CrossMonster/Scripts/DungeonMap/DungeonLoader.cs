using Zenject;
using MyLibrary;

namespace CrossMonsters {
    public class DungeonLoader {
        readonly ILoadingScreenPM mLoadingPM;

        public DungeonLoader( ILoadingScreenPM i_loadingPM ) {
            mLoadingPM = i_loadingPM;
        }

        public void OnClick() {
            mLoadingPM.Show();
        }

        public class Factory : Factory<DungeonLoader> { }
    }
}
