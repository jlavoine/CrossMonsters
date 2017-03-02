using Zenject;

namespace CrossMonsters {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IChainManager>().To<ChainManager>().AsSingle();

            Container.Bind<GameBackgroundPM>().AsSingle();

            Container.Bind<IInitializable>().To<GameBoard>().AsSingle();
            Container.Bind<IGameBoard>().To<GameBoard>().AsSingle();

            Container.Bind<IInitializable>().To<GameBoardPM>().AsSingle();
            Container.Bind<GameBoardPM>().AsSingle();                  

            Container.Bind<IGameRules>().To<GameRules>().AsSingle();
            Container.Bind<IMonsterManager>().To<MonsterManager>().AsSingle();

            Container.BindFactory<int, GamePiece, GamePiece.Factory>();
        }
    }
}