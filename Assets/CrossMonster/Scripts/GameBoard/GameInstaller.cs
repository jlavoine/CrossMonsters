using Zenject;

namespace CrossMonsters {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IChainManager>().To<ChainManager>().AsSingle();
            Container.Bind<IMonsterManager>().To<MonsterManager>().AsSingle();

            Container.Bind<GameBackgroundPM>().AsSingle();
        }
    }
}